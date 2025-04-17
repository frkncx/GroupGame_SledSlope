using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    public float jumpThreshold = 0.05f;

    public Transform[] groundChecks;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool isGrounded = true;

    public GameObject slideeee;
    private Rigidbody2D rb;

    [Header("Hazard Related")]
    public GameObject Sled;
    public GameObject sledPrefab;
    public GameObject Penguin;
    public Sprite[] penguinVariations; // 0 norm, 1 sliding
    public int lifes = 2;
    private bool isImmune = false;
    private SpriteRenderer penguinSpriteRenderer;
    private bool shouldSlideOnLand = false; // Flag to check if slide on landing

    [Header("Ability Functions")]
    public bool immunityActivated = false;
    public GameObject immunityAurora;

    // Checkpoint Cooldown Stuff
    private bool checkpointCooldownActive = false;
    public float checkpointCooldownTime = 15f;

    // At Start(), store original sled position:
    private Vector3 originalSledPosition;
    private Quaternion originalSledRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        penguinSpriteRenderer = Penguin.GetComponent<SpriteRenderer>();

        if (Sled != null)
        {
            originalSledPosition = Sled.transform.localPosition;
            originalSledRotation = Sled.transform.localRotation;
        }

        GameManager.Instance.OnRepairRequested += Repair;
        GameManager.Instance.OnImmunityRequested += ImmunityAbility;


        
    }

    private void Update()
    {
        FallBelow();

        // Always reset before checking
        isGrounded = false;

        if (immunityActivated)
        {
            immunityAurora.SetActive(true);

            isGrounded = true; // Disable ground check while immune
        }
        else
        {
            foreach (Transform check in groundChecks)
            {
                if (Physics2D.OverlapCircle(check.position, groundCheckRadius, groundLayer))
                {
                    isGrounded = true;
                    break; // No need to check others if one is grounded
                }
            }
            immunityAurora.SetActive(false);
        }
           
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (!isGrounded)
        {
            slideeee.SetActive(false);
        }
        else
        {
            if (GameManager.Instance.isSliding)
            {
                slideeee.SetActive(true);
            }

            if (shouldSlideOnLand)
            {
                penguinSpriteRenderer.sprite = penguinVariations[1]; // Sliding sprite
                shouldSlideOnLand = false;
            }
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void FallBelow()
    {

        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("House") && !checkpointCooldownActive)
        {
            GameManager.Instance.CheckpointReached = true;
            GameManager.Instance.CheckpointCounter++;

            // Start a coroutine to automatically exit checkpoint state after a delay
            StartCoroutine(ExitCheckpointState());
            StartCoroutine(CheckpointCooldown());
        }
    }

    private IEnumerator ExitCheckpointState()
    {
        yield return new WaitForSeconds(2f); // Adjust time as needed
        GameManager.Instance.ResetCheckpointState();
    }

    private IEnumerator CheckpointCooldown()
    {
        checkpointCooldownActive = true;
        yield return new WaitForSeconds(checkpointCooldownTime);
        checkpointCooldownActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            if (immunityActivated)
            {
                Destroy(collision.gameObject);
            }
            else if (!isImmune)
            {
                // Handle hazard collision
                lifes--;
                Destroy(Sled);
                Jump();
                shouldSlideOnLand = true;
                StartCoroutine(ImmunityCooldown());

                if (lifes <= 0)
                {
                    Die();
                }
            }
        }
    }

    IEnumerator ImmunityCooldown()
    {
        isImmune = true;
        yield return new WaitForSeconds(3f);
        isImmune = false;
    }

    void Die()
    {
        if (Penguin != null)
        {
            Destroy(Penguin);
            SceneManager.LoadScene("GameOver");
            GameManager.Instance.StarsCounter = 0;
        }
    }

    #region Ability Function

    public void Repair()
    {
        if (GameManager.Instance.StarsCounter >= 2 && lifes < 2)
        {
            GameManager.Instance.StarsCounter -= 2;
            lifes = 2;

            // Instantiate new sled first
            if (Sled == null && sledPrefab != null)
            {
                Sled = Instantiate(sledPrefab, transform);
                Sled.transform.localPosition = originalSledPosition;
                Sled.transform.localRotation = originalSledRotation;
            }

            // Reset immunity state
            isImmune = true;
            immunityActivated = false;
            StartCoroutine(ImmunityCooldown());

            // Visual feedback
            if (penguinSpriteRenderer != null)
            {
                penguinSpriteRenderer.sprite = penguinVariations[0]; // Normal sprite
            }
        }
    }

    public void ImmunityAbility()
    {
        if (GameManager.Instance.StarsCounter >= 3)
        {
            GameManager.Instance.StarsCounter -= 3;
            StartCoroutine(ImmunityAbilityDuration());
        }
    }

    IEnumerator ImmunityAbilityDuration()
    {
        immunityActivated = true;
        yield return new WaitForSeconds(15f);
        immunityActivated = false;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (groundChecks != null)
        {
            foreach (Transform check in groundChecks)
            {
                if (check != null)
                    Gizmos.DrawWireSphere(check.position, groundCheckRadius);
            }
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRepairRequested -= Repair;

        if (GameManager.Instance != null)
            GameManager.Instance.OnImmunityRequested -= ImmunityAbility;
    }
}
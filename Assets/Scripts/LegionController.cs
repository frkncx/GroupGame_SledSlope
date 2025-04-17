using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionController : MonoBehaviour
{
    public float jumpForce = 7f;
    public float jumpThreshold = 0.05f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool isGrounded = true;

    public Sprite[] supplicantVariations; // 0 norm, 1 sliding
    public GameObject slideeee;
    private Rigidbody2D rb;
    private SpriteRenderer penguinSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        penguinSpriteRenderer = GetComponent<SpriteRenderer>();

        Jump();

        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform based on the current speed
        transform.Translate(2 * Time.deltaTime * Vector2.right);

        // Always reset before checking
        isGrounded = false;

        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
        {
            isGrounded = true;
        }

        if (isGrounded)
        {
            penguinSpriteRenderer.sprite = supplicantVariations[1]; // Sliding sprite
            slideeee.SetActive(true);
        }
        else
        {
            slideeee.SetActive(false);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

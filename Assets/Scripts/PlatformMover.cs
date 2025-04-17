using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float acceleration = 5f;  // How fast the platform accelerates
    public float deceleration = 5f;  // How fast the platform decelerates
    public float maxSpeed = 10f;     // Maximum speed
    public static float currentSpeed = 0f;

    public float breakDeceleration = 10f;

    [Header("Player Check")]
    public LayerMask playerLayer; // Set this to your player's layer
    public float checkRadius = 1f;
    public Transform checkPosition; // Position to check for player (usually center of platform)
    private GameObject player;

    void Start()
    {
        playerLayer = LayerMask.GetMask("Player"); // Ensure this is set to the correct layer
        checkPosition = transform; // Set to the platform's transform
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object
    }

    void Update()
    {
        // Check for movement input
        bool isMoving = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W);

        // Check for break input (left movement)
        bool isBreaking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow);

            if (isMoving && !isBreaking)
            {
                // Accelerate the platform
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
                GameManager.Instance.isSliding = true;
            }
            else if (isBreaking)
            {
                // Apply stronger deceleration for breaking
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, breakDeceleration * Time.deltaTime);
                GameManager.Instance.isSliding = false;
            }
            else
            {
                // Decelerate the platform normally when no keys are pressed
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
                GameManager.Instance.isSliding = false;
            }


            // Move the platform based on the current speed
            transform.Translate(currentSpeed * Time.deltaTime * Vector2.left);

        CheckForDespawn();
    }

    void CheckForDespawn()
    {
        if (player == null) return;

        float playerX = player.transform.position.x;
        float platformX = transform.position.x;

        // Only despawn if platform is at least 30 units behind player
        if (platformX < playerX - 60f)
        {
            Destroy(gameObject);
        }
    }
}

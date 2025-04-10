using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private float speed = 10f;

    void Update()
    {
        // TODO: Create a smooth Movement for the platform, and make it move by player pressing right arrow key, D, or W
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -50f)
        {
            Destroy(gameObject);
        }
    }
}

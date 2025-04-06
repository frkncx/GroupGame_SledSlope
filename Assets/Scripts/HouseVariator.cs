using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseVariator : MonoBehaviour
{
    public Sprite[] houseSprites;

    private void OnEnable()
    {
        // Spawn a random star sprite when the object is enabled
        int randomIndex = Random.Range(0, houseSprites.Length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && houseSprites.Length > 0)
        {
            spriteRenderer.sprite = houseSprites[randomIndex];
        }
    }
}

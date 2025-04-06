using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDiversifier : MonoBehaviour
{
    public Sprite[] starSprites;

    private void OnEnable()
    {
        // Spawn a random star sprite when the object is enabled
        int randomIndex = Random.Range(0, starSprites.Length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (starSprites.Length > 0)
        {
            spriteRenderer.sprite = starSprites[randomIndex];
        }
    }
}

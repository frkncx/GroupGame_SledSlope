using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            GameManager.Instance.StarsCounter++;
        }
    }
}

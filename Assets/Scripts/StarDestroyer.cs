using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarDestroyer : MonoBehaviour
{
    UIController uIControllerScript;

    private void Start()
    {
        uIControllerScript = GetComponent<UIController>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            uIControllerScript.starsCounter++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsMeny : MonoBehaviour
{
    [SerializeField] private Button backGameButton;

    // Start is called before the first frame update
    void Start()
    {
        backGameButton.onClick.AddListener(OnBackGameClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
            //AudioManager.Play(AudioName.ButtonClick);
        }
    }

    void OnBackGameClicked()
    {
        SceneManager.LoadScene("MenuScene");
        //AudioManager.Play(AudioName.ButtonClick);
    }
}

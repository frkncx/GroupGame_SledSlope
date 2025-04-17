using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

        resumeButton.onClick.AddListener(OnStartGameClicked);
        quitGameButton.onClick.AddListener(OnQuitGameClicked);

    }
    void OnStartGameClicked()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
        //AudioManager.Play(AudioName.ButtonClick);
    }

    void OnQuitGameClicked()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
        SceneManager.LoadScene("MenuScene");
        //AudioManager.Play(AudioName.ButtonClick);
    }
}

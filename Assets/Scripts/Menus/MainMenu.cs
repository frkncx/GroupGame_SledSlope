using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button controlsGameButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameClicked);
        controlsGameButton.onClick.AddListener(OnSettingsClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);
        quitGameButton.onClick.AddListener(OnQuitGameClicked);
    }

    void OnStartGameClicked()
    {
        SceneManager.LoadScene("Gameplay");
        //AudioManager.Play(AudioName.ButtonClick);
    }

    void OnSettingsClicked()
    {
        SceneManager.LoadScene("SettingsMenu");
        //AudioManager.Play(AudioName.ButtonClick);
    }

    void OnCreditsClicked()
    {
        SceneManager.LoadScene("CreditsMenu");
        //AudioManager.Play(AudioName.ButtonClick);
    }

    void OnQuitGameClicked()
    {
        Application.Quit();
        //AudioManager.Play(AudioName.ButtonClick);
    }
}

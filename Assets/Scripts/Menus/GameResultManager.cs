using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GameResultManager : MonoBehaviour
{
    [SerializeField] private Button playAgainGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private TMP_Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        playAgainGameButton.onClick.AddListener(OnStartGameClicked);
        quitGameButton.onClick.AddListener(OnQuitGameClicked);
        scoreText.text = "Your Score: " + CheckpointCounterStorage.checkpointScore.ToString();

        GameManager.Instance.CheckpointCounter = 0;

    }
    void OnStartGameClicked()
    {
        SceneManager.LoadScene("Gameplay");
        //AudioManager.Play(AudioName.ButtonClick);
    }

    void OnQuitGameClicked()
    {
        SceneManager.LoadScene("MenuScene");
        //AudioManager.Play(AudioName.ButtonClick);
    }
}

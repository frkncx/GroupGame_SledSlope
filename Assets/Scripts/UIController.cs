using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text secondsNumberText;
    public TMP_Text starsNumberText;
    public TMP_Text checkpointCounter;

    private int secondsCounter = 0;

    public GameObject CheckpointPanel;
    private bool hasCheckpointBeenShown = false;

    void Start()
    {
        StartCoroutine(CountUp());
    }

    private void Update()
    {
        starsNumberText.text = GameManager.Instance.StarsCounter.ToString();
        checkpointCounter.text = GameManager.Instance.CheckpointCounter.ToString();

        // Show panel only once when checkpoint is reached
        if (GameManager.Instance.CheckpointReached && !hasCheckpointBeenShown)
        {
            CheckpointPanel.SetActive(true);
            hasCheckpointBeenShown = true; // Mark as shown
        }

        // Close panel on ESC
        if (Input.GetKeyDown(KeyCode.Escape) && CheckpointPanel.activeSelf)
        {
            CloseCheckpointPanel();
        }
    }

    private IEnumerator CountUp()
    {
        while (true)
        {
            secondsCounter++;
            secondsNumberText.text = secondsCounter.ToString()+"s";
            yield return new WaitForSeconds(1f);
        }
    }

    public void ImmunityAbility()
    {
        if (GameManager.Instance.StarsCounter >= 3)
        {
            GameManager.Instance.RequestImmunity();
            CheckpointPanel.SetActive(false);
            GameManager.Instance.CheckpointReached = false;
            CloseCheckpointPanel();
            GameManager.Instance.BeginGracePeriod();
        }
    }

    public void RepairSled()
    {
        if (GameManager.Instance.StarsCounter >= 2)
        {
            GameManager.Instance.RequestRepair();
            CheckpointPanel.SetActive(false);
            GameManager.Instance.CheckpointReached = false;
            CloseCheckpointPanel();
            GameManager.Instance.BeginGracePeriod();
        }
    }

    public void PenguinLegion()
    {
        if (GameManager.Instance.StarsCounter >= 5)
        {
            GameManager.Instance.RequestLegions();
            CloseCheckpointPanel();
            GameManager.Instance.BeginGracePeriod();
        }
    }

    private void CloseCheckpointPanel()
    {
        Debug.Log("Closing checkpoint panel.");
        CheckpointPanel.SetActive(false);
        GameManager.Instance.CheckpointReached = false;
        hasCheckpointBeenShown = false;
    }
}

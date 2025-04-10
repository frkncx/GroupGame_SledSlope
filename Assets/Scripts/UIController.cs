using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text secondsNumberText;
    public TMP_Text starsNumberText;

    private int secondsCounter = 0;
    public int starsCounter = 0;

    void Start()
    {
        StartCoroutine(CountUp());
    }

    private void Update()
    {
        starsNumberText.text = starsCounter.ToString();
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
}

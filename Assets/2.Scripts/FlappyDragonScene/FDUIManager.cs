using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class FDUIManager : MonoBehaviour
{
    public static FDUIManager Instance { get; private set; }

    public GameObject homeUI;
    public GameObject howToUI;
    public GameObject playUI;
    public GameObject resultUI;

    public TextMeshProUGUI scoreText;
    private GameObject currentUI;

    public TextMeshProUGUI resultUIScore;
    public TextMeshProUGUI resultUIBestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        homeUI.SetActive(true);
        playUI.SetActive(false);

        currentUI = homeUI;

        FDGameManager.Instance.UpdateScore += UpdateScore;
    }

    public void SwitchUI(GameObject UIobj)
    {
        currentUI.SetActive(false);
        currentUI = UIobj;
        currentUI.SetActive(true);
    }

    public void ShowUI(FDGameState gameState)
    {
        switch (gameState)
        {
            case FDGameState.Home:
                SwitchUI(homeUI);
                break;
            case FDGameState.HowTo:
                SwitchUI(howToUI);
                break;
            case FDGameState.Play:
                SwitchUI(playUI);
                break;
            case FDGameState.Result:
                resultUIScore.text = FDGameManager.Instance.score.ToString();
                resultUIBestScore.text = FDGameManager.Instance.highScore.ToString();
                SwitchUI(resultUI);
                break;
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}

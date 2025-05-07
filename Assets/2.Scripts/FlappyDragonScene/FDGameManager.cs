using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FDGameState
{
    Home,
    HowTo,
    Play,
    Result
}
public class FDGameManager : MonoBehaviour
{
    public static FDGameManager Instance { get; private set; }

    private const string FlappyDragon_HighScore = "FlappyDragon_HighScore";

    [SerializeField] private FDCameraManager cam;
    [SerializeField] private FDLevelSpawner levelSpawner;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;
    
    [SerializeField] private List<GameObject> floors;
    [SerializeField] private List<Vector3> floorsOriginalPos;

    public FDGameState gameState;

    public int highScore;
    public int score;

    public event Action<int> UpdateScore;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        gameState = FDGameState.Home;

        // 게임이 가벼워서 편법으로 각종 초기화
        floorsOriginalPos = new List<Vector3>();
        foreach (GameObject floor in floors)
        {
            floorsOriginalPos.Add(floor.transform.position);
        }
    }

    private void Update()
    {
        if (gameState == FDGameState.Home)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeGameState(FDGameState.HowTo);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else if (gameState == FDGameState.HowTo)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
        else if (gameState == FDGameState.Play)
            Time.timeScale *= 1 + 0.03f * Time.deltaTime;
        else if (gameState == FDGameState.Result)
        {
            HandleResultState();
        }
    }

    private void HandleResultState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(AudioManager.Instance.gameObject);
            SceneManager.LoadScene("GameScene");
        }
    }

    public void StartGame()
    {
        GameObject playerObj = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
        cam.Init(playerObj);

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].transform.position = floorsOriginalPos[i];
        }

        levelSpawner.ClearObjs();
       
        score = 0;
        UpdateScore?.Invoke(score);
        ChangeGameState(FDGameState.Play);
        levelSpawner.SpawnLevels();
    }

    public void GameOver()
    {
        Time.timeScale = 1;
        highScore = GetHighScore();
        if (score > highScore)
        {
            highScore = score;
            SaveScore();
        }
        ChangeGameState(FDGameState.Result);
    }

    private void ChangeGameState(FDGameState state)
    {
        gameState = state;
        FDUIManager.Instance.ShowUI(state);
    }

    public void AddScore(int score)
    {
        Debug.Log("AddScore");
        this.score += score;
        UpdateScore?.Invoke(this.score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(FlappyDragon_HighScore, 0);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(FlappyDragon_HighScore, score);
    }
}

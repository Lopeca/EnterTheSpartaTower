using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Default,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PlayerController player;
    public PlayerController Player { get => player; }

    public GameState State { get; private set; }
    public Vector2 startingPos;
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
        startingPos = player.transform.position;
    }

    private void Update()
    {
        if(State == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        if (GameStateCarrier.Instance.isFirstLoad)
        {
            GameStateCarrier.Instance.isFirstLoad = false;
            return;
        }

        GameStateCarrier.Instance.LoadState();
    }

    internal void GameOver()
    {
        GameStateCarrier.Instance.ResetState();
        State = GameState.GameOver;
        UIManager.Instance.ShowRestartGuide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 세이브 로드 구현 예정이 없어서 직렬화 구조체 없이 진행
public class GameStateCarrier : MonoBehaviour
{
    public static GameStateCarrier Instance { get; private set; }

    public float lastHP;
    public float lastStamina;
    public Vector2 playerPos;

    public bool isFirstLoad;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            isFirstLoad = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveState()
    {
        PlayerController player = GameManager.Instance.Player;
        lastHP = player.PlayerStatHandler.CurrentHP;
        lastStamina = player.PlayerStatHandler.CurrentStamina;
        playerPos = player.transform.position;
    }
    
    public void LoadState()
    {
        PlayerController player = GameManager.Instance.Player;
        player.PlayerStatHandler.LoadStatState(lastHP, lastStamina);
        player.transform.position = playerPos;
        isFirstLoad = false;
    }
}

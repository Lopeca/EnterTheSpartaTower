using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PlayerController player;
    public PlayerController Player { get => player; }

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


}

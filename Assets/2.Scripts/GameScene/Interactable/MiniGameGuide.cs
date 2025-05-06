using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameGuide : MonoBehaviour
{
    public void LoadFlappyBirb()
    {
        GameStateCarrier.Instance.SaveState();
        Debug.Log("플래피 버드 게임으로");
        SceneManager.LoadScene("FlappyDragonScene");
    }

    public void LoadTheStack()
    {
        GameStateCarrier.Instance.SaveState();
        Debug.Log("더 스택 게임으로");
    }
}

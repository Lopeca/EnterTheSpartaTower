using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameGuide : MonoBehaviour
{
    public void LoadFlappyBirb()
    {
        GameStateCarrier.Instance.SaveState();
        Debug.Log("�÷��� ���� ��������");
        SceneManager.LoadScene("FlappyDragonScene");
    }

    public void LoadTheStack()
    {
        GameStateCarrier.Instance.SaveState();
        Debug.Log("�� ���� ��������");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDCoin : MonoBehaviour, ICollectable
{
    public int score;
    public AudioClip coinSE;
    public void Collect()
    {
        FDGameManager.Instance.AddScore(score);
        Destroy(gameObject);
    }
}

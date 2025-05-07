using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNPC : MonoBehaviour
{
    public GameObject chatBox1;
    public GameObject chatBox2;

    public NPC npc;
    private Coroutine balloonDuration;
    
    public void ShowHealMessageBalloon()
    {
        if(balloonDuration != null) StopCoroutine(balloonDuration);
        chatBox1.SetActive(false);
        chatBox2.SetActive(true);
        balloonDuration = StartCoroutine(AutoHideBalloon());
    }

    IEnumerator AutoHideBalloon()
    {
        yield return new WaitForSeconds(3f);
        chatBox2.SetActive(false);
        if (npc.IsInteracting) chatBox1.SetActive(true);
    }
}

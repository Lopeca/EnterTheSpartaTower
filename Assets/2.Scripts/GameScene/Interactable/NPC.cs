using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour, IInteractable
{
    public SpriteRenderer sprite;
    private Material material;

    static readonly float thicknessZero = 0f;
    static readonly float thicknessInteractable = 0.05f;

    public GameObject chatBalloon;
    public UnityEvent npcEvent;
    public bool IsInteracting { get; set; }

    void Start()
    {
        if (chatBalloon != null) chatBalloon.SetActive(false);
        material = sprite.material;
        material.SetFloat("_Thickness", thicknessZero);
    }
    public void Interact()
    {
        npcEvent.Invoke();
    }

    void Update()
    {
        sprite.sortingOrder = GameUtility.SortByPos(transform.position.y);
       
        if (IsInteracting)
        {
            ActivateOutLine();
            if (chatBalloon != null) chatBalloon.SetActive(true);
        }
        else
        {
            DeactivateOutLine();
            if (chatBalloon != null) chatBalloon.SetActive(false);
        }
    }

    public void ActivateOutLine()
    {
        material.SetFloat("_Thickness", thicknessInteractable);
    }
    public void DeactivateOutLine()
    {
        material.SetFloat("_Thickness", thicknessZero);
    }


}

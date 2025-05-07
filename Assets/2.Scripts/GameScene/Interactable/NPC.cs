using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// 부모 오브젝트의 충돌은 혹시나 폭발 연출이 있을 시 폭발에 밀려나는 npc를 상상하며
// 충돌 박스와 탐지범위 박스 콜라이더 두 종류가 셋팅되어 있습니다
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider staminaSlider;

    private PlayerController player;


    private void Start()
    {
        player = GameManager.Instance.Player;

        player.PlayerStatHandler.OnChangeStamina += UpdateStaminaSlider; 
        UpdateHPSlider(1);
        UpdateStaminaSlider(1);
    }
    private void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    private void UpdateStaminaSlider(float percentage)
    {
        staminaSlider.value = percentage;
    }

    private void OnDisable()
    {
        player.PlayerStatHandler.OnChangeStamina -= UpdateStaminaSlider;
    }
}

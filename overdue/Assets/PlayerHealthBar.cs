using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerEntity player;
    [SerializeField] private float animTime;

    [SerializeField] private Slider slider;

    private float max;
    private float current;

    private void Awake()
    {
        UpdateSliderValues();
        player.OnTakeDamage += OnTakeDamageUIUpdate;
    }

    private void OnTakeDamageUIUpdate() {
        UpdateSliderValues();
    }
    private void UpdateSliderValues()
    {
        current = player.GetCurrentHealth();
        max = player.GetMaxHealth();

        slider.maxValue = max;
        slider.minValue = 0;
        slider.value = current;
    }

}

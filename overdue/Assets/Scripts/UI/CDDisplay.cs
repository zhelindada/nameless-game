using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDDisplay : MonoBehaviour
{
    [SerializeField] private PlayerAbility _abilities;

    [SerializeField] private RectTransform _skillRT;
    [SerializeField] private RectTransform _burstRT;

    [SerializeField] private SkillChargesDisplayUI _skillCharge;
    [SerializeField] private SkillChargesDisplayUI _burstCharge;

    [SerializeField] private Slider _skillFill;
    [SerializeField] private Slider _burstFill;

    private void Awake()
    {
        
    }

    private void Update()
    {
        _skillFill.value = _abilities.SkillCooldownTimer01;
        _burstFill.value = _abilities.BurstCooldownTimer01;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDDisplay : MonoBehaviour
{
    [SerializeField] private PlayerAbility _abilities;

    [SerializeField] private RectTransform _skillRT;
    [SerializeField] private RectTransform _burstRT;

    [SerializeField] private SkillChargesDisplayUI _skillCharge;
    [SerializeField] private SkillChargesDisplayUI _burstCharge;

    private void Awake()
    {
        
    }

    private void Update()
    {
        // TODO: update cooldown for skill
        RectTransform p = _skillRT.parent as RectTransform;
        float skillSize = p.rect.height;
        _skillRT.anchoredPosition = new Vector2(_skillRT.anchoredPosition.x, _abilities.SkillCooldownTimer01 * skillSize);

        // TODO: update cooldown for burst
    }

}


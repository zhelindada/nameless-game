using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChargesDisplayUI : MonoBehaviour
{

    [SerializeField] private PlayerAbility _abilities;

    [SerializeField] private GameObject _charge;

    [SerializeField] private Color _colour;

    public bool isSkill;
    private int _maxCharge;
    private int _currentCharge;

    private void OnEnable()
    {
        if(isSkill)
            _abilities.OnSkillChargeChange += PullAndUpdateSkillCharge;
        PullAndUpdateSkillCharge();
    }

    private void PullAndUpdateSkillCharge() {
        _maxCharge = _abilities.SkillMaxCharge;
        _currentCharge = _abilities.SkillCurrentCharge;

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < _maxCharge; i++)
        {
            Image img = Instantiate(_charge, transform).GetComponent<Image>();

            if (i < _currentCharge)
                img.color = _colour;
        }
    }

    public void OnDisable()
    {
        if (isSkill)
            _abilities.OnSkillChargeChange -= PullAndUpdateSkillCharge;
    }

}

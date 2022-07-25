using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private PlayerEntity p;

    private void LateAwake()
    {
        p.OnDeath += OnDeathShowPanel;
        gameObject.SetActive(false);
    }

    private void OnDeathShowPanel() {
        gameObject.SetActive(true);
    }

}

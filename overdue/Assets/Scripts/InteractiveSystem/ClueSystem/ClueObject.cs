using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueObject : Interactable
{
    [SerializeField] private ClueData _data;
    [SerializeField] private Transform _indicator;
    private void Awake()
    {
        RegisterEnableInteraction(() =>
        {
            _indicator.gameObject.SetActive(true);
        });
        RegisterDisableInteraction(() =>
        {
            _indicator.gameObject.SetActive(false);
        });
    }

    protected override void Update()
    {
        base.Update();
        if (playerInRange)
            _Interactable = true;
        else
            _Interactable = false;
    }

    public override void OnInteracted()
    {
        base.OnInteracted();
        if (ClueManager.Instance.AddNewFoundClue(_data.index))
            return;
    }

    
}

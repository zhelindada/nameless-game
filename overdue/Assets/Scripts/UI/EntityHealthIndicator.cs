using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthIndicator : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private SpriteMask mask;

    private float currentScale = 1;

    private void Awake()
    {
        entity = transform.parent.GetComponent<Entity>();
        Debug.Log(entity);

        mask = GetComponentInChildren<SpriteMask>();
    }

    public void ChangeBarDisplay(float f) {

        Debug.Log("Entity Health Bar Display Changing");

        Vector2 scale = mask.transform.localScale;
        scale.x = scale.x / currentScale * f;
        mask.transform.localScale = scale;
        currentScale = f;
    }
}

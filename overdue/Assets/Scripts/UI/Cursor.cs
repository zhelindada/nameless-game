using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    public CursorType currentType;

    [SerializeField] private SpriteRenderer spr;

    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _aim;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void SetType(CursorType type) {
        currentType = type;

        switch (type) {
            case CursorType.Normal:
                spr.sprite = _normal;
                break;
            case CursorType.Aim:
                spr.sprite = _aim;
                break;
            default:
                break;
        }
    }

}

public enum CursorType { 
    Normal,
    Aim,
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{

    [SerializeField] private ItemData item;
    [SerializeField] private int number;

    private Collider2D _collider2d;


    private void Awake()
    {
        _collider2d = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Collection Range") {
            while(InventoryManager.Instance.AddItem(item)){ 
                
            }
        }
    }
}
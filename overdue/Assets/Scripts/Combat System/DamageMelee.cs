using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMelee : MonoBehaviour
{
    public bool damagingPlayer;
    public bool damagingEnemy;
    public float damage;
    public float persistenceTime;

    [SerializeField] private float currentTime;

    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= persistenceTime) {
            Debug.Log("DamageMelee persistence terminated.");
            Destroy(gameObject);
        }
    }
}

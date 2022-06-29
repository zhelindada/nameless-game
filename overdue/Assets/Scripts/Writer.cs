using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : MonoBehaviour
{

    [SerializeField] private EntityHealthIndicator healthBar;

    [SerializeField] private int _spawnDistance;
    [SerializeField] private WriterEntity entity;


    public GameObject deadBodyPrefab;
    public GameObject parent;

    private void Awake()
    {
        healthBar = GetComponentInChildren<EntityHealthIndicator>();
        entity = GetComponent<WriterEntity>();
        if (parent == null)
        {
            GameObject _parent = GameObject.Find("World Items");
            if (_parent == null)
            {
                _parent = new GameObject("World Items");
            }
            parent = _parent;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Projectile proj)) {
            /*Debug.Log("Writer collided with projectile");*/
            entity.TakeDamage(proj.damage);
        }
    }
    public void DropStuff() {
        GameObject preb = Instantiate(deadBodyPrefab, parent.transform);

        Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value).normalized;

        preb.transform.position = gameObject.transform.position + randomDirection * _spawnDistance * Random.value;
        WriterSpawner.WriterGetsKilled();
        Destroy(gameObject);
    }

    public void UpdateHealthBar(float f) {
        healthBar.ChangeBarDisplay(f);
    }
}

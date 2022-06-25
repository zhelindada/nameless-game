using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : MonoBehaviour
{

    [SerializeField] private int _spawnDistance;

    public GameObject deadBodyPrefab;
    public GameObject parent;

    private void Start()
    {
        
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
            GameObject.Instantiate(deadBodyPrefab, parent.transform);

            Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value).normalized;

            deadBodyPrefab.transform.position = gameObject.transform.position + randomDirection * _spawnDistance * Random.value;
            WriterSpawner.WriterGetsKilled();
            Destroy(gameObject);
        }
    }
}

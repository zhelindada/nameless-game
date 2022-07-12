using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{

    public bool DamagesPlayer;

    [SerializeField]protected int _remainingHitTimeCounter;
    [SerializeField] protected float _remainingTravelDistance;
    [SerializeField]protected float _disappearTimer;
    [SerializeField]protected float _damageTimer;

    // ser priv references
    [SerializeField] protected Collider2D _collider2d;
    [SerializeField] protected Sprite _objectSprite;
    [SerializeField] protected Entity _sourceEntity;

    // ser priv variables
    [SerializeField] protected List<DisappearType> _disappearTypes;
    [SerializeField] protected float _dmgAmount;

    // Hit Instances
    [SerializeField] protected int _hitTriggerTime;

    // Travel Distance
    [SerializeField] protected float _travelDistance;
    [SerializeField] protected Vector2 _travelDirection;
    [SerializeField] protected float _projectileSpeed;

    // Time out
    [SerializeField] protected float _disappearTime;
    [SerializeField] protected float _damageTime;

    private void Awake()
    {
        _remainingHitTimeCounter = _hitTriggerTime;
        _remainingTravelDistance = _travelDistance;
        _disappearTimer = _disappearTime;
        _damageTimer = _damageTime;
    }
    private void Update()
    {
        _remainingTravelDistance -= Time.deltaTime * _projectileSpeed;
        _disappearTimer -= Time.deltaTime;
        _damageTimer -= Time.deltaTime;

        if(_disappearTypes.Contains(DisappearType.OnTravelDistance))
            MoveProjectile();

        if (ShallDisappear())
            StartCoroutine(Disappear());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageTimer <= 0)
            return;

        if (collision.TryGetComponent(out Entity entity)) {
            Debug.Log(entity);
            if (DamagesPlayer)
            {
                if (entity.name.Equals("Player"))
                {
                    OnHitSupposedTarget(collision);
                    entity.GetHitBy(this);
                }
            }
            else
            {
                if (!entity.name.Equals("Player"))
                {
                    Debug.Log(collision.name);
                    OnHitSupposedTarget(collision);
                    entity.GetHitBy(this);
                }
            }
        }
    }
    protected void MoveProjectile()
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) +
            _travelDirection * _projectileSpeed * Time.deltaTime;

        float deltaDistance = (_travelDirection * _projectileSpeed * Time.deltaTime).magnitude;
        _travelDistance -= deltaDistance;

        transform.position = newPosition;
    }

    protected virtual void OnHitSupposedTarget(Collider2D hit)
    {
        if (_disappearTypes.Contains(DisappearType.OnHitInstances))
        {
            _remainingHitTimeCounter --;
        }
    }

    protected bool ShallDisappear() {
        if (_disappearTypes.Contains(DisappearType.OnHitInstances)) {
            if (_remainingHitTimeCounter <= 0) {
                return true;
            }
        }
        if (_disappearTypes.Contains(DisappearType.OnTravelDistance))
        {
            if (_remainingTravelDistance <= 0)
            {
                return true;
            }
        }
        if (_disappearTypes.Contains(DisappearType.OnTimeOut))
        {
            if (_disappearTimer <= 0)
                return true;
        }
        return false;
    }

    protected virtual IEnumerator Disappear() {
        yield return null;
        Destroy(gameObject);
    }

    public float GetDamageAmount() {

        return _dmgAmount;
    }
    public void SetTravelDirection(Vector2 direction)
    {
        if(_disappearTypes.Contains(DisappearType.OnTravelDistance))
        _travelDirection = direction.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);
    }

}

public enum DisappearType { 
    OnHitInstances,
    OnTravelDistance,
    OnTimeOut,
}
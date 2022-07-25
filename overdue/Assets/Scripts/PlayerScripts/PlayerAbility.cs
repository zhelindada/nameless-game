using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    // priv f
    [SerializeField] private float _skillSoftCooldownTimer;
    [SerializeField] private float _skillCooldownTimer;
    [SerializeField] private int _skillCurrentCharge;

    [SerializeField] private float _burstSoftCooldownTimer;
    [SerializeField] private float _burstCooldownTimer;
    [SerializeField] private int _burstCurrentCharge;

    // priv ser f
    [SerializeField] private float _skilldashDistance;
    [SerializeField] private float _skilldashTime;
    [SerializeField] private float _skillSoftCooldown = 0.1f;
    [SerializeField] private float _skillCooldown;
    [SerializeField] private int _skillMaxCharge;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private float _burstSoftCooldown = 0.1f;
    [SerializeField] private float _burstCooldown;
    [SerializeField] private int _burstMaxCharge;

    // fine tuning variable
    [SerializeField] private float _skillBlinkCollisionAvoidance;
    [SerializeField] private float _skillLandingDistance;
    [SerializeField] private float _skillLandingMaxSpeed;
    [SerializeField] private float _skillLandingDeceleration;
    [SerializeField] private float _skillLandingTime;

    [SerializeField] private GameObject _burstInitialDamageCollider;
    [SerializeField] private GameObject _burstFollowUpDamageCollider;
    [SerializeField] private float _burstInitialPauseInterval;
    [SerializeField] private float _burstFollowUpInterval;
    [SerializeField] private float _burstMaxDistance;

    // prefab ref
    [SerializeField] GameObject _skill_sssakura;

    // events
    public event System.Action OnSkillChargeChange;

    #region properties
    public bool IsDashing { get { return _skillSoftCooldownTimer > 0; } }
    public float SkillSoftCooldownTimer { get { return _skillSoftCooldownTimer; } }
    public float SkillSoftCooldownTimer01 { get { return _skillSoftCooldownTimer / _skillSoftCooldown; } }
    public float SkillCooldownTimer { get { return _skillCooldownTimer; } }
    public float SkillCooldownTimer01 { get { return _skillCooldownTimer / _skillCooldown; } }
    public int SkillCurrentCharge {
        get { return _skillCurrentCharge; }
        private set {
            _skillCurrentCharge = value;
            OnSkillChargeChange?.Invoke();
        }
    }
    public int SkillMaxCharge { get { return _skillMaxCharge; } }
    public float BurstSoftCooldownTimer { get { return _burstSoftCooldownTimer; } }
    public float BurstSoftCooldownTimer01 { get { return _burstSoftCooldownTimer / _burstSoftCooldown; } }
    public float BurstCooldownTimer { get { return _burstCooldownTimer; } }
    public float BurstCooldownTimer01 { get { return _burstCooldownTimer / _burstCooldown; } }
    public int BurstCurrentCharge { 
        get { return _burstCurrentCharge; } 
        private set{ _burstCurrentCharge = value; } 
    }
    public int BurstMaxCharge { get { return _burstMaxCharge; } }
    #endregion properties

    // overriden private methods
    private void Awake()
    {
        if (_playerCollider == null) _playerCollider = GetComponent<Collider2D>();
        if(_playerMovement == null) _playerMovement = GetComponent<PlayerMovement>();
        _skillCurrentCharge = _skillMaxCharge;
        _skillCooldownTimer = _skillCooldown;
    }
    private void Update()
    {
        _skillSoftCooldownTimer -= Time.deltaTime;

        if(_skillCurrentCharge < _skillMaxCharge)
            _skillCooldownTimer -= Time.deltaTime;

        if (_skillCooldownTimer <= 0) {
            _skillCooldownTimer = _skillCooldown;
            SkillCurrentCharge++;
        }

        if (_burstCurrentCharge < _burstMaxCharge)
            _burstCooldownTimer -= Time.deltaTime;

        if (_burstCooldownTimer <= 0) {
            _burstCooldownTimer = _burstCooldown;
            BurstCurrentCharge++;
        }

    }
    // private methods
    private IEnumerator SkillDash(Vector2 dir) {
        #region collision
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("WorldObjects");

        RaycastHit2D[] hits = new RaycastHit2D[10];

        _playerCollider.Cast(dir, hits, _skilldashDistance);

        float blinkEndDistance = _skilldashDistance;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
                if (hit.collider.attachedRigidbody != null)
                    blinkEndDistance = Mathf.Min(Vector2.Distance(hit.collider.transform.position, transform.position), blinkEndDistance) - _skillBlinkCollisionAvoidance;
        }

        Vector2 totalTravel = dir * _skilldashDistance;
        Vector3 finalPosition = transform.position + new Vector3(totalTravel.x, totalTravel.y, 0);
        #endregion collision
        #region pre
        yield return null;
        _skillSoftCooldownTimer = _skilldashTime;
        #endregion pre
        #region blink
        Vector3 posChangeBlink = blinkEndDistance * dir;
        gameObject.transform.position = gameObject.transform.position + posChangeBlink;
        #endregion blink
        #region spawn SSS
        SeishouSakura.AssignGO(_skill_sssakura);
        SeishouSakura.MakeNewSSS(GameObject.Find("Skill Entities").transform, gameObject.transform.position);
        #endregion spawn SSS
        #region landing
        float vland = _skillLandingMaxSpeed;
        float aland = vland / _skillLandingTime;
        while (vland >= 0) {
            vland -= Time.deltaTime * aland;
            _skillSoftCooldownTimer -= Time.deltaTime;

            Vector3 posChangeLanding = vland * totalTravel.normalized * Time.deltaTime;
            gameObject.transform.position = gameObject.transform.position + posChangeLanding;

            yield return null;
        }
        #endregion landing
        #region post 
        _skillSoftCooldownTimer = 0;
        Debug.Log("Dash Termination");
        #endregion post
    }

    private IEnumerator Burst(Vector2 dir) {
        #region collision prep
        Entity target = Toolkit.FindClosestEnemy(transform.position);
        Vector3 targetPosition;
        if (target != null)
            targetPosition = target.transform.position;
        else
            targetPosition = new Vector3(float.MaxValue, float.MaxValue);

        #endregion collision prep
        if ((targetPosition - transform.position).magnitude > _burstMaxDistance) {
            targetPosition = transform.position + new Vector3(dir.x, dir.y, 0).normalized * _burstMaxDistance;
        }
        yield return null;
        GameObject ini = Instantiate(_burstInitialDamageCollider);
        ini.transform.position = targetPosition;
        int count = SeishouSakura.currentNum;
        SeishouSakura.DestroyAllSSS();

        yield return new WaitForSeconds(_burstInitialPauseInterval);

        for (int i = count; i > 0; i--) {
            GameObject o = Instantiate(_burstFollowUpDamageCollider);
            o.transform.position = targetPosition;
            yield return new WaitForSeconds(_burstFollowUpInterval);
        }
    }
    // public methods
    public void UseSkill(Vector2 dir) {

        if (_skillCurrentCharge < 1) {
            return;
        }
        
        StartCoroutine("SkillDash", dir);
        SkillCurrentCharge --;
    }

    public void UseBurst(Vector2 dir) {
        if (_burstCurrentCharge < 1)
        {
            return;
        }

        StartCoroutine("Burst", dir);
        BurstCurrentCharge --;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    // priv f
    [SerializeField] private float _skillSoftCooldownTimer;
    [SerializeField] private int _skillCurrentCharge;
    [SerializeField] private float _skillCooldownTimer;

    private float _burstSoftCooldownTimer;
    private float _burstCooldownTimer;
    private int _burstCurrentCharge;

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

    // prefab ref
    [SerializeField] GameObject _skill_sssakura;

    // events
    public event System.Action OnSkillChargeChange;

    // public properties
    public bool IsDashing { get { return _skillSoftCooldownTimer > 0; } }

    // for UI use
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
    public float BurstCooldownTimer01 { get { return _burstCooldownTimer / _skillCooldown; } }
    public int BurstCurrentCharge { 
        get { return _burstCurrentCharge; } 
        private set{ _burstCurrentCharge = value; } 
    }
    public int BurstMaxCharge { get { return _burstMaxCharge; } }

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

        // physics and collision prep
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

        // pre skill
        yield return null;
        _skillSoftCooldownTimer = _skilldashTime;

        // TODO: blink (part 1)
        Vector3 posChangeBlink = blinkEndDistance * dir;
        gameObject.transform.position = gameObject.transform.position + posChangeBlink;
        
        /*
        if (dir.magnitude > _skillLandingDistance)
        {
            Vector3 blinkPosChange = (dir.magnitude - _skillLandingDistance) * dir.normalized;
            gameObject.transform.position = gameObject.transform.position + blinkPosChange;
        }
        else {
            Vector3 blinkPosChange = (dir.magnitude - _skillLandingDistance) * dir.normalized;
            gameObject.transform.position = gameObject.transform.position + blinkPosChange;
        }
        */

        // spawn sss (p2)
        SeishouSakura.AssignGO(_skill_sssakura);
        SeishouSakura.MakeNewSSS(GameObject.Find("Skill Entities").transform, gameObject.transform.position);

        // TODO: landing (p3)
        float vland = _skillLandingMaxSpeed;
        float aland = vland / _skillLandingTime;
        while (vland >= 0) {
            vland -= Time.deltaTime * aland;
            _skillSoftCooldownTimer -= Time.deltaTime;

            Vector3 posChangeLanding = vland * totalTravel.normalized * Time.deltaTime;
            gameObject.transform.position = gameObject.transform.position + posChangeLanding;

            yield return null;
        }
        // post skill
        _skillSoftCooldownTimer = 0;
        Debug.Log("Dash Termination");
    }

    private IEnumerator Burst(Vector2 dir) {
        yield return null;
    }


    // public methods
    public void UseSkill(Vector2 dir) {

        if (_skillCurrentCharge < 1) {
            return;
        }

        // initiation
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{

    [Flags]
    public enum EProjectileType
    {
        Water = 1,
        Poison = 2,
        Fire = 4
    }

    [field: SerializeField] public EProjectileType BulletType { get; protected set; }



    [Header("Weapon Base Stats")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float chargeUpTime;
    [SerializeField, Range(0, 1)] protected float minChargePercent;
    [SerializeField] private bool isFullyAuto;

    private Coroutine _currentFireTimer;
    private bool _isOnCooldown;
    private float _currentChargeTime;

    private WaitForSeconds _coolDownWait;
    private WaitUntil _coolDownEnforce;


    [SerializeField] protected int maxBullet;
    [SerializeField] protected int bulletCount;


    [SerializeField] private TextMeshProUGUI shots;
    [SerializeField] protected AudioSource gunShotSource;




    


    void Start()
    {
        bulletCount = maxBullet;
        _coolDownWait = new(timeBetweenAttacks);
        _coolDownEnforce = new WaitUntil(() => !_isOnCooldown);

        BulletType = EProjectileType.Fire;
    }

    private void Update()
    {
        shots.text = bulletCount.ToString() + " / " + maxBullet.ToString();
    }

    public void StartShooting()
    {
        _currentFireTimer = StartCoroutine(RefireTimer());
    }

    public void StopShooting()
    {
        StopCoroutine(_currentFireTimer);

        float percent = _currentChargeTime / chargeUpTime;
        if (percent != 0) TryAttack(percent);
    }

    public void Refresh()
    {
        bulletCount = maxBullet;
        Debug.Log("Reloaded in ProjectileWeapon");
    }

    public void Chan()
    {
        print("Bullet Type changed to " + BulletType);
        if (BulletType == EProjectileType.Fire)
        {
            BulletType = EProjectileType.Water;
        }
        else if (BulletType == EProjectileType.Water)
        {
            BulletType = EProjectileType.Poison;
        }
        else
        {
            BulletType = EProjectileType.Fire;
        }
    }



    private IEnumerator CooldownTimer()
    {
        _isOnCooldown = true;
        yield return _coolDownWait;
        _isOnCooldown = false;
    }

    private IEnumerator RefireTimer() 
    {
        print("Waiting for cooldown");
        yield return _coolDownEnforce;
        print("Post Cooldown");

        while (_currentChargeTime < chargeUpTime)
        {
            _currentChargeTime += Time.deltaTime;
            yield return null;
        }


        TryAttack(1);
        yield return null;
    }

    private void TryAttack(float percent)
    {
        _currentChargeTime = 0;
        if (!CanAttack(percent)) return;

        Attack(percent);

        StartCoroutine(CooldownTimer());

        if (isFullyAuto && percent >= 1) _currentFireTimer = StartCoroutine(RefireTimer()); //autofire

    }

    protected virtual bool CanAttack(float percent)
    {
        return !_isOnCooldown && percent >= minChargePercent;
    }

    protected abstract void Attack(float percent);
}

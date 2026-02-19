using UnityEngine;

[RequireComponent(typeof(WeaponComponent))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(CharacterAnimatorBridge))]
public class CombatController : MonoBehaviour
{
    private WeaponComponent weapon;
    private HealthComponent health;
    private CharacterAnimatorBridge animatorBridge;

    public System.Action OnAmmoEmptyEvent;
    public System.Action OnAmmoLowEvent;

    private bool isDead;
    public System.Action OnDeathEvent;


    private void Awake()
    {
        weapon = GetComponent<WeaponComponent>();
        health = GetComponent<HealthComponent>();
        animatorBridge = GetComponent<CharacterAnimatorBridge>();
    }

    private void Start()
    {
        health.OnDeathEvent += HandleDeath;

        if (weapon.Model != null)
        {
            weapon.Model.OnAmmoEmpty += HandleAmmoEmpty;
            weapon.Model.OnAmmoLow += HandleAmmoLow;
        }
    }


    public void StartAttack()
    {
        if (isDead) return;

        weapon.TryStartFiring();
    }

    public void StopAttack()
    {
        if (isDead) return;

        weapon.StopFiring();
    }

    public void Reload()
    {
        if (isDead) return;

        weapon.TryReload();
    }

    private void HandleDeath()
    {
        isDead = true;
        weapon.StopFiring();

        OnDeathEvent?.Invoke();
    }


    public bool IsDead()
    {
        return isDead;
    }


    public int GetCurrentAmmo()
    {
        return weapon.Model.CurrentAmmo;
    }

    private void HandleAmmoEmpty()
    {
        StopAttack();
        OnAmmoEmptyEvent?.Invoke();
    }

    private void HandleAmmoLow()
    {
        OnAmmoLowEvent?.Invoke();
    }


    public bool IsReloading()
    {
        return weapon.Model.IsReloading;
    }

    public void StartHeal(int amount, float duration)
    {
        if (isDead) return;

        animatorBridge.PlayHeal();
        health.StartHeal(amount, duration);
    }

    public void OnHealAnimationFinished()
    {
        // Optional future hook
    }




}

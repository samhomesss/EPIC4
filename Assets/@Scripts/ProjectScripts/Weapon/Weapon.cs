using UnityEngine;

public class Weapon : BaseObject
{
    #region Variables
    float _coolTimer = 0.0f;
    protected HeroAttackPos _heroAttackPos;
    protected float WeaponDamage = 0.0f; // 무기별 데미지 
    protected float WeaponSpeed = 0.0f; // 무기별 속도 
    protected float WeaponCoolTime = 0.0f;
    protected GameObject WeaponBullet = null; //무기에서 나가는 총알 
    protected bool IsWeaponAttack = false; // 플레이어가 공격을 눌렀을때 체크 
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _heroAttackPos = transform.GetComponentInParent<HeroAttackPos>();

        #region Actions
        Managers.Game.OnAttackActionEvent -= HandleOnAttackChanged;
        Managers.Game.OnAttackActionEvent += HandleOnAttackChanged;
        #endregion


        return true;
    }
    private void Update()
    {
        _coolTimer += Time.deltaTime;
        if (IsWeaponAttack && _coolTimer > WeaponCoolTime)
        {
            WeaponShoot();
            _coolTimer = 0f;
            IsWeaponAttack = !IsWeaponAttack;
        }
    }

    protected virtual void WeaponShoot() 
    {
        GameObject go = Instantiate(WeaponBullet, (Vector2)transform.position + _heroAttackPos.ShootDir, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().AddForce((_heroAttackPos.ShootDir).normalized * WeaponSpeed , ForceMode2D.Impulse);
        float angle = Mathf.Atan2(_heroAttackPos.ShootDir.y, _heroAttackPos.ShootDir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        IsWeaponAttack = !IsWeaponAttack;
    }

    #region Handler
    private void HandleOnAttackChanged(bool isattack)
    {
        IsWeaponAttack = isattack;
    }
    #endregion
}

using System.Collections;
using UnityEngine;

public class DiceWeapon : Weapon
{
    bool _isChangeDice = false;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        WeaponBullet = Resources.Load<GameObject>("Prefabs/DiceBullet");
        WeaponSpeed = 25f;
        WeaponCoolTime = 0.4f;

        Managers.Game.OnChangeDiceEvent += HandlerChangeDice;

        return true;
    }
    protected override void WeaponShoot()
    {
        StartCoroutine(Shoot());
    }

    void HandlerChangeDice(bool isChange)
    {
        _isChangeDice = isChange;
    }

    #region DiceWeapon
    void DiceShootSystem()
    {
        if (_isChangeDice)
        {
            WeaponBullet = Resources.Load<GameObject>("Prefabs/ExplosionDice");
            _isChangeDice = false;
        }
        GameObject go = Instantiate(WeaponBullet, (Vector2)transform.position + _heroAttackPos.ShootDir, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().AddForce(_heroAttackPos.ShootDir * WeaponSpeed, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(_heroAttackPos.ShootDir.y, _heroAttackPos.ShootDir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        WeaponBullet = Resources.Load<GameObject>("Prefabs/DiceBullet");
    }

    IEnumerator Shoot()
    {
        DiceShootSystem();
        yield return new WaitForSeconds(0.1f);
        DiceShootSystem();
        //TODO : UI 변경 들어가야 됨 
        Managers.Game.RollTheDice();
    }
    #endregion
}

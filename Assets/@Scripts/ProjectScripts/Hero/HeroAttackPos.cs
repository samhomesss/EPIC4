using UnityEngine;

/// <summary>
/// 플레이어를 기점으로 마우스 방향으로 바라 보는 위치로 변경 해주는 Attack Pos
/// </summary>
public class HeroAttackPos : BaseObject
{
    public Vector2 ShootDir => _shootDir;

    GameObject _hero;
    Vector2 _shootDir;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _hero = transform.parent.gameObject;

        return true;
    }

    private void Update()
    {
        PlayerShoot();
    }

    void PlayerShoot()
    {
        _shootDir = (GetWorldMousePosition() - (Vector2)_hero.transform.position).normalized;
        transform.position = (Vector2)_hero.transform.position + _shootDir;
    }

    public Vector2 GetScreenMousePosition()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public Vector2 GetWorldMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
}

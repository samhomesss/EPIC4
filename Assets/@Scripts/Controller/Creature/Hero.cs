using UnityEngine;
using static Define;
public class Hero : Creature
{
    enum GameObjects
    {
        Dice,
        GoldParticle,
    }

    #region Variables
    Vector2 _moveDir = Vector2.zero;
    bool _isAttack = false;
    GameObject _goldParticle;
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = ECreatureType.Hero;
        CreatureState = ECreatureState.Idle;
        HP = 100f;
        Damage = 10f;
        Speed = 5.0f;

        BindObjects(typeof(GameObjects));
        _goldParticle = GetObject((int)GameObjects.GoldParticle);

        _goldParticle.SetActive(false);

        Managers.Object.Heros.Add(this); // 현재는 생성 하는게 아니라서 이거 추가 해야됨 
        
        #region Actions
        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        Managers.Game.OnAttackActionEvent -= HandleOnAttackChanged;
        Managers.Game.OnAttackActionEvent += HandleOnAttackChanged;
        Managers.Game.OnSpecialSlotAttackEvent -= HandleSpecialAttackEffect;
        Managers.Game.OnSpecialSlotAttackEvent += HandleSpecialAttackEffect;
        Managers.Game.OnInputSystemStateChanged -= HandleOnInputSystemStateChanged;
        Managers.Game.OnInputSystemStateChanged += HandleOnInputSystemStateChanged;
        #endregion

        return true;
    }

    private void Update()
    {
        HeroMove();
        HeroAttack();
    }

    #region HeroInputAction
    /// <summary>
    /// Hero 움직임
    /// </summary>
    void HeroMove() 
    {
        transform.TranslateEx(_moveDir * Time.deltaTime * Speed);
    }
    /// <summary>
    /// Hero 기본 공격 현재는 이부분이 아마 Weapon 안에 들어 갈 거같음 
    /// </summary>
    void HeroAttack()
    {
        if (_isAttack)
        {
            Debug.Log("Hero 공격 중");
            _isAttack = !_isAttack;
        }
    }
    #endregion

    #region Handler
    private void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }
    private void HandleOnAttackChanged(bool isAttack)
    {
        _isAttack = isAttack;
    }
    private void HandleSpecialAttackEffect(bool isSpecial)
    {
        //TODO : 플레이어 안에 있는 Particle 조절 
        _goldParticle.SetActive(isSpecial);
    }
    private void HandleOnInputSystemStateChanged(EInputSystemState inputSystemState)
    {
        switch (inputSystemState)
        {
            case EInputSystemState.Idle:
                CreatureState = ECreatureState.Idle;
                break;
            case EInputSystemState.Move:
                CreatureState = ECreatureState.Move;
                break;
            case EInputSystemState.Attack:
                CreatureState = ECreatureState.Attack;
                break;
        }
    }
    #endregion
}

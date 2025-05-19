using System;
using UnityEngine;
using static Define;

/// <summary>
/// 게임 중앙 관리 
/// </summary>
public class GameManager
{
    #region Hero
    private Vector2 _moveDir;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set
        {
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }

    private bool _attackAction;
    public bool AttackAction
    {
        get => _attackAction;
        set
        {
            _attackAction = value;
            OnAttackActionEvent?.Invoke(value);
        }
    }

    private EInputSystemState _inputSystemState;
    public EInputSystemState InputSystemState
    {
        get => _inputSystemState;
        set

        {
            _inputSystemState = value;
            OnInputSystemStateChanged?.Invoke(_inputSystemState);
        }
    }
    #endregion

    #region GameAction
    public event Action<Vector2> OnMoveDirChanged;
    public event Action<bool> OnAttackActionEvent;
    public event Action<Define.EInputSystemState> OnInputSystemStateChanged;
    #endregion

    #region UIAction
    public event Action OnRollTheDice; // 주사위 던졌을때 굴러갈 함수 
    public event Action<bool> OnChangeDiceEvent; // TODO : 현재는 boolean으로 구현해서 다음을 보려 하는 거 이후에는 달라 질 듯?
    public event Action<bool> OnSlotMachineEvent; // SlotMachine 돌아가는 이벤트 
    public event Action<bool> OnSpecialSlotAttackEvent; // SlotMachine 공격 이펙트 
    /// <summary>
    /// 주사위 던진거 실행 함수 
    /// </summary>
    public void RollTheDice()
    {
        OnRollTheDice?.Invoke();
    }
    /// <summary>
    /// 주사위가 바뀌었는지 알 수 있는거 
    /// </summary>
    /// <param name="isChange"></param>
    public void ChangeDice(bool isChange)
    {
        OnChangeDiceEvent?.Invoke(isChange);
    }
    public void SlotMachineRoll(bool isSlot)
    {
        OnSlotMachineEvent?.Invoke(isSlot);
    }
    public void SpecialSlotAttack(bool isSpecial)
    {
        OnSpecialSlotAttackEvent?.Invoke(isSpecial);
    }
    #endregion

    public void DisConnect()
    {
        OnMoveDirChanged = null;
        OnAttackActionEvent = null;
        OnInputSystemStateChanged = null;
    }
}

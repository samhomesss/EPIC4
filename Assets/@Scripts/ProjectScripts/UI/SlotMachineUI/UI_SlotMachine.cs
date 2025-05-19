using System.Collections;
using UnityEngine;

public class UI_SlotMachine : UI_Scene
{
    enum GameObjects
    {
        Slot1Animation,
        Slot2Animation,
        Slot3Animation,
    }

    UI_SlotMachineEffect _slotEffect;

    Animator _slot1Animation;
    Animator _slot2Animation;
    Animator _slot3Animation;

    bool _isSlot1Active = false;
    bool _isSlot2Active = false;
    bool _isSlot3Active = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObjects(typeof(GameObjects));

        _slotEffect = FindAnyObjectByType<UI_SlotMachineEffect>();
        _slot1Animation = GetObject((int)GameObjects.Slot1Animation).GetComponent<Animator>();
        _slot2Animation = GetObject((int)GameObjects.Slot2Animation).GetComponent<Animator>();
        _slot3Animation = GetObject((int)GameObjects.Slot3Animation).GetComponent<Animator>();
        Managers.Game.OnSlotMachineEvent += SlotRolling;
        return true;
    }

    private void Update()
    {
        SpecialEffects();
    }

    /// <summary>
    /// if문 구조 수정 해야 될 듯?
    /// </summary>
    /// <param name="isRolling"></param>
    void SlotRolling(bool isRolling)
    {
        if (_isSlot1Active && _isSlot2Active && _isSlot3Active)
            return;

        if (!_isSlot1Active)
        {
            _isSlot1Active = isRolling;
            _slot1Animation.Play("Roll");
            return;
        }
        if (!_isSlot2Active)
        {
            _isSlot2Active = isRolling;
            _slot2Animation.Play("Roll");

            return;
        }
        if (!_isSlot3Active)
        {
            _isSlot3Active = isRolling;
            _slot3Animation.Play("Roll");
            return;
        }
    }

    void SpecialEffects()
    {
        if (_isSlot1Active && _isSlot2Active && _isSlot3Active && Input.GetKeyDown(KeyCode.R))
        {
            _slot1Animation.Play("Idle");
            _slot2Animation.Play("Idle");
            _slot3Animation.Play("Idle");
            _isSlot1Active = false;
            _isSlot2Active = false;
            _isSlot3Active = false;
            // TODO: 여기서 해야 하는거 하고 
            Managers.Game.SpecialSlotAttack(true);
            _slotEffect.GetComponent<Canvas>().enabled = true;
            StartCoroutine(SpecialAttackTime());
        }
    }

    IEnumerator SpecialAttackTime()
    {
        yield return new WaitForSeconds(2f);
        _slotEffect.GetComponent<Canvas>().enabled = false;
        Managers.Game.SpecialSlotAttack(false);
    }

}

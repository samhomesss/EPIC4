using TMPro;
using UnityEngine;

public class UI_Dice : UI_Scene
{
    enum Texts
    {
        Dice1Text,
        Dice2Text,
    }

    TMP_Text _dice1Text; // 던져진 Dice 결과 들어갈 텍스트 1
    TMP_Text _dice2Text;

    //TODO : 추후에 맞았을때만 적용할거면 사용해야됨 
    //bool isDice1 = false; // 해당 오브젝트 다이스가 던져 졌는지 
    //bool isDice2 = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindTexts(typeof(Texts));

        _dice1Text = GetText((int)Texts.Dice1Text);
        _dice2Text = GetText((int)Texts.Dice2Text);

        Managers.Game.OnRollTheDice += RandomDice;

        return true;
    }

    void RandomDice()
    {
        int randomDice1 = Random.Range(1, 7);
        int randomDice2 = Random.Range(1, 7);

        if (randomDice1 == randomDice2)
        {
            // TODO : 같을때 효과 다음 던지는 주사위 하나에 폭발 하나 넣어주면 될 듯?
            Managers.Game.ChangeDice(true);
            Managers.Game.SlotMachineRoll(true);
        }

        _dice1Text.text = $"{randomDice1}";
        _dice2Text.text = $"{randomDice2}";
    }

}

using UnityEngine;

public class UI_777 : UI_Scene
{
    Canvas _canvas;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        Managers.Game.OnSlotMachineFullEvent += OnCanvas;
        return true;
    }

    void OnCanvas(bool isOn)
    {
        _canvas.enabled = isOn;
    }


}

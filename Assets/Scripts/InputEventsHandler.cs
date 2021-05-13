using System;
using Modules.TickModule;
using UI.Components;
using UnityEngine;

public class InputEventsHandler : ITickUpdate
{
    public bool Enabled { get; set; }
    public static EventHandler<int> OnMouseScroll;
    public static EventHandler<Vector2> OnWASDInput;
    public static EventHandler<float> OnMouseRotate;
    public static EventHandler<float> OnMouseScrollRotate;

    public InputEventsHandler(ITickManager tick)
    {
        tick.AddTick(this, this);
        Enabled = true;
    }

    public void Tick()
    {
        if (!Enabled)
            return;
        
        WASDInput();
        MouseRotate();
        OnMouseScroll?.Invoke(this, (int) Input.mouseScrollDelta.y);
    }

    private void MouseRotate()
    {
        OnMouseRotate?.Invoke(this,Input.GetAxis("Mouse X"));
        if(Input.GetMouseButton(2))
            OnMouseScrollRotate?.Invoke(this,Input.GetAxis("Mouse X"));
    }
    private void WASDInput()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var input = new Vector2(x, y);
        OnWASDInput?.Invoke(this, input);
    }


}
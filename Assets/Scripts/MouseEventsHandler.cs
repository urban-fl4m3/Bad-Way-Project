using System;
using Modules.TickModule;
using UI.Components;
using UnityEngine;

public class MouseEventsHandler: ITickUpdate
{
    public bool Enabled { get; set; }
    public static EventHandler<int> OnMouseScroll;
    public static EventHandler<Vector2> OnMouseOverScene;

    public MouseEventsHandler(ITickManager tick)
    {
        tick.AddTick(this, this);
        Enabled = true;
    }

    public void Tick()
    {
        if (!Enabled)
            return;


        //MouseOutOfScene();
        OnMouseScroll?.Invoke(this, (int) Input.mouseScrollDelta.y);
    }

    private void MouseOutOfScene()
    {
        var offset = 200;
        
        var mousePosition = Input.mousePosition;
        var hWidth = Screen.width/2;
        var hHeight = Screen.height/2;

        var mouseX = (hWidth- mousePosition.x) / hWidth;
        var mouseY = (hHeight - mousePosition.y) / hHeight;

        var offsetVector2 =Vector2.zero;

        if (mousePosition.x <= offset) offsetVector2 = new Vector2(-(offset-mousePosition.x)/offset, -mouseY);
        if (mousePosition.x >= Screen.width - offset)
            offsetVector2 = new Vector2( (offset - (Screen.width-mousePosition.x))/offset, -mouseY);

        if (mousePosition.y <= offset) offsetVector2 = new Vector2(-mouseX, -(offset-mousePosition.y)/offset);
        if (mousePosition.y >= Screen.height - offset)
            offsetVector2 = new Vector2(-mouseX, (offset - (Screen.height-mousePosition.y))/offset);
        
        if(!HoverCheck.IsCover)
            OnMouseOverScene?.Invoke(this, offsetVector2);

    }
}
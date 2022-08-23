using Godot;
using System;
using CustomAnimations;

public class SecondOrderNode : RelativeCoordSaver
{

    /// <summary>
    /// Поле, хранящее ноду, которую в данный момент двигают анимации данной ноды
    /// </summary>
    protected Node2D MovingNode = null;

    public override void _EnterTree()
    {
        base._EnterTree();
        MovingNode = GetChild<Node2D>(0);
    }
    
    public override void _Process(float delta)
    {
        base._Process(delta);
        GD.Print(MovingNode.GlobalPosition);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

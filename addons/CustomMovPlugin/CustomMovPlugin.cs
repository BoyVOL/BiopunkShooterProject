using Godot;
using System;

[Tool]
public class CustomMovPlugin : EditorPlugin
{    
    string LibDir = "res://addons/CustomMovPlugin/";
    public override void _EnterTree(){
        base._EnterTree();
        GD.Print("Plugin ready");
        AddCustomType("SwarmController","Node2D",GD.Load<Script>("res://addons/CustomMovPlugin/SwarmController/SwarmControllerNode.cs"),
        GD.Load<Texture>("res://addons/CustomMovPlugin/SwarmController/icon.png"));
        AddCustomType("MoveAngleFollower","Node2D",GD.Load<Script>("res://addons/CustomMovPlugin/MoveAngleFollower/MoveAngleFollowerNode.cs"),
        GD.Load<Texture>("res://addons/CustomMovPlugin/MoveAngleFollower/icon.png"));
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        RemoveCustomType("SwarmController");
        RemoveCustomType("MoveAngleFollower");
    }
}

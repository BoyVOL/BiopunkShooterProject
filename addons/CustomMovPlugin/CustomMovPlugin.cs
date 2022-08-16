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
        AddCustomType("SwarmKreep","Node2D",GD.Load<Script>("res://addons/CustomMovPlugin/SwarmKreep/SwarmKreepNode.cs"),
        GD.Load<Texture>("res://addons/CustomMovPlugin/SwarmKreep/icon.png"));
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        RemoveCustomType("SwarmController");
        RemoveCustomType("SwarmKreep");
    }
}

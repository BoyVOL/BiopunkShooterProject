using Godot;
using System;

[Tool]
public class CustomMovPlugin : EditorPlugin
{    
    string LibDir = "res://addons/CustomMovPlugin/";
    public override void _EnterTree(){
        base._EnterTree();
        GD.Print("Plugin ready");
        GD.Print("Plugin ready2");
        GD.Print("Plugin ready3");
        AddCustomType("CustomBody","Node",GD.Load<Script>(LibDir+"IerarchyNodes/CustomBody.cs"),null);
        AddCustomType("CustomBody1","Node2D",GD.Load<Script>(LibDir+"IerarchyNodes/CustomBody.cs"),null);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        RemoveCustomType("CustomBody");
        RemoveCustomType("CustomBody1");
    }
}

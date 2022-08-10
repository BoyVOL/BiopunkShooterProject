using Godot;
using System;
using customMovement;

public class TestScene : Node2D
{
    SwarmBody TestBody;

    SwarmKreep Kreep1;

    SwarmKreep Kreep2;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TestBody = new SwarmBody(new CollisionShape2D(),new Sprite());
        TestBody.Sprite.Texture = (Texture)ResourceLoader.Load("res://icon.png");
        TestBody.SwarmScale = new Vector2(20,20);
        Kreep1 = new SwarmKreep(new Sprite());
        Kreep1.Sprite.Texture = (Texture)ResourceLoader.Load("res://icon.png");
        Kreep2 = new SwarmKreep(new Sprite());
        Kreep2.Sprite.Texture = (Texture)ResourceLoader.Load("res://icon.png");
        TestBody.AddKreep(Kreep1);
        TestBody.AddKreep(Kreep2);
        this.AddChild(TestBody);
    }

    public override void _PhysicsProcess(float delta){
        TestBody.Position = GetViewport().GetMousePosition();
        /*if(TestBody.OldPos.DistanceSquaredTo(TestBody.Position) <= 4){
            TestBody.Rotation = TestBody.OldTransform.Rotation;
        } else {
            TestBody.Rotation = TestBody.OldPos.AngleToPoint(TestBody.Position);
        }*/
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

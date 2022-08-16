using Godot;
using System;
using customMovement;

namespace customMovement{
    
    /// <summary>
    /// Класс члена роя
    /// </summary>
    public class SwarmCreep: Node2D{

        /// <summary>
        /// Цель движения члена роя
        /// </summary>
        public Vector2 Target;

        [Export]
        /// <summary>
        /// Свойство, отвечающее за то, что член роя перестанет двигаться к его цели
        /// </summary>
        public float epsilon = 1;

        [Export]
        public float MoveSpeed = 0.1f;

        /// <summary>
        /// Метод, проверяющий, достаточно ли объект близко к своей цели
        /// </summary>
        /// <returns></returns>
        public bool NearTarget(){
            return Position.DistanceSquaredTo(Target) < epsilon;
        }

        public override void _Process(float delta){
            base._Process(delta);
            if(!NearTarget()){
                if(Position.DistanceTo(Target) > MoveSpeed)
                Position += Position.DirectionTo(Target)*MoveSpeed;
                else Position = Target;
            };
        }
    }
}

public class SwarmKreepNode : SwarmCreep
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

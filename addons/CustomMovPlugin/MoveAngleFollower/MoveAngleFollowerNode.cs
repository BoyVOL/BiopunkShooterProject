using Godot;
using System;
using CustomAnimations;
using System.Collections.Generic;
    
namespace CustomAnimations{
    
        /// <summary>
        /// Тело, состоящее из нескольких плывущих по воздуху объектов
        /// </summary>
        public class MoveAngleFollower: RelativeCoordSaver{
                public float GetSpeedAngle(){
                        return OldPosition.DirectionTo(GetRelPosition()).Angle();
                }

                public override void _Process(float delta){
                        Rotation = GetSpeedAngle();
                        base._Process(delta);
                }
        }
            
}

public class MoveAngleFollowerNode: MoveAngleFollower{
        
}

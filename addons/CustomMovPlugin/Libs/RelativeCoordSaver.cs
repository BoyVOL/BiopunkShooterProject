using Godot;
using System;

namespace CustomAnimations{

    public class RelativeCoords: Node2D{

        [Export]
        public NodePath RefNodePath = null;
        
        protected Node2D RefNode = null;

        public void GetRefNode(NodePath refNodePath){
            if(refNodePath != null && refNodePath != "") RefNode = GetNode<Node2D>(refNodePath);
        }

        public Vector2 GetRelPosition(){
            if(RefNode != null){
                return GlobalPosition-RefNode.GlobalPosition;
            } else return GlobalPosition;
        }

        public void SetRelPosition(Vector2 newPosition){
            if(RefNode != null){
                GlobalPosition = newPosition+RefNode.GlobalPosition;
            } else GlobalPosition = newPosition;
        }

        public float GetRelRotation(){
            if(RefNode != null){
                return GlobalRotation-RefNode.GlobalRotation;
            } else return GlobalRotation;
        }

        public void SetRelRotation(float newRotation){
            if(RefNode != null){
                GlobalRotation = newRotation+RefNode.GlobalRotation;
            } else GlobalRotation = newRotation;
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            GetRefNode(RefNodePath);
        }
    }

    /// <summary>
    /// Нод2D, сохраняющий изменения относительного положения и угла относительно другой ноды
    /// </summary>
    public class CoordSaver: RelativeCoords{

        public Vector2 OldPosition = Vector2.Zero;

        float OldRotation = 0;

        public override void _Process(float delta)
        {
            base._Process(delta);
            OldPosition = GetRelPosition();
            OldRotation = GetRelRotation();
        }
    }

}
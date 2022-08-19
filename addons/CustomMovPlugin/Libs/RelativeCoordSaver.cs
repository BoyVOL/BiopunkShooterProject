using Godot;
using System;

namespace CustomAnimations{

    /// <summary>
    /// Нод2D, сохраняющий изменения относительного положения и угла относительно другой ноды
    /// </summary>
    public class RelativeCoordSaver: Node2D{
        
        [Export]
        public NodePath RefNodePath = null;

        [Export]
        public bool UseGlobal = true;

        public Vector2 OldPosition = Vector2.Zero;

        float OldRotation = 0;

        protected Node2D RefNode = null;

        public void GetRefNode(NodePath refNodePath){
            if(refNodePath != null && refNodePath != "") RefNode = GetTree().Root.GetNode<Node2D>(refNodePath);
        }

        public Vector2 GetRelPosition(){
            if(RefNode != null){
                return GlobalPosition-RefNode.GlobalPosition;
            } else if(!UseGlobal){
                return Position;
            } else {
                return GlobalPosition;
            }
        }

        public float GetRelRotation(){
            if(RefNode != null){
                return GlobalRotation-RefNode.GlobalRotation;
            } else if(!UseGlobal){
                return Rotation;
            } else {
                return GlobalRotation;
            }
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            GetRefNode(RefNodePath);
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            OldPosition = GetRelPosition();
            OldRotation = GetRelRotation();
        }
    }

}
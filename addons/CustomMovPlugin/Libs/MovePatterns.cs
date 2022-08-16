using Godot;
using System;
using System.Collections.Generic;

namespace customMovement{

    /// <summary>
    /// Базовый класс для частей тел существ
    /// </summary>
    public class BodyPart: Node2D{
    }

    /// <summary>
    /// Базовый класс для частей тел существ с коллизией
    /// </summary>    
    public class CollisionBodyPart: BodyPart{
        [Export]
        NodePath CollisionShape = "";

        public CollisionObject2D CollShape = null;

        public void SetCollShape(){
            CollShape = GetNode<CollisionObject2D>(CollisionShape);
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            if(CollisionShape != "") SetCollShape();
        }
    }

    public class CustomBody: Node2D{

        /// <summary>
        /// Поле, отвечающее за сохранение предыдущего положения объекта
        /// </summary>
        public Vector2 OldPos;

        /// <summary>
        /// Поле, отвечающее за сохранение предыдущей трансформации объекта
        /// </summary>
        public Transform2D OldTransform;

        /// <summary>
        /// Метод для сохранения старой трансформации объекта
        /// </summary>
        public void SaveState(){
            OldPos = this.Position;
            OldTransform = this.Transform;
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            SaveState();            
        }
    }


    /// <summary>
    /// Класс, отображающий тело с сочлененными ногами
    /// </summary>
    public class CrabBody: CustomBody{
    }

    /// <summary>
    /// Класс, отображающий змеиный тип тела
    /// </summary>
    public class SnakeBody: CustomBody{
    }

    /// <summary>
    /// Класс, отображающий тело осминога
    /// </summary>
    public class OctopusBody: CustomBody{
    }
}
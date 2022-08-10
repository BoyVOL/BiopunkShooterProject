using Godot;
using System;
using System.Collections.Generic;

namespace customMovement{

    /// <summary>
    /// Базовый класс для частей тел существ
    /// </summary>
    class BodyPart: Node2D{

        /// <summary>
        /// Поле, содержащее графическую репрезентацию объекта
        /// </summary>
        public Sprite Sprite;

        /// <summary>
        /// Поле, отвечающее за сохранение предыдущего положения объекта
        /// </summary>
        public Vector2 OldPos;

        /// <summary>
        /// Поле, отвечающее за сохранение предыдущей трансформации объекта
        /// </summary>
        public Transform2D OldTransform;

        public BodyPart(Sprite graph){
            Sprite = graph;
            AddChild(Sprite);
        }
    }

    class SwarmKreep: BodyPart{

        /// <summary>
        /// Цель движения члена роя
        /// </summary>
        public Vector2 Target;

        /// <summary>
        /// Свойство, отвечающее за то, что член роя перестанет двигаться к его цели
        /// </summary>
        public float epsilon = 1;

        public float MoveSpeed = 0.1f;

        /// <summary>
        /// Метод, проверяющий, достаточно ли объект близко к своей цели
        /// </summary>
        /// <returns></returns>
        public bool NearTarget(){
            return Position.DistanceSquaredTo(Target) < epsilon;
        }

        public SwarmKreep(Sprite sprite):base(sprite){
            Sprite = sprite;
        }

        public override void _Process(float delta){
            base._Process(delta);
            if(!NearTarget()){
                Position += Position.DirectionTo(Target)*MoveSpeed;
            };
        }
    }

    /// <summary>
    /// Базовый класс для частей тел существ с коллизией
    /// </summary>    
    class CollisionBodyPart: BodyPart{

        /// <summary>
        /// Поле, содержащее объект коллизии тела
        /// </summary>
        public CollisionShape2D CollisionBody;

        public CollisionBodyPart(CollisionShape2D body, Sprite graph):base (graph){
            CollisionBody = body;
            AddChild(CollisionBody);
        }
    }

    class CustomBody: CollisionBodyPart{

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

        public CustomBody(CollisionShape2D body, Sprite graph):base (body,graph){

        }
    }

    /// <summary>
    /// Тело, состоящее из нескольких плывущих по воздуху объектов
    /// </summary>
    class SwarmBody: CustomBody{

        RandomNumberGenerator Randomiser = new RandomNumberGenerator();

        /// <summary>
        /// Список всех членов роя
        /// </summary>
        /// <typeparam name="SwarmMember"></typeparam>
        /// <returns></returns>
        public List<SwarmKreep> Kreeps = new List<SwarmKreep>();

        /// <summary>
        /// Свойство, отвечающее за то, как широко расходится рой относительно размера источника
        /// </summary>
        /// <returns></returns>
        public Vector2 SwarmScale = new Vector2(1,1);

        public SwarmBody(CollisionShape2D body, Sprite graph):base (body,graph){

        }

        public Vector2 GetNewTarget(){
            return new Vector2((Randomiser.Randf()-0.5f)*Scale.x*SwarmScale.x,(Randomiser.Randf()-0.5f)*Scale.y*SwarmScale.y);
        }

        public void AddKreep(SwarmKreep kreep){
            Kreeps.Add(kreep);
            kreep.Target = GetNewTarget();
            AddChild(kreep);
        }

        public void RemoveKreep(SwarmKreep kreep){
            Kreeps.Remove(kreep);
            RemoveChild(kreep);
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            foreach (SwarmKreep kreep in Kreeps)
            {
                if(kreep.NearTarget()) kreep.Target = GetNewTarget();
            }
        }
    }

    /// <summary>
    /// Класс, отображающий тело с сочлененными ногами
    /// </summary>
    class CrabBody: CustomBody{
        public CrabBody(CollisionShape2D body, Sprite graph):base (body,graph){
            
        }
    }

    /// <summary>
    /// Класс, отображающий змеиный тип тела
    /// </summary>
    class SnakeBody: CustomBody{
        public SnakeBody(CollisionShape2D body, Sprite graph):base (body,graph){
            
        }
    }

    /// <summary>
    /// Класс, отображающий тело осминога
    /// </summary>
    class OctopusBody: CustomBody{
        public OctopusBody(CollisionShape2D body, Sprite graph):base (body,graph){
            
        }
    }
}
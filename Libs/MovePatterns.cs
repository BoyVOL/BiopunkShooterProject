using Godot;
using System;
using System.Collections.Generic;

namespace customMovement{

    /// <summary>
    /// Базовый класс для частей тел существ
    /// </summary>
    public class BodyPart: Node2D{
    }

    public class SwarmCreep: BodyPart{

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

    public class CustomBody: RigidBody2D{

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
    /// Тело, состоящее из нескольких плывущих по воздуху объектов
    /// </summary>
    public class SwarmBody: CustomBody{

        RandomNumberGenerator Randomiser = new RandomNumberGenerator();

        /// <summary>
        /// Описывает, какие типы насекомышей используются в рое
        /// </summary>
        [Export]
        public PackedScene[] SwarmSamples = {};

        /// <summary>
        /// Описывает размер роя
        /// </summary>
        [Export]
        public int SwarmSize;

        /// <summary>
        /// Описывает, какую форму принимает рой
        /// </summary>
        [Export]
        public NodePath SwarmTemplate;

        public CircleShape2D SwarmShape = new CircleShape2D();

        /// <summary>
        /// Список всех членов роя
        /// </summary>
        /// <typeparam name="SwarmMember"></typeparam>
        /// <returns></returns>
        public List<SwarmCreep> Kreeps = new List<SwarmCreep>();

        public Vector2 GetNewTarget(){
            float r = SwarmShape.Radius * (float)Math.Sqrt(Randomiser.Randf());
            float theta = Randomiser.Randf() * 2 * (float)Math.PI;
            return new Vector2((float)Math.Sin(theta)*r,(float)Math.Cos(theta)*r);
        }

        public void AddKreep(SwarmCreep kreep){
            Kreeps.Add(kreep);
            kreep.Target = GetNewTarget();
            AddChild(kreep);
        }

        public void RemoveKreep(SwarmCreep kreep){
            Kreeps.Remove(kreep);
            RemoveChild(kreep);
        }

        public void LoadSwarm(){
            for (int i = 0; i < SwarmSize; i++)
            {
                int SampID = Randomiser.RandiRange(0,SwarmSamples.Length-1);
                AddKreep(SwarmSamples[SampID].Instance<SwarmCreep>());
            }
        }

        public void UnloadSwarm(){
            foreach(SwarmCreep kreep in Kreeps){
                RemoveKreep(kreep);
            }
        }

        public void SetSwarmShape(){
            CollisionShape2D Temp = GetNode<CollisionShape2D>(SwarmTemplate);
            SwarmShape = (CircleShape2D)Temp.Shape;
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            foreach (SwarmCreep kreep in Kreeps)
            {
                if(kreep.NearTarget()) kreep.Target = GetNewTarget();
            }
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            SetSwarmShape();
            LoadSwarm();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
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
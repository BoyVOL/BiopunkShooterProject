using Godot;
using System;
using customMovement;
using System.Collections.Generic;
    
namespace customMovement{
    
    /// <summary>
    /// Тело, состоящее из нескольких плывущих по воздуху объектов
    /// </summary>
    public class SwarmController: Node2D{

        RandomNumberGenerator Randomiser = new RandomNumberGenerator();

        [Export]
        /// <summary>
        /// Описывает, какие типы насекомышей используются в рое
        /// </summary>
        public PackedScene SwarmSample;

        [Export]
        /// <summary>
        /// Описывает количество юнитов в рое
        /// </summary>
        public int SwarmSize;

        [Export]
        /// <summary>
        /// Описывает размер роя
        /// </summary>
        public float SwarmRadius = 0;

        /// <summary>
        /// Список всех членов роя
        /// </summary>
        /// <typeparam name="SwarmMember"></typeparam>
        /// <returns></returns>
        public List<CanvasItem> Kreeps = new List<CanvasItem>();

        public Vector2 GetNewTarget(){
            float r = SwarmRadius * (float)Math.Sqrt(Randomiser.Randf());
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
                AddKreep(SwarmSample.Instance<SwarmCreep>());
            }
        }

        public void UnloadSwarm(){
            foreach(SwarmCreep kreep in Kreeps){
                RemoveKreep(kreep);
            }
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
            LoadSwarm();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
        }

    }

}
public class SwarmControllerNode : SwarmController
{
    

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

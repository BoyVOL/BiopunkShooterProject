using Godot;
using System;

namespace customMovement{

    /// <summary>
    /// Базовый класс для тел существ
    /// </summary>
    class Body: Node2D{
        public CollisionShape2D CollisionBody;

        public Body(CollisionShape2D Body){
            CollisionBody = Body;
        }
    }

    /// <summary>
    /// Тело, состоящее из нескольких плывущих по воздуху объектов
    /// </summary>
    class SwarmBody: Body{
        public SwarmBody(CollisionShape2D body):base (body){

        }
    }

    /// <summary>
    /// Класс, отображающий тело с сочлененными ногами
    /// </summary>
    class CrabBody: Body{
        public CrabBody(CollisionShape2D body):base (body){
            
        }
    }

    /// <summary>
    /// Класс, отображающий змеиный тип тела
    /// </summary>
    class SnakeBody: Body{
        public SnakeBody(CollisionShape2D body):base (body){
            
        }
    }

    /// <summary>
    /// Класс, отображающий тело осминога
    /// </summary>
    class OctopusBody: Body{
        public OctopusBody(CollisionShape2D body):base (body){
            
        }
    }
}
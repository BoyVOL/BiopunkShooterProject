using Godot;
using System;
using CustomAnimations;

namespace CustomAnimations
{
    public class VectorDampener{
        /// <summary>
        /// Предыдущий вход
        /// </summary>
        private Vector2 xp;

        /// <summary>
        /// Переменные состояния
        /// </summary>
        private Vector2 y = Vector2.Zero,yd = Vector2.Zero;

        /// <summary>
        /// Константы
        /// </summary>
        private float k1,k2,k3;

        /// <summary>
        /// Метод, задающий константы
        /// </summary>
        /// <param name="f">Натуральная частота системы, или скорость, с которой система изменяется</param>
        /// <param name="z">коэффициент сопротивления</param>
        /// <param name="r">начальный ответ системы</param>
        public void SetConstants(float f, float z, float r){
            k1 = z / ((float)Math.PI * f);
            k2 = 1 / ((2 * (float)Math.PI * f)*(2 * (float)Math.PI * f));
            k3 = r*z/(2 * (float)Math.PI * f);
            GD.Print(k1,k2,k3);
        }

        /// <summary>
        /// Метод, обновляющий состояние класса. 
        /// </summary>
        /// <param name="T">промежуток времени</param>
        /// <param name="x">целевая координата</param>
        /// <returns></returns>
        public Vector2 Update(float T, Vector2 x){
            Vector2 xd = (x-xp)/T;
            xp = x;
            float Stabk2 = Math.Max(k2,1.1f*(T*T/4+T*k1/2));//Вычисляем стабильный коэффициент, чтобы не привести к разбалансировки модели
            y=y+T*yd;
            yd = yd+T*(x+k3*xd-y-k1*yd)/Stabk2;
            return y;
        }
    }
}

public class SecondOrderNode : RelativeCoords
{

    /// <summary>
    /// Поле, хранящее ноду, которую в данный момент двигают анимации данной ноды
    /// </summary>

    [Export]
    public float f=1,z=1,r=1;

    [Export]
    public NodePath TargetNodePath;

    protected Node2D TargetNode;

    protected VectorDampener Dampener = new VectorDampener();

    /// <summary>
    /// Метод, задающий целевую ноду
    /// </summary>
    /// <param name="refNodePath"></param>    
    public void GetTargetNode(NodePath refNodePath){
            if(refNodePath != null && refNodePath != "") RefNode = GetNode<Node2D>(refNodePath);
        }

    public override void _EnterTree()
    {
        base._EnterTree();
        Dampener.SetConstants(f,z,r);
    }
    
    public override void _Process(float delta)
    {
        base._Process(delta);
        GlobalPosition = Dampener.Update(delta,RefNode.GlobalPosition);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

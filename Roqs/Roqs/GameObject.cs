using System;
using System.Numerics;
using System.Windows;
using System.Windows.Shapes;

namespace Roqs
{
    public class GameObject
    {
        private float xPos, yPos, width, height;
        private Rect collisionBox;
        private double timeSinceLastFall;
        private Vector2 velocity;
        private Rectangle sprite; 

        public float XPos { get => xPos; set => xPos = value; }
        public float YPos { get => yPos; set => yPos = value; }
        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }
        public double TimeSinceLastFall { get => timeSinceLastFall; set => timeSinceLastFall = value; }
        public Rect CollisionBox { get => collisionBox; set => collisionBox = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Rectangle Sprite { get => sprite; set => sprite = value; }

        public GameObject(float xPos, float yPos, float width, float height,  Rectangle sprite)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.CollisionBox = new Rect(XPos, YPos, Width, Height);
            this.TimeSinceLastFall = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            this.Velocity = new Vector2(0,0);
            this.Sprite = sprite; 
        }
        public GameObject(float xPos, float yPos, float width, float height)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.CollisionBox = new Rect(XPos, YPos, Width, Height);
            this.TimeSinceLastFall = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            this.Velocity = new Vector2(0, 0);
        }
        public GameObject(float xPos, float yPos)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.TimeSinceLastFall = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
        public void ActualiseCollisionBoxes()
        {
            this.CollisionBox = new Rect(XPos, YPos, Width, Height);
        }
        public void ApplyGravity()
        {
                
            double deltaTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - this.TimeSinceLastFall) / 1000f;
            this.Velocity += new Vector2(0,(float)(1 / 2f * Gravity.g * Constants.ratioPixelMeter / (1000f / 60f) * (deltaTime * deltaTime) / 2));
            

        }
        public void ResetGravity()
        {
            this.TimeSinceLastFall = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; 
        }
        public bool isColliding(GameObject other)
        {
            return this.CollisionBox.IntersectsWith(other.CollisionBox); 
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Roqs
{
    public class Player : Entity
    {
        private Controls controls;
        public bool isGrounded;
        public bool isJumping;
        public float initialYWhenJumping; 
        public const float speed = Constants.averageSpeedWalk * 1000f / 3600f / (1000f/60f) * Constants.ratioPixelMeter; // Average speed walk per ticks (60fps)

        public Player(float xPos, float yPos, float width, float height, float hp, float dammage, float defense, Controls controls, Rectangle sprite) : base(xPos, yPos,  width,  height, hp, dammage, defense,sprite)
        {
            this.Controls = controls;
        }

        public Controls Controls { get => controls; set => controls = value; }

        public void UpdatePosition()
        {
            this.XPos += this.Velocity.X;
            this.YPos += this.Velocity.Y;
            this.ActualiseCollisionBoxes(); 
        }
        public void UpdateVelocity()
        {
            this.Velocity = new Vector2(0, 0);
            
        }
        public void UpdateSprite()
        {
            Canvas.SetLeft(Sprite, this.XPos);
            Canvas.SetTop(Sprite, this.YPos);
        }
        public void Move()
        {
            if (this.Controls.left)
            {
                this.Velocity += new Vector2(-speed, 0); 
            }
            if (this.Controls.right)
            {
                this.Velocity += new Vector2(speed, 0);

            }
            if (this.Controls.up)
            {

            }
            if (this.Controls.down)
            {
                this.Velocity += new Vector2(0, speed);

            }        
        }
        public void ApplyJump()
        {
            double deltaTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - this.TimeSinceLastFall) / 1000f;
            this.Velocity = new Vector2(this.Velocity.X, -this.YPos + (float)(200*deltaTime*deltaTime  + initialYWhenJumping - 200*deltaTime)); 
        }

    }
}

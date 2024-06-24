using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Roqs
{
    public abstract class Entity : GameObject
    {
        private float defense;
        private float hp;
        private float dammage;
        public Entity(float xPos, float yPos, float width, float height, float hp, float dammage, float defense, Rectangle sprite) : base(xPos, yPos, width, height, sprite)
        {
            this.Hp = hp;
            this.Dammage = dammage;
            this.Defense = defense;
            this.Sprite.Width = this.Width;
            this.Sprite.Height = this.Height;
        }




        public float Hp { get => hp; set => hp = value; }
        public float Dammage { get => dammage; set => dammage = value; }
        public float Defense { get => defense; set => defense = value; }
    }
}

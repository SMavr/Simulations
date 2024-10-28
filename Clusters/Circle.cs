using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusters
{
    internal class Circle
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Color Color { get; set; }

        public Circle(Vector2 position, float radius, Color color)
        {
            Position = position;
            Radius = radius;
            Color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Use MonoGame.Extended's DrawCircle (if available) or your own method
            spriteBatch.DrawCircle(Position, Radius, 100, Color, 2f); // 100 segments for smoothness, 2f thickness
        }
    }
}

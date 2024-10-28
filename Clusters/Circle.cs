using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Clusters;

internal class Circle
{
    public Vector2 Position { get; set; }
    public Color Color { get; set; }

    private Random random = new Random(22);
    private int radius = 2;

    public Circle(Vector2 position, Color color)
    {
        Position = position;
        Color = color;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(Position, radius, 100, Color, 5); // 100 segments for smoothness, 2f thickness
    }

    public void Move(float deltaTime)
    {
        Position += new Vector2(random.Next(-100, 100) * deltaTime, random.Next(-100, -100) * deltaTime);

        if (Position.Y + radius * 2 < 0)
        {
            Position = new Vector2(Position.X, 600);
        }

    }
}

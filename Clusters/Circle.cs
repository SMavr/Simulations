using Clusters.Constants;
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
        Position += new Vector2(100 * deltaTime, 3 * deltaTime);

        // Check horizontal wrapping
        if (Position.X + radius * 2 < 0) // Past the left side
        {
            Position = new Vector2(WindowConstants.Width, Position.Y);
        }
        else if (Position.X > WindowConstants.Width) // Past the right side
        {
            Position = new Vector2(-radius * 2, Position.Y);
        }

        // Check vertical wrapping
        if (Position.Y + radius * 2 < 0) // Past the top
        {
            Position = new Vector2(Position.X, WindowConstants.Height);
        }
        else if (Position.Y > WindowConstants.Height) // Past the bottom
        {
            Position = new Vector2(Position.X, -radius * 2);
        }
    }
}

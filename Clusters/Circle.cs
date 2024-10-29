using Clusters.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Clusters;

internal class Circle
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Color Color { get; set; }

    private Random random = new ();
    private int radius = 2;
    private float detectionRadious = 200;
    private float attractionStrength = 800;

    public Circle(Vector2 position, Color color)
    {
        Position = position;
        Color = color;
        Velocity = Vector2.Zero;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(Position, radius, 100, Color, 5); // 100 segments for smoothness, 2f thickness
    }

    public void Move(float deltaTime)
    {
        Position += Velocity * deltaTime;

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

    public bool IsInVicinity(Circle otherCircle)
    {
        float distance = Vector2.Distance(Position, otherCircle.Position);
        return distance <= detectionRadious;
    }

    public bool IsOverlapping(Circle otherCircle)
    {
        float distance = Vector2.Distance(Position, otherCircle.Position);
        return distance < 2 * (radius + 1);
    }

    public Vector2 CalculateAttractiveForce(Circle circle)
    {
        Vector2 direction = circle.Position - Position; // Direction from A to B
        float distance = direction.Length();

        if (distance < 1f) distance = 1f; // Prevent excessive force for very close distances
        direction.Normalize();

        // Calculate force magnitude using inverse-square law
        float forceMagnitude = attractionStrength / (distance * distance);
        return direction * forceMagnitude; // Return force vector
    }

    public Vector2 CalculateRepulsiveForce(Circle circle)
    {
        Vector2 direction = Position - circle.Position; // Direction from B to A
        float distance = direction.Length();
        direction.Normalize();

        // Calculate repulsive force proportional to the overlap
        float overlap = (radius + radius) - distance;
        float forceMagnitude = attractionStrength * overlap;
        return direction * overlap;
    }
}

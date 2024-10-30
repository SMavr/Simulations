using Clusters.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using System;
using static System.Collections.Specialized.BitVector32;

namespace Clusters;

internal class Circle
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Color Color { get; }
    public int Team { get; }

    private Random random = new ();
    private int radius = 2;
    private float detectionRadious = 400;
    private float attractionStrength = 200;
    public Circle(Vector2 position, Color color, int team = 0)
    {
        Position = position;
        Color = color;
        Velocity = Vector2.Zero;
        Team = team;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(Position, radius, 100, Color, 5); // 100 segments for smoothness, 2f thickness
    }

    public void Move(float deltaTime)
    {
        RestrictVelocity();

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

    private void RestrictVelocity()
    {
        int velocityLimit = 50;

        if (Velocity.X > velocityLimit)
            Velocity = new Vector2(velocityLimit, Velocity.Y);

        if (Velocity.X < -velocityLimit)
            Velocity = new Vector2(-velocityLimit, Velocity.Y);

        if (Velocity.Y > velocityLimit)
            Velocity = new Vector2(Velocity.X, velocityLimit);

        if (Velocity.Y < -velocityLimit)
            Velocity = new Vector2(Velocity.X, -velocityLimit);
    }

    public bool IsInVicinity(Circle otherCircle)
    {
        float distance = Vector2.Distance(Position, otherCircle.Position);
        return distance <= detectionRadious;
    }

    public bool IsOverlapping(Circle otherCircle)
    {
        float distance = Vector2.Distance(Position, otherCircle.Position);
        return distance < 2 * radius + 40;
    }

    public Vector2 CalculateAttractiveForce(Circle circle)
    {
        Vector2 direction = circle.Position - Position; // Direction from A to B
        float distance = direction.Length();

        //if (distance < 2 * radius + 4) // Prevent excessive force for very close distances
        //    return -direction;
        //direction.Normalize();

        // Calculate force magnitude using inverse-square law
        float forceMagnitude = attractionStrength / (distance * distance);

        var forceVector = direction * forceMagnitude;

        if (Team == circle.Team)
            return 8 * forceVector;
        else
            return -forceVector;
    }

    public void ResolveOverlap(Circle circleB)
    {
        Vector2 direction = circleB.Position - Position;
        float distance = direction.Length();
        float minDistance = 2 * radius;

        if (distance < minDistance)
        {
            // Calculate overlap amount
            float overlap = minDistance - distance;

            // Normalize direction and push circles apart by half the overlap distance
            direction.Normalize();
            Position -= direction * (overlap / 2);
            circleB.Position += direction * (overlap / 2);

            // Adjust velocities to separate them slightly (optional)
            //circleA.Velocity -= direction * 0.1f;
            //circleB.Velocity += direction * 0.1f;
        }
    }
}

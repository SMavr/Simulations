using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RomeVsOrcs.Textures;

internal class Blood
{
    private Random random = new Random();

    private List<BloodParticle> particles;

    private void SpawnBlood(Vector2 position, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Generate random velocity for the particles
            Vector2 velocity = new Vector2(
            (float)(random.NextDouble() * 2 - 1), // Random X velocity
                (float)(random.NextDouble() * 2 - 1)  // Random Y velocity
            ) * 5f;

            // Random lifespan between 0.5 to 1.5 seconds
            float lifespan = (float)(random.NextDouble() * 1.0 + 0.5);

            // Random size for the circle particles
            float size = (float)(random.NextDouble() * 0.3 + 0.2);

            // Create the particle and add it to the list
            particles.Add(new BloodParticle(position, velocity, lifespan, size, Color.Red));
        }
    }
}

internal class BloodParticle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public float Lifespan;
    public float Size;
    public Color Color;
   

    public BloodParticle(Vector2 position, Vector2 velocity, float lifespan, float size, Color color)
    {
        Position = position;
        Velocity = velocity;
        Lifespan = lifespan;
        Size = size;
        Color = color;
    }

    public void Update(GameTime gameTime)
    {
        Position += Velocity;
        Lifespan -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

   
}



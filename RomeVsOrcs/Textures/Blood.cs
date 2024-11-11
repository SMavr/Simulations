using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RomeVsOrcs.Textures;

internal class Blood(ContentManager content)
{
    private Random random = new Random();

    private List<BloodParticle> particles;

    private Texture2D bloodTexture;

    public void Load()
    {
        bloodTexture = content.Load<Texture2D>("blood"); // Load your blood texture here
        particles = new List<BloodParticle>();
    }

    public void Update(float elapsed)
    {
        for (int i = particles.Count - 1; i >= 0; i--)
        {
            particles[i].Update(elapsed);
            if (particles[i].Lifespan <= 0)
            {
                particles.RemoveAt(i);
            }
        }
    }

    public void SpawnBlood(Vector2 position, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Generate random velocity for the particles
            Vector2 velocity = new Vector2(
            (float)(random.NextDouble() * 2 - 1), // Random X velocity
                (float)(random.NextDouble() * 2 - 1)  // Random Y velocity
            ) * 3f;

            // Random size for the circle particles
            float size = (float)(random.NextDouble() * 0.3 + 0.2);

            // Create the particle and add it to the list
            particles.Add(new BloodParticle(position, velocity, 1f, size, Color.Red));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var particle in particles)
        {
            spriteBatch.Draw(
                bloodTexture,
                particle.Position,
                null,
                particle.Color,
                0f,
                new Vector2(bloodTexture.Width / 2, bloodTexture.Height / 2), // Center the circle
                particle.Size,
                SpriteEffects.None,
                0f
            );
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

    public void Update(float elapsed)
    {
        Position += Velocity;
        Lifespan -= elapsed;
    }
}



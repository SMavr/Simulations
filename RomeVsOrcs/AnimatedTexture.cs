﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography;


namespace RomeVsOrcs;
// <summary>
/// A helper class for handling animated textures.
/// </summary>
public class AnimatedTexture
{
    // Number of frames in the animation.
    private int frameCount;

    // The animation spritesheet.
    private Texture2D texture;

    // The number of frames to draw per second.
    private float timePerFrame;

    // The current frame being drawn.
    private int frame;

    // Total amount of time the animation has been running.
    private float totalElapsed;

    // Is the animation currently running?
    private bool isPaused;

    private string asset;

    // The current rotation, scale and draw depth for the animation.
    public float Rotation, Scale, Depth;

    public AnimatedTexture(float rotation, float scale, float depth)
    {
        this.Rotation = rotation;
        this.Scale = scale;
        this.Depth = depth;
    }

    public void Load(ContentManager content, string asset, int frameCount, int framesPerSec)
    {
        if (this.asset == asset)
            return;

        this.frameCount = frameCount;
        texture = content.Load<Texture2D>(asset);
        timePerFrame = (float)1 / framesPerSec;
        frame = 0;
        totalElapsed = 0;
        isPaused = false;
        this.asset = asset;
    }

    public void UpdateFrame(float elapsed)
    {
        if (isPaused)
            return;
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame)
        {
            frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            frame %= frameCount;
            totalElapsed -= timePerFrame;
        }
    }

    public void DrawFrame(SpriteBatch batch, Vector2 screenPos, int row, int frameCount)
    {
        this.frameCount = frameCount;
        DrawFrame(batch, frame, screenPos, row);
    }

    protected int frameWidth = 64;

    public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos, int row)
    {

        if (row <= 51)
            DrawStartFromUp();
        else
            DrawStartFromDown();

        void DrawStartFromDown()
        {
            var oppositeRow = 64 - row;
            Rectangle sourcerect = new Rectangle(128 * frame, texture.Height - (oppositeRow * 128), 128, 128);
            var origin = new Vector2(32, 32);

            batch.Draw(texture, screenPos, sourcerect, Color.White,
                Rotation, origin, Scale, SpriteEffects.None, Depth);
        }

        void DrawStartFromUp()
        {
            Rectangle sourcerect = new Rectangle(frameWidth * frame, row * 64, frameWidth, 64);
            var origin = Vector2.Zero;
            batch.Draw(texture, screenPos, sourcerect, Color.White,
               Rotation, origin, Scale, SpriteEffects.None, Depth);
        }
    }

   

    public bool IsPaused
    {
        get { return isPaused; }
    }

    public void Reset()
    {
        frame = 0;
        totalElapsed = 0f;
    }

    public void Stop()
    {
        Pause();
        Reset();
    }

    public void Play()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
    }
}

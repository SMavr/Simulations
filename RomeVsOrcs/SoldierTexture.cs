﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RomeVsOrcs;
internal class SoldierTexture : AnimatedTexture
{

    // How many frames/images are included in the animation
    private int frames = 9;
    // How many frames should be drawn each second, how fast does the animation run?
    private const int framesPerSec = 10;

    // The position to draw the character
    private Vector2 position;

    private int currentRow = 10;

    public SoldierTexture() : base(Vector2.Zero, 0, 1f, 0.5f)
    {
    }

    public void Load(ContentManager content, Vector2 initialPosition)
    {
        position = initialPosition;
        base.Load(content, "soldier", frames, framesPerSec);
    }

    public void Update(float elapsed)
    {
        this.UpdateFrame(elapsed);
        this.Pause();

        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Right))
        {
            Play();
            position.X += 2;
            currentRow = 11;
            frames = 9;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            Play();
            position.X -= 2;
            currentRow = 9;
            frames = 9;
        }

        if (state.IsKeyDown(Keys.Up))
        {
            Play();
            position.Y -= 2;
            currentRow = 8;
            frames = 9;
        }
        if (state.IsKeyDown(Keys.Down))
        {
            Play();
            position.Y += 2;
            currentRow = 10;
            frames = 9;
        }
    }

    public void DrawFrame(SpriteBatch spriteBatch)
    {
        base.DrawFrame(spriteBatch, position, currentRow, frames);
    }
}
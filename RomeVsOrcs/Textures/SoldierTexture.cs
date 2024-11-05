using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RomeVsOrcs.Enums;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RomeVsOrcs.Textures;
internal class SoldierTexture(ContentManager content, Viewport viewport) : AnimatedTexture(content, viewport)
{
    private Direction direction = Direction.South;

    private bool spacePressed = false;

    private Texture2D bubble;

    public override void Load(Vector2 initialPosition)
    {
        Load( "soldier", initialPosition);
        bubble = content.Load<Texture2D>("speech-bubble");
    }

    public void Update(float elapsed, List<OrcTexture> objects)
    {
        Pause();

        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
        {
            Play();
            position.X += 2;
            currentRow = 11;
            frameCount = 9;
            direction = Direction.East;
        }
        if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
        {
            Play();
            position.X -= 2;
            currentRow = 9;
            frameCount = 9;
            direction = Direction.West;
        }

        if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
        {
            Play();
            position.Y -= 2;
            currentRow = 8;
            frameCount = 9;
            direction = Direction.North;
        }
        if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
        {
            Play();
            position.Y += 2;
            currentRow = 10;
            frameCount = 9;
            direction = Direction.South;
        }
        if (state.IsKeyDown(Keys.Space))
        {
            Play();
            currentRow = ToRow();
            frameCount = 6;
            spacePressed = true;
            foreach (var item in objects)
            {
                if (this.Rectangle.Intersects(item.Rectangle))
                {
                    item.Hit();
                }
            }
        }
        if (spacePressed && state.IsKeyUp(Keys.Space))
        {
            Reset();
            spacePressed = false;
        }
        foreach (var item in objects)
        {
            if(this.Rectangle.Intersects(item.Rectangle))
            {
                ResolveCollision(item.Rectangle);
            }
        }

        if (showBubble)
        {
            overlayTimer += elapsed;

            // Hide overlay after the specified duration
            if (overlayTimer >= 5f)
            {
                showBubble = false;
            }
        }

        UpdateFrame(elapsed);
    }

    private int ToRow() => direction switch
    {
        Direction.East => 55,
        Direction.North => 52,
        Direction.South => 54,
        Direction.West => 53,
        _ => 54
    };

    private void ResolveCollision(Rectangle otherBounds)
    {
        float overlapX = Math.Min(Rectangle.Right, otherBounds.Right) - Math.Max(Rectangle.Left, otherBounds.Left);
        float overlapY = Math.Min(Rectangle.Bottom, otherBounds.Bottom) - Math.Max(Rectangle.Top, otherBounds.Top);

        if (Math.Abs(overlapX) < Math.Abs(overlapY))
        {
            // [100 150][149 199] position X = +1
            if (Rectangle.Right > otherBounds.Right)
                position.X += overlapX;
            //  [101 151] [150 200] position X =-1
            else
                position.X -= overlapX;
        }
        else
        {
            if (Rectangle.Top > otherBounds.Top)
                position.Y += overlapY;
            else
                position.Y -= overlapY;
        }
    }

    private float overlayTimer = 0f;
    bool showBubble = true;

    public override void DrawFrame(SpriteBatch spriteBatch)
    {
        base.DrawFrame(spriteBatch);

        if(showBubble)
        {
            spriteBatch.Draw(bubble, position + new Vector2(-32, -32), null,
                Color.White, 0, Vector2.Zero, 0.3f, SpriteEffects.None, 1);
        }
    }
}

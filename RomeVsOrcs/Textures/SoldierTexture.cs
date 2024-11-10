using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RomeVsOrcs.Enums;
using System;
using System.Collections.Generic;

namespace RomeVsOrcs.Textures;
internal class SoldierTexture(ContentManager content, Viewport viewport) : AnimatedTexture(content, viewport)
{
    private Direction direction = Direction.South;

    private bool spacePressed = false;

    private BubbleTexture bubble = new BubbleTexture(content);

    public override void Load(Vector2 initialPosition)
    {
        Load( "soldier", initialPosition);
        bubble.Load("speech-bubble");
    }

    public void Update(float elapsed, List<OrcTexture> objects)
    {
        Pause();

        KeyboardState state = Keyboard.GetState();

        // Determine if the Shift key is held down
        bool isSprinting = state.IsKeyDown(Keys.LeftShift);

        // Set the current speed based on whether the Shift key is held
        int normalSpeed = 2;
        int sprintSpeed = 4;
        int currentSpeed = isSprinting ? sprintSpeed : normalSpeed;

        if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
        {
            Play();
            position.X += currentSpeed;
            currentRow = 11;
            frameCount = 9;
            direction = Direction.East;
        }
        if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
        {
            Play();
            position.X -= currentSpeed;
            currentRow = 9;
            frameCount = 9;
            direction = Direction.West;
        }

        if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
        {
            Play();
            position.Y -= currentSpeed;
            currentRow = 8;
            frameCount = 9;
            direction = Direction.North;
        }
        if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
        {
            Play();
            position.Y += currentSpeed;
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

        bubble.Update(elapsed);

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

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        bubble.DrawFrame(spriteBatch, position);
    }
}

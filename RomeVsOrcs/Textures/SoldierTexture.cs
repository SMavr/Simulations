using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RomeVsOrcs.Enums;
using System;
using System.Collections.Generic;

namespace RomeVsOrcs.Textures;
internal class SoldierTexture : AnimatedTexture
{
    private Direction direction = Direction.South;

    private bool spacePressed = false;

    public override void Load(ContentManager content, Vector2 initialPosition)
    {
        Load(content, "soldier", initialPosition);
    }

    public void Update(float elapsed, List<Rectangle> objects)
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
        }
        if (spacePressed && state.IsKeyUp(Keys.Space))
        {
            Reset();
            spacePressed = false;
        }
        foreach (var item in objects)
        {
            if(this.Rectangle.Intersects(item))
            {
                ResolveCollision(item);
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
        // Calculate overlap amounts in each direction
        float overlapX = Math.Min(Rectangle.Right, otherBounds.Right) - Math.Max(Rectangle.Left, otherBounds.Left);
        float overlapY = Math.Min(Rectangle.Bottom, otherBounds.Bottom) - Math.Max(Rectangle.Top, otherBounds.Top);

        List<int> collisionX = [Rectangle.Right, Rectangle.Left, otherBounds.Right, otherBounds.Left];
        collisionX.Sort();
        if (Rectangle.Right > otherBounds.Right)
            position.X += otherBounds.Right - Rectangle.Left;
        else
            position.X += otherBounds.Left - Rectangle.Right;

        //100 150 149 200
        //101 151 150 200

        //if (overlapX < 50 && overlapY < 50)
        //    return;

        ////position.X += overlapX;
        ////position.Y += overlapY;

        ////// Correct the position based on the minimum overlap
        //if (Math.Abs(overlapX) < Math.Abs(overlapY))
        //{
        //    // Resolve collision on X axis
        //    position.X += overlapX;
        //}
        //else
        //{
        //    // Resolve collision on Y axis
        //    position.Y += overlapY;
        //}
    }
}

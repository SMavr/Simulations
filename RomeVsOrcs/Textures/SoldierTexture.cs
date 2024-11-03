using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RomeVsOrcs.Enums;
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
                position.X = 0;
                position.Y = 0;
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
}

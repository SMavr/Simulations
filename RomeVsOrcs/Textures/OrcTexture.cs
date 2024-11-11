using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RomeVsOrcs.Enums;
using System;

namespace RomeVsOrcs.Textures;
internal class OrcTexture(ContentManager content, Viewport viewport) : AnimatedTexture(content, viewport)
{

    Random random = new Random();
    int randomNumber = 1;
    float duration;
    //private BubbleTexture bubble = new BubbleTexture(content, true);

    public override void Load(Vector2 initialPosition)
    {
        Load("orc", initialPosition);
        //bubble.Load("arghhh-bubble");
    }

    public void Update(float elapsed)
    {
        duration += elapsed;

        if(duration > 1)
        {
            Pause();
            randomNumber = random.Next(1, 10);
            duration = 0;
        }

        if (randomNumber == 1)
        {
            Play();
            position.X += 1;
            currentRow = 11;
            frameCount = 9;
        }
        if (randomNumber == 2)
        {
            Play();
            position.X -= 1;
            currentRow = 9;
            frameCount = 9;
        }
        if (randomNumber == 3)
        {
            Play();
            position.Y -= 1;
            currentRow = 8;
            frameCount = 9;
        }
        if (randomNumber == 4)
        {
            Play();
            position.Y += 1;
            currentRow = 10;
            frameCount = 9;
        }

        //bubble.Update(elapsed);

        UpdateFrame(elapsed);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        //bubble.DrawFrame(spriteBatch, position, new Vector2(48, 0));
    }
}

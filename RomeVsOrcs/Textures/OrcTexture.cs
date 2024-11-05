using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RomeVsOrcs.Enums;
using System;

namespace RomeVsOrcs.Textures;
internal class OrcTexture : AnimatedTexture
{

    Random random = new Random();
    int randomNumber = 1;
    float duration;
    int life = 3;

    public override void Load(ContentManager content, Vector2 initialPosition)
    {
        Load(content, "orc", initialPosition);
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

        UpdateFrame(elapsed);
    }
}

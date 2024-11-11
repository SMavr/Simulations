using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RomeVsOrcs.Textures;
internal class Target(ContentManager content, Texture2D texture)
{
    public int Life { get; set; } = 3;
    public bool IsDead { get; private set; } = false;

    private float overlayTimer = 0f;

    private Blood blood;

    public void Load()
    {
        blood = new Blood(content);
        blood.Load();
    }

    public void Update(float deltaTime)
    {
        blood.Update(deltaTime);
    }

    public void Draw(SpriteBatch batch)
    {
        blood.Draw(batch);
    }

    public void Hit(float elapsed, Vector2 position)
    {
        overlayTimer += elapsed;
        if (overlayTimer < 0.1f)
        {
            return;
        }

        overlayTimer = 0f;
        blood.SpawnBlood(position, 5);

        if (Life != 0)
        {
            Life--;
        }
        else
        {
            IsDead = true;
            Constants.NumberOfKills++;
            if (texture != null)
            {
                texture = null;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RomeVsOrcs.Textures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RomeVsOrcs;
internal class OrcFactory(ContentManager content, Viewport viewport)
{
    public OrcList OrcTextures { get; private set; } = [];

    public void Load(int count)
    {
        for (int i = 0; i < count; i++)
        {
            OrcTexture orcTexture = new (content, viewport);
            orcTexture.Load(new Vector2(100 + (i * 200), 150));
            OrcTextures.Add(orcTexture);
        }
    }

    public void Update(float elapsedTime)
    {
        OrcTextures.ForEach(it => it.Update(elapsedTime));
        if (OrcTextures.Count == 0)
            this.Load(4);
    }

    public void Draw(SpriteBatch spriteBatch) 
    {
        foreach(var texture in OrcTextures.Where(it => !it.IsDead()))
        {
            texture.Draw(spriteBatch);
        }

        OrcTextures.RemoveAll(it => it.IsDead());
    }
}

internal class OrcList : List<OrcTexture>
{
    public bool Intersects(Rectangle rectangle, out OrcTexture orcTexture)
    {
        foreach (var item in this)
        {
            if (rectangle.Intersects(item.Rectangle))
            {
                orcTexture = item;
                return true;
            }

        }

        orcTexture = null;
        return false;
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RomeVsOrcs.Textures;
using System.Collections.Generic;
using System.Linq;

namespace RomeVsOrcs;
internal class OrcFactory
{
    public List<OrcTexture> OrcTextures { get; private set; } = [];

    public void Load(ContentManager content, int count)
    {
        for (int i = 0; i < count; i++)
        {
            OrcTexture orcTexture = new OrcTexture();
            orcTexture.Load(content, new Vector2(100 + (i * 200), 150));
            OrcTextures.Add(orcTexture);
        }
    }

    public void Update(float elapsedTime)
    {
        OrcTextures.ForEach(it => it.Update(elapsedTime));
    }

    public void Draw(SpriteBatch spriteBatch) 
    {
        OrcTextures.Where(it => !it.IsDead)
            .ToList()
            .ForEach(it => it.DrawFrame(spriteBatch));

        OrcTextures.RemoveAll(it => it.IsDead);
    }
}

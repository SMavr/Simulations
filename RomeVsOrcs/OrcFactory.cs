using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RomeVsOrcs.Textures;
using System.Collections.Generic;
using System.Linq;

namespace RomeVsOrcs;
internal class OrcFactory
{
    private List<OrcTexture> orcTextures = new();

    public List<Rectangle> OrcShapes => orcTextures.Select(it => it.Rectangle).ToList();

    public void Load(ContentManager content, int count)
    {
        for (int i = 0; i < count; i++)
        {
            OrcTexture orcTexture = new OrcTexture();
            orcTexture.Load(content, new Vector2(100 + (i * 200), 150));
            orcTextures.Add(orcTexture);
        }
    }

    public void Update(float elapsedTime)
    {
        orcTextures.ForEach(it => it.Update(elapsedTime));
    }

    public void Draw(SpriteBatch spriteBatch) 
    {
        orcTextures.ForEach(it => it.DrawFrame(spriteBatch));
    }
}

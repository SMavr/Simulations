using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RomeVsOrcs.Textures;

namespace RomeVsOrcs;
internal class OrcFactory
{
    private OrcTexture orcTexture = new();

    public Rectangle OrcShapes => orcTexture.Rectangle;


    public void Load(ContentManager content)
    {
        orcTexture.Load(content, new Vector2(150, 150));
    }


    public void Update(float elapsedTime)
    {
        orcTexture.Update(elapsedTime);
    }

    public void Draw(SpriteBatch spriteBatch) 
    {
        orcTexture.DrawFrame(spriteBatch);
    }
}

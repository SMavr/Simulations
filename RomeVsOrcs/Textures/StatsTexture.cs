using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RomeVsOrcs.Textures;
internal class StatsTexture(ContentManager content)
{
    private SpriteFont counterFont;

    public void Load()
    {
        counterFont = content.Load<SpriteFont>("Stats");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(counterFont, $"Kills: {Constants.NumberOfKills}", new Vector2(10, 10), Color.White);
    }
}

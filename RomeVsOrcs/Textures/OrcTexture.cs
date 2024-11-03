using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace RomeVsOrcs.Textures;
internal class OrcTexture : AnimatedTexture
{
    public override void Load(ContentManager content, Vector2 initialPosition)
    {
        Load(content, "orc", initialPosition);
    }
}

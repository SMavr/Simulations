using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace RomeVsOrcs;
internal class SheetAnimatedTexture
{
    Texture2D spriteSheet;
    int frameWidth = 64;  // Width of a single frame
    int frameHeight = 64; // Height of a single frame
    int currentFrame = 0; // Current frame index
    double timePerFrame = 0.1; // Time each frame is displayed in seconds
    double timeElapsed = 0; // Time elapsed for frame switching

    public void Load(ContentManager content, string asset)
    {
        spriteSheet = content.Load<Texture2D>(asset);
    }

    Rectangle GetFrameRectangle(int frameIndex)
    {
        int framesPerRow = spriteSheet.Width / frameWidth;
        int row = frameIndex / framesPerRow;
        int column = frameIndex % framesPerRow;
        return new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
    }

    public void UpdateFrame(GameTime gameTime)
    {
        timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        // Move to the next frame if enough time has elapsed
        if (timeElapsed >= timePerFrame)
        {
            currentFrame++;
            timeElapsed -= timePerFrame;

            // Loop back to the first frame if at the end of the animation
            if (currentFrame * frameWidth >= spriteSheet.Width * spriteSheet.Height / frameHeight)
                currentFrame = 0;
        }
    }
}

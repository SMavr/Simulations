using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing.Imaging;
using System.Security.Cryptography;


namespace RomeVsOrcs;
// <summary>
/// A helper class for handling animated textures.
/// </summary>
public class AnimatedTexture
{
    // Number of frames in the animation.
    protected int frameCount;

    // The animation spritesheet.
    private Texture2D texture;

    // The number of frames to draw per second.
    private float timePerFrame;

    // The current frame being drawn.
    private int frame;

    // Total amount of time the animation has been running.
    private float totalElapsed;

    // How many frames should be drawn each second, how fast does the animation run?
    private const int framesPerSec = 10;



    // The current rotation, scale and draw depth for the animation.
    public float Rotation, Scale, Depth;

    public AnimatedTexture(float rotation, float scale, float depth)
    {
        this.Rotation = rotation;
        this.Scale = scale;
        this.Depth = depth;
    }

    // Is the animation currently running?
    public bool IsPaused { get; private set; }

    public void Load(ContentManager content, string asset)
    {
        texture = content.Load<Texture2D>(asset);
        timePerFrame = (float)1 / framesPerSec;
        frame = 0;
        totalElapsed = 0;
        IsPaused = false;
    }

    public void UpdateFrame(float elapsed)
    {
        if (IsPaused)
            return;
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame)
        {
            frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            frame %= frameCount;
            totalElapsed -= timePerFrame;
        }
    }

    public void DrawFrame(SpriteBatch batch, Vector2 screenPos, int row)
    {
        DrawFrame(batch, frame, screenPos, row);
    }

    public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos, int row)
    {
        if (row <= 51)
            DrawStartFromUp();
        else
            DrawStartFromDown();

        void DrawStartFromDown()
        {
            int frameDimension = 128;
            var oppositeRow = 64 - row;
            Rectangle sourcerect = new Rectangle(frameDimension * frame, 
                texture.Height - (oppositeRow * frameDimension), frameDimension, frameDimension);
            var origin = new Vector2(32, 32);

            batch.Draw(texture, screenPos, sourcerect, Color.White,
                Rotation, origin, Scale, SpriteEffects.None, Depth);
        }

        void DrawStartFromUp()
        {
            int frameDimension = 64;
            Rectangle sourcerect = new Rectangle(frameDimension * frame, row * frameDimension, frameDimension, frameDimension);
            var origin = Vector2.Zero;
            batch.Draw(texture, screenPos, sourcerect, Color.White,
               Rotation, origin, Scale, SpriteEffects.None, Depth);
        }
    }




    public void Reset()
    {
        frame = 0;
        totalElapsed = 0f;
    }

    public void Stop()
    {
        Pause();
        Reset();
    }

    public void Play()
    {
        IsPaused = false;
    }

    public void Pause()
    {
        IsPaused = true;
    }
}

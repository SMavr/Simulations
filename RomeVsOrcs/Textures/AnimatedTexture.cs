using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RomeVsOrcs.Textures;

public abstract class AnimatedTexture(ContentManager content, Viewport viewport) : Playable
{
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

    // The position to draw the character
    protected Vector2 position;

    // Number of frames in the animation.
    protected int frameCount;

    // Current row in animation sprite sheet.
    protected int currentRow = 10;

    public Rectangle Rectangle => new((int)position.X, (int)position.Y, 32, 32);

    private Target target;

    public abstract void Load(Vector2 initialPosition);

    public void Load(string asset, Vector2 initialPosition)
    {
        position = initialPosition;
        texture = content.Load<Texture2D>(asset);
        timePerFrame = (float)1 / framesPerSec;
        frame = 0;
        totalElapsed = 0;

        target = new Target(content, texture);
        target.Load();
        base.Pause();
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

        target.Update(elapsed);

        PreventMoveOutsideWindow();
    }

    private void PreventMoveOutsideWindow()
    {
        position.X = MathHelper.Clamp(position.X, 0, viewport.Width - 64);
        position.Y = MathHelper.Clamp(position.Y, 0, viewport.Height - 64);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        DrawFrame(spriteBatch, frame, position, currentRow);
    }

    private void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos, int row)
    {
        target.Draw(batch);
        if (row <= 51)
            DrawStartFromUp();
        else
            DrawStartFromDown();


        void DrawStartFromDown()
        {
            int frameDimension = 128;
            var oppositeRow = 64 - row;
            Rectangle sourcerect = new Rectangle(frameDimension * frame,
                texture.Height - oppositeRow * frameDimension, frameDimension, frameDimension);
            var origin = new Vector2(32, 32);

            batch.Draw(texture, screenPos, sourcerect, Color.White,
                0, origin, 1f, SpriteEffects.None, 0.5f);
        }

        void DrawStartFromUp()
        {
            int frameDimension = 64;
            Rectangle sourcerect = new Rectangle(frameDimension * frame, row * frameDimension, frameDimension, frameDimension);
            var origin = Vector2.Zero;
            batch.Draw(texture, screenPos, sourcerect, Color.White,
               0, origin, 1f, SpriteEffects.None, 0.5f);
        }
    }

    protected override void Reset()
    {
        frame = 0;
        totalElapsed = 0f;
    }

    public void Hit(float elapsed) => target.Hit(elapsed, position);

    public bool IsDead() => target.IsDead;
}

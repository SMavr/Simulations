using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace RomeVsOrcs;

public class MainGame : Game
{
    //private Texture2D romanSoldierTexture;
    //private Vector2 romanSoldierPosition;
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    // The reference to the AnimatedTexture for the character
    private AnimatedTexture spriteTexture;
    // The rotation of the character on screen
    private const float rotation = 0;
    // The scale of the character, how big it is drawn
    private const float scale = 1f;
    // The draw order of the sprite
    private const float depth = 0.5f;

    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        spriteTexture = new AnimatedTexture(Vector2.Zero, rotation, scale, depth);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    // The game visible area
    private Viewport viewport;
    // The position to draw the character
    private Vector2 characterPos;
    // How many frames/images are included in the animation
    private const int frames = 9;
    // How many frames should be drawn each second, how fast does the animation run?
    private const int framesPerSec = 10;


    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);


        // "AnimatedCharacter" is the name of the sprite asset in the project.
        spriteTexture.Load(Content, "SoldierAnimation_Right", frames, framesPerSec);
        viewport = graphics.GraphicsDevice.Viewport;
        characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);


    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        spriteTexture.UpdateFrame(elapsed);

        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Right))
        {
            characterPos.X += 2;
            spriteTexture.Load(Content, "SoldierAnimation_Right", frames, framesPerSec);
        }
        if (state.IsKeyDown(Keys.Left))
        {
            characterPos.X -= 2;
            spriteTexture.Load(Content, "SoldierAnimation_Left", frames, framesPerSec);
        }
        if (state.IsKeyDown(Keys.Up))
        {
            characterPos.Y -= 2;
            spriteTexture.Load(Content, "SoldierAnimation_Back", frames, framesPerSec);
        }
        if (state.IsKeyDown(Keys.Down))
        {
            characterPos.Y += 2;
            spriteTexture.Load(Content, "SoldierAnimation_Front", frames, framesPerSec);
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);

        spriteBatch.Begin();
        spriteTexture.DrawFrame(spriteBatch, characterPos);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}

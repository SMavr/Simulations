using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace RomeVsOrcs;

public class MainGame : Game
{
    //private Texture2D romanSoldierTexture;
    //private Vector2 romanSoldierPosition;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // The reference to the AnimatedTexture for the character
    private AnimatedTexture spriteTexture;
    // The rotation of the character on screen
    private const float rotation = 0;
    // The scale of the character, how big it is drawn
    private const float scale = 0.5f;
    // The draw order of the sprite
    private const float depth = 0.5f;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
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
    private const int frames = 8;
    // How many frames should be drawn each second, how fast does the animation run?
    private const int framesPerSec = 10;


    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        // "AnimatedCharacter" is the name of the sprite asset in the project.
        spriteTexture.Load(Content, "AnimatedCharacter", frames, framesPerSec);
        viewport = _graphics.GraphicsDevice.Viewport;
        characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);

        //romanSoldierTexture = Content.Load<Texture2D>("soldier2");
        //romanSoldierPosition = Vector2.Zero;

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        spriteTexture.UpdateFrame(elapsed);

        //KeyboardState state = Keyboard.GetState();
        //if (state.IsKeyDown(Keys.Right))
        //{
        //    romanSoldierPosition.X += 2;
        //}
        //if (state.IsKeyDown(Keys.Left))
        //{
        //    romanSoldierPosition.X -= 2;
        //}
        //if (state.IsKeyDown(Keys.Up))
        //{
        //    romanSoldierPosition.Y -= 2;
        //}
        //if (state.IsKeyDown(Keys.Down))
        //{
        //    romanSoldierPosition.Y += 2;
        //}


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);

        _spriteBatch.Begin();
        // Replacing the normal SpriteBatch.Draw call to use the version from the "AnimatedTexture" class instead
        spriteTexture.DrawFrame(_spriteBatch, characterPos);
        //_spriteBatch.Draw(romanSoldierTexture, romanSoldierPosition, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace RomeVsOrcs;

public class MainGame : Game
{
    private Texture2D romanSoldierTexture;
    private Vector2 romanSoldierPosition;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        romanSoldierTexture = Content.Load<Texture2D>("soldier2");
        romanSoldierPosition = Vector2.Zero;

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Right))
        {
            romanSoldierPosition.X += 2;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            romanSoldierPosition.X -= 2;
        }
        if (state.IsKeyDown(Keys.Up))
        {
            romanSoldierPosition.Y -= 2;
        }
        if (state.IsKeyDown(Keys.Down))
        {
            romanSoldierPosition.Y += 2;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);

        _spriteBatch.Begin();
        _spriteBatch.Draw(romanSoldierTexture, romanSoldierPosition, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

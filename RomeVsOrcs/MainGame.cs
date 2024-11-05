using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RomeVsOrcs.Textures;

namespace RomeVsOrcs;

public class MainGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SoldierTexture soldierTexture;
    private OrcFactory orcFactory;

    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.GraphicsProfile = GraphicsProfile.HiDef;
        //graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        
    }

    // The game visible area
    private Viewport viewport;
    // The position to draw the character
    private Vector2 characterPos;

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        viewport = graphics.GraphicsDevice.Viewport;
        characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
        soldierTexture = new SoldierTexture(graphics.GraphicsDevice.Viewport);
        soldierTexture.Load(Content, characterPos);
        orcFactory = new OrcFactory();
        orcFactory.Load(Content, viewport, 4);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        soldierTexture.Update(elapsed, orcFactory.OrcTextures);
        orcFactory.Update(elapsed);
       
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);

        spriteBatch.Begin();
        soldierTexture.DrawFrame(spriteBatch);
        orcFactory.Draw(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}

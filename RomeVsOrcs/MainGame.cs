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
    private StatsTexture statsTexture;
    private GrassTexture grassTexture;

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

        statsTexture = new StatsTexture(Content);
        statsTexture.Load();
        grassTexture = new GrassTexture(Content, viewport);
        grassTexture.Load();
      
        characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
        soldierTexture = new SoldierTexture(Content, graphics.GraphicsDevice.Viewport);
        soldierTexture.Load(characterPos);
        orcFactory = new OrcFactory(Content, viewport);
        orcFactory.Load(4);
    
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

        grassTexture.Draw(spriteBatch);
        statsTexture.Draw(spriteBatch);

        soldierTexture.Draw(spriteBatch);
        orcFactory.Draw(spriteBatch);

        spriteBatch.End();

        base.Draw(gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using Myra;
using RomeVsOrcs.Textures;
using RomeVsOrcs.UIComponents;
using Myra.Graphics2D.TextureAtlases;

namespace RomeVsOrcs;

public class MainGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SoldierTexture soldierTexture;
    private OrcFactory orcFactory;
    private StatsTexture statsTexture;
    private GrassTexture grassTexture;
    private Desktop desktop;
    private SettingsButton settingsButton;
    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Viewport viewport = graphics.GraphicsDevice.Viewport;

        statsTexture = new StatsTexture(Content);
        statsTexture.Load();

        MyraEnvironment.Game = this;
        desktop = new Desktop();
        settingsButton = new SettingsButton(Content, graphics, desktop);
        settingsButton.Load();

        grassTexture = new GrassTexture(Content, viewport);
        grassTexture.Load();

        soldierTexture = new SoldierTexture(Content, graphics.GraphicsDevice.Viewport);
        Vector2 characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
        soldierTexture.Load(characterPos);

        orcFactory = new OrcFactory(Content, viewport);
        orcFactory.Load(4);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (Keyboard.GetState().IsKeyDown(Keys.F11))
            ToggleFullScreen();

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
        orcFactory.Draw(spriteBatch);
        soldierTexture.Draw(spriteBatch);
        statsTexture.Draw(spriteBatch);
        spriteBatch.End();
        desktop.Render();

        base.Draw(gameTime);
    }

    private void ToggleFullScreen()
    {
        graphics.IsFullScreen = !graphics.IsFullScreen;
        graphics.ApplyChanges();
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RomeVsOrcs.Textures;
using RomeVsOrcs.UIComponents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace RomeVsOrcs;

public class MainGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SoldierTexture soldierTexture;
    private OrcFactory orcFactory;
    private StatsTexture statsTexture;
    private GrassTexture grassTexture;
    private SettingsButton settingsButton;
    private SettingsDialog dialog;

    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.GraphicsProfile = GraphicsProfile.HiDef;
        //graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Viewport viewport = graphics.GraphicsDevice.Viewport;

        statsTexture = new StatsTexture(Content);
        statsTexture.Load();
        settingsButton = new SettingsButton(Content, viewport);
        settingsButton.Load();
        dialog = new SettingsDialog(Content, GraphicsDevice);
        dialog.Load();

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

        if (Keyboard.GetState().IsKeyDown(Keys.T))
        {
            if (!dialog.IsVisible)
                dialog.Show();
        }

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
        settingsButton.Draw(spriteBatch);
        dialog.Draw(spriteBatch);
     
        spriteBatch.End();

        base.Draw(gameTime);
    }

    private void ToggleFullScreen()
    {
        graphics.IsFullScreen = !graphics.IsFullScreen;
        graphics.ApplyChanges();
    }
}

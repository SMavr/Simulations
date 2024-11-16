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

        MyraEnvironment.Game = this;
        desktop = new Desktop();
        settingsButton = new SettingsButton(Content, GraphicsDevice, desktop);
        settingsButton.Load();


        grassTexture = new GrassTexture(Content, viewport);
        grassTexture.Load();

        soldierTexture = new SoldierTexture(Content, graphics.GraphicsDevice.Viewport);
        Vector2 characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
        soldierTexture.Load(characterPos);

        orcFactory = new OrcFactory(Content, viewport);
        orcFactory.Load(4);

        CheckButton();
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
        settingsButton.Update(elapsed);

       
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
        spriteBatch.End();
        desktop.Render();

        base.Draw(gameTime);
    }

    private void ToggleFullScreen()
    {
        graphics.IsFullScreen = !graphics.IsFullScreen;
        graphics.ApplyChanges();
    }

    Button _button;
    private void CheckButton()
    {
        // Create the button and set its properties
        _button = new Button
        {
            Content = new Image
            {
                Renderable = new TextureRegion(Content.Load<Texture2D>("cog"))
            },
            Width = 40,
            Height = 40,
            Background = null,
            PressedBackground = null,
        };

        // Set the button position to the upper right corner
        _button.Left = graphics.PreferredBackBufferWidth - _button.Width.Value - 100;
        _button.Top = 10;

        // Add an event handler for the button click
        //_button.Click += (s, e) =>
        //{
        //    // Toggle fullscreen as an example action
        //    _graphics.IsFullScreen = !_graphics.IsFullScreen;
        //    _graphics.ApplyChanges();
        //};

        // Add the button to the desktop
        desktop.Widgets.Add(_button);
    }
}

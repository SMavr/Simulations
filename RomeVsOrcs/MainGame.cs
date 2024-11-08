﻿using Microsoft.Xna.Framework;
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
    private Texture2D grassTexture;

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
        soldierTexture = new SoldierTexture(Content, graphics.GraphicsDevice.Viewport);
        soldierTexture.Load(characterPos);
        orcFactory = new OrcFactory(Content, viewport);
        orcFactory.Load(4);
        statsTexture = new StatsTexture(Content);
        statsTexture.Load();
        grassTexture = Content.Load<Texture2D>("grass");
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


        int windowWidth = GraphicsDevice.Viewport.Width;
        int windowHeight = GraphicsDevice.Viewport.Height;
        for (int x = 0; x < windowWidth; x += grassTexture.Width)
        {
            for (int y = 0; y < windowHeight; y += grassTexture.Height)
            {
                spriteBatch.Draw(grassTexture, new Vector2(x, y), Color.White);
            }
        }


        soldierTexture.DrawFrame(spriteBatch);
        orcFactory.Draw(spriteBatch);
        statsTexture.Draw(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}

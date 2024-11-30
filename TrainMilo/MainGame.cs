using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TrainMilo;

public class MainGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Texture2D dotTexture;
    private Vector2 dotPosition;
    private Vector2 goalPosition;
    //private NeuralNetwork neuralNetwork;
    private Random random = new Random();

    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        dotPosition = new Vector2(100, 100);
        goalPosition = new Vector2(400, 400);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        dotTexture = new Texture2D(GraphicsDevice, 1, 1);
        dotTexture.SetData(new[] { Color.White });

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    private void TrainDot()
    {
        double[] inputs = {
            dotPosition.X, dotPosition.Y,
            goalPosition.X, goalPosition.Y
        };
            
        //double[] outputs = neuralNetwork.Forward(inputs);

        //float moveX = (float)(outputs[0] * 2 - 1);
        //float moveY = (float)(outputs[1] * 2 - 1);
            
        //dotPosition.X += moveX;
        //dotPosition.Y += moveY;

        //double distance = Vector2.Distance(dotPosition, goalPosition);
        //double[] targets = { moveX, moveY };

      //  neuralNetwork.Train(inputs, targets);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();
        spriteBatch.Draw(dotTexture, new Rectangle((int)dotPosition.X, (int)dotPosition.Y, 10, 10), Color.Red);
        spriteBatch.Draw(dotTexture, new Rectangle((int)goalPosition.X, (int)goalPosition.Y, 10, 10), Color.LightGreen);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace TrainMilo;
// https://medium.com/analytics-vidhya/building-a-simple-neural-network-in-c-7e917e9fc2cc
public class MainGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Texture2D dotTexture;
    private Vector2 dotPosition;
    private Vector2 goalPosition;
    private NeuralNetwork neuralNetwork;

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
        neuralNetwork = new NeuralNetwork(4, 8, 2);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        dotTexture = new Texture2D(GraphicsDevice, 1, 1);
        dotTexture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        TrainDot();

        base.Update(gameTime);
    }

    private void TrainDot()
    {
        // Normalize inputs to the range 0-1
        double[] inputs = {
                dotPosition.X / graphics.PreferredBackBufferWidth,
                dotPosition.Y / graphics.PreferredBackBufferHeight,
                goalPosition.X / graphics.PreferredBackBufferWidth,
                goalPosition.Y / graphics.PreferredBackBufferHeight
            };

        // Neural network outputs for movement
        double[] outputs = neuralNetwork.Forward(inputs);

        // Convert outputs to movement (-1 to 1 range)
        float moveX = (float)(outputs[0] * 2 - 1);
        float moveY = (float)(outputs[1] * 2 - 1);

        // Calculate distance to the goal
        double previousDistance = Vector2.Distance(dotPosition, goalPosition);

        // Update dot position based on neural network outputs
        dotPosition.X += moveX;
        dotPosition.Y += moveY;

        // Ensure the dot stays on screen
        dotPosition.X = Math.Clamp(dotPosition.X, 0, graphics.PreferredBackBufferWidth);
        dotPosition.Y = Math.Clamp(dotPosition.Y, 0, graphics.PreferredBackBufferHeight);

        // Calculate new distance to the goal
        double currentDistance = Vector2.Distance(dotPosition, goalPosition);

        // Reward: positive for getting closer, negative for getting farther
        double reward = previousDistance - currentDistance;

        // Training targets: scaled movement adjusted by the reward
        double[] targets = {
                (moveX + 1) / 2 + reward,
                (moveY + 1) / 2 + reward
            };

        Debug.WriteLine($"x: {targets[0]:F3}, y: {targets[1]:F3}");

        neuralNetwork.Train(inputs, targets);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();
        spriteBatch.Draw(dotTexture, new Rectangle((int)goalPosition.X, (int)goalPosition.Y, 10, 10), Color.LightGreen);
        spriteBatch.Draw(dotTexture, new Rectangle((int)dotPosition.X, (int)dotPosition.Y, 10, 10), Color.Red);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}

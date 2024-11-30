using System;

internal class NeuralNetwork
{
    private int inputNodes, hiddenNodes, outputNodes;
    private double[,] weightsInputHidden, weightsHiddenOutput;
    private double[] hiddenLayer, outputLayer;
    private double learningRate = 0.1;

    public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes)
    {
        this.inputNodes = inputNodes;
        this.hiddenNodes = hiddenNodes;
        this.outputNodes = outputNodes;

        weightsInputHidden = InitializeWeights(inputNodes, hiddenNodes);
        weightsHiddenOutput = InitializeWeights(hiddenNodes, outputNodes);
    }

    private double[,] InitializeWeights(int rows, int cols)
    {
        Random rand = new Random();
        double[,] weights = new double[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                weights[i, j] = rand.NextDouble() * 2 - 1;
        return weights;
    }

    private double Sigmoid(double x) => 1 / (1 + Math.Exp(-x));
    private double SigmoidDerivative(double x) => x * (1 - x);

    public double[] Forward(double[] inputs)
    {
        hiddenLayer = new double[hiddenNodes];
        for (int i = 0; i < hiddenNodes; i++)
        {
            hiddenLayer[i] = 0;
            for (int j = 0; j < inputNodes; j++)
                hiddenLayer[i] += inputs[j] * weightsInputHidden[j, i];
            hiddenLayer[i] = Sigmoid(hiddenLayer[i]);
        }

        outputLayer = new double[outputNodes];
        for (int i = 0; i < outputNodes; i++)
        {
            outputLayer[i] = 0;
            for (int j = 0; j < hiddenNodes; j++)
                outputLayer[i] += hiddenLayer[j] * weightsHiddenOutput[j, i];
            outputLayer[i] = Sigmoid(outputLayer[i]);
        }
        return outputLayer;
    }

    public void Train(double[] inputs, double[] targets)
    {
        Forward(inputs);

        double[] outputErrors = new double[outputNodes];
        for (int i = 0; i < outputNodes; i++)
            outputErrors[i] = targets[i] - outputLayer[i];

        double[] hiddenErrors = new double[hiddenNodes];
        for (int i = 0; i < hiddenNodes; i++)
        {
            hiddenErrors[i] = 0;
            for (int j = 0; j < outputNodes; j++)
                hiddenErrors[i] += outputErrors[j] * weightsHiddenOutput[i, j];
            hiddenErrors[i] *= SigmoidDerivative(hiddenLayer[i]);
        }

        for (int i = 0; i < hiddenNodes; i++)
            for (int j = 0; j < outputNodes; j++)
                weightsHiddenOutput[i, j] += learningRate * outputErrors[j] * hiddenLayer[i];

        for (int i = 0; i < inputNodes; i++)
            for (int j = 0; j < hiddenNodes; j++)
                weightsInputHidden[i, j] += learningRate * hiddenErrors[j] * inputs[i];
    }
}

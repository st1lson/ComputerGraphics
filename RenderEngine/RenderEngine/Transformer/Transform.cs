using RenderEngine.Basic;

namespace RenderEngine.Transformer;

public class Transform
{
    public static readonly float[,] IdentityMatrix = new float[,]
    {
        { 1, 0, 0, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 1 }
    };

    public Transform(float[,] matrix)
    {
        MatrixTransform = matrix;
    }

    public float[,] MatrixTransform { get; }

    public Transform RotateX(float rad, bool clockwise=true)
    {
        if (clockwise)
        {
            rad *= -1;
        }

        float[,] rotationMatrix =
        {
            { 1, 0, 0, 0 },
            { 0, (float)Math.Cos(rad), -(float)Math.Sin(rad), 0 },
            { 0, (float)Math.Sin(rad), (float)Math.Cos(rad), 0 },
            { 0, 0, 0, 1 }
        };

        return new Transform(Matrix.Multiply(rotationMatrix, MatrixTransform));
    }

    public Transform RotateY(float rad, bool clockwise = true)
    {
        if (clockwise)
        {
            rad *= -1;
        }

        float[,] rotationMatrix =
        {
            { (float)Math.Cos(rad), 0, (float)Math.Sin(rad), 0 },
            { 0, 1, 0, 0 },
            { -(float)Math.Sin(rad), 0, (float)Math.Cos(rad), 0 },
            { 0, 0, 0, 1 }
        };

        return new Transform(Matrix.Multiply(rotationMatrix, MatrixTransform));
    }

    public Transform RotateZ(float rad, bool clockwise = true)
    {
        if (clockwise)
        {
            rad *= -1;
        }

        float[,] rotationMatrix =
        {
            { (float)Math.Cos(rad), -(float)Math.Sin(rad), 0, 0 },
            { (float)Math.Sin(rad), (float)Math.Cos(rad), 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
        };

        return new Transform(Matrix.Multiply(rotationMatrix, MatrixTransform));
    }

    public Transform Translate(Vector3 vector)
    {
        float[,] translationMatrix = new float[4, 4]
        {
            { 1, 0, 0, vector.X },
            { 0, 1, 0, vector.Y },
            { 0, 0, 1, vector.Z },
            { 0, 0, 0, 1 }
        };

        return new Transform(Matrix.Multiply(translationMatrix, MatrixTransform));
    }

    public Transform Scale(Vector3 vector)
    {
        float[,] scaleMatrix = new float[4, 4]
        {
            { vector.X, 0, 0, 0 },
            { 0, vector.Y, 0, 0 },
            { 0, 0, vector.Z, 0 },
            { 0, 0, 0, 1 }
        };

        return new Transform(Matrix.Multiply(scaleMatrix, MatrixTransform));
    }
}
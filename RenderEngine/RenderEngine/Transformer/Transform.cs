using RenderEngine.Basic;
using System;
using System.Runtime.InteropServices;

namespace RenderEngine.Transformer
{
    public class Transformer
    {
        public float[,] IdentityMatrix { get; } = new float[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

        public Transformer(float[,] matrix)
        {
            MatrixTransform = matrix;
        }

        public float[,] MatrixTransform { get; }

        public Transformer RotateX(float rad, bool clockwise=true)
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

            return new Transformer(Matrix.Multiply(rotationMatrix, MatrixTransform));
        }

        public Transformer RotateY(float rad, bool clockwise = true)
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

            return new Transformer(Matrix.Multiply(rotationMatrix, MatrixTransform));
        }

        public Transformer RotateZ(float rad, bool clockwise = true)
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

            return new Transformer(Matrix.Multiply(rotationMatrix, MatrixTransform));
        }

        public void Translate(Vector3 vector)
        {
            float[,] translationMatrix = new float[4, 4]
            {
                { 1, 0, 0, vector.X },
                { 0, 1, 0, vector.Y },
                { 0, 0, 1, vector.Z },
                { 0, 0, 0, 1 }
            };

            new Transformer(Matrix.Multiply(translationMatrix, MatrixTransform));
        }

        public Transformer Scale(Vector3 vector)
        {
            float[,] scaleMatrix = new float[4, 4]
            {
            { vector.X, 0, 0, 0 },
            { 0, vector.Y, 0, 0 },
            { 0, 0, vector.Z, 0 },
            { 0, 0, 0, 1 }
            };

            return new Transformer(Matrix.Multiply(scaleMatrix, MatrixTransform));
        }
    }
}

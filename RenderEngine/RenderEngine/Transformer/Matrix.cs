namespace RenderEngine.Transformer
{
    public static class Matrix
    {
        public static float[,] Multiply(float[,] matrix1, float[,] matrix2)
        {
            float[,] multiplyMatrix = new float[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int row = 0; row < matrix1.GetLength(0); row++)
            {
                for (int col = 0; col < matrix2.GetLength(1); col++)
                {
                    for (int i = 0; i < matrix1.GetLength(1); i++)
                    {
                        multiplyMatrix[row, col] = multiplyMatrix[row, col] + matrix1[row, i] * matrix2[i, col];
                    }
                }
            }

            return multiplyMatrix;
        }
    }
}

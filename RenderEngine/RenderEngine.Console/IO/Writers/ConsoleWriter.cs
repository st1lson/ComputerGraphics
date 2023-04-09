using RenderEngine.Models;

namespace RenderEngine.Console.IO.Writers;

public sealed class ConsoleWriter : IWriter
{
    public void Serialize(Bitmap bitmap)
    {
        for (int i = 0; i < bitmap.GetLength(0); i++)
        {
            for (int j = 0; j < bitmap.GetLength(1); j++)
            {
                var numericValue = bitmap[i, j].GetNumeric();

                if (numericValue <= 0)
                {
                    System.Console.Write(" ");
                }
                else if (numericValue <= 0.2 && numericValue > 0)
                {
                    System.Console.Write(".");
                }
                else if (numericValue <= 0.5 && numericValue > 0.2)
                {
                    System.Console.Write("*");
                }
                else if (numericValue <= 0.8 && numericValue > 0.5)
                {
                    System.Console.Write("O");
                }
                else
                {
                    System.Console.Write("#");
                }
            }

            System.Console.WriteLine();
        }
    }
}
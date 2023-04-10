using RenderEngine.Models;

namespace RenderEngine.Cli.IO.Writers;

internal sealed class ConsoleWriter
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
                    Console.Write(" ");
                }
                else if (numericValue <= 0.2 && numericValue > 0)
                {
                    Console.Write(".");
                }
                else if (numericValue <= 0.5 && numericValue > 0.2)
                {
                    Console.Write("*");
                }
                else if (numericValue <= 0.8 && numericValue > 0.5)
                {
                    Console.Write("O");
                }
                else
                {
                    Console.Write("#");
                }
            }

            Console.WriteLine();
        }
    }
}
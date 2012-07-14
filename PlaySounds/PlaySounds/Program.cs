// This example demonstrates the Console.Beep(Int32, Int32) method
using System;
using System.Collections.Generic;
using System.Threading;
using PlaySounds;
using System.Linq;

class Sample
{
    public static void Main()
    {
        Random randomNumberGenerator = new Random();
        
        Matrix m = new Matrix(GetRandomFirstRow(randomNumberGenerator));

    }

    private static Note[] GetRandomFirstRow(Random randomNumberGenerator)
    {
        Note[] firstRow = new Note[12];
        var firstFrequencies = Enumerable.Range(0, 12).Select(GetFrequency).ToList();

        for (int i = 0; i < 12; i++)
        {
            int nextIndex = randomNumberGenerator.Next(0, firstFrequencies.Count - 1);
            firstRow[i] = new Note(firstFrequencies[nextIndex]);
            firstFrequencies.RemoveAt(nextIndex);
        }

        return firstRow;
    }

    public static int GetFrequency(int x)
    {
        return (int)(440 * Math.Pow(2, (x / 12.0)));
    }
}
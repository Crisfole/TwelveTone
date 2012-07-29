using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaySounds
{
    public class Matrix
    {
        public static Tone[] GetRandomFirstRow(Random randomNumberGenerator = null)
        {
            randomNumberGenerator = randomNumberGenerator ?? new Random();
            Tone[] firstRow = new Tone[12];
            var firstFrequencies = Enumerable.Range(0, 12).Select(x => Tone.GetFrequency(x)).ToList();

            for (int i = 0; i < 12; i++)
            {
                int nextIndex = randomNumberGenerator.Next(0, firstFrequencies.Count - 1);
                firstRow[i] = new Tone(firstFrequencies[nextIndex]);
                firstFrequencies.RemoveAt(nextIndex);
            }

            return firstRow;
        }

        Tone[][] notes;
        Dictionary<Tone, int> noteToOrdinal;
        Dictionary<int, Tone> ordinalToNote;

        public Matrix(Tone[] firstRow = null)
        {
            firstRow = firstRow ?? GetRandomFirstRow();
            Validate(firstRow);
            InitializeVariables(firstRow);
            InitializeMatrix(firstRow);
        }

        private void Validate(Tone[] firstRow)
        {
            if (firstRow.Length != 12)
            {
                throw new ArgumentException("firstRow must have 12 tones");
            }

            if (firstRow.Select(x => (int)x).Distinct().Count() != firstRow.Length)
            {
                throw new ArgumentException("row must not have repeated values");
            }
        }

        private void InitializeVariables(Tone[] firstRow)
        {
            notes = new Tone[12][];
            
            notes[0] = firstRow
                .Select(x => Normalize(x, firstRow[0], firstRow[0] * 2))
                .ToArray();
            for (int i = 1; i < 12; i++)
            {
                notes[i] = new Tone[12];
            }

            noteToOrdinal = notes[0]
                .OrderBy(x => (int)x)
                .Select((note, ordinal) => new { note, ordinal })
                .ToDictionary(obj => obj.note, obj => obj.ordinal);
            ordinalToNote = noteToOrdinal
                .ToDictionary(x => x.Value, x => x.Key);
        }

        private Tone Normalize(Tone x, int lowend, int highend)
        {
            while (lowend > (int)x)
            {
                x = new Tone(2 * (int)x);
            }
            while (highend <= (int)x)
            {
                x = new Tone((int)x / 2);
            }
            return x;
        }
    
        private void InitializeMatrix(Tone[] firstRow)
        {
            for (int i = 1; i < 12; ++i)
            {
                int primeRowNumber = 12 - noteToOrdinal[notes[0][i]];
                notes[i][0] = ordinalToNote[primeRowNumber];
                for (int j = 1; j < 12; ++j)
                {
                    int nextOrdinal = primeRowNumber + noteToOrdinal[notes[0][j]];
                    nextOrdinal = nextOrdinal >= 12 ? nextOrdinal - 12 : nextOrdinal;
                    notes[i][j] = ordinalToNote[nextOrdinal];
                }
            }
        }

        public Tone[] Row(int i)
        {
            return notes[i].ToArray();
        }

        public Tone[] Column(int i)
        {
            Tone[] column = new Tone[12];
            for (int j = 0; j < 12; j++)
            {
                column[j] = notes[j][i];
            }
            return column;
        }

        public override string ToString()
        {
            return string.Join("\r\n", notes.Select(x => string.Join(" ", x.Select(n => n.ToString().PadRight(2)))));
        }

        internal Tone Note(int i, int j)
        {
            return notes[i][j];
        }
    }
}

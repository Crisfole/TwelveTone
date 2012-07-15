using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaySounds
{
    public class Matrix
    {
        Note[][] notes;
        Dictionary<Note, int> noteToOrdinal;
        Dictionary<int, Note> ordinalToNote;

        public Matrix(Note[] firstRow)
        {
            Validate(firstRow);
            InitializeVariables(firstRow);
            InitializeMatrix(firstRow);
        }

        private void Validate(Note[] firstRow)
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

        private void InitializeVariables(Note[] firstRow)
        {
            notes = new Note[12][];
            
            notes[0] = firstRow
                .Select(x => Normalize(x, firstRow[0], firstRow[0] * 2))
                .ToArray();
            for (int i = 1; i < 12; i++)
            {
                notes[i] = new Note[12];
            }

            noteToOrdinal = notes[0]
                .OrderBy(x => (int)x)
                .Select((note, ordinal) => new { note, ordinal })
                .ToDictionary(obj => obj.note, obj => obj.ordinal);
            ordinalToNote = noteToOrdinal
                .ToDictionary(x => x.Value, x => x.Key);
        }

        private Note Normalize(Note x, int lowend, int highend)
        {
            while (lowend > (int)x)
            {
                x = new Note(2 * (int)x);
            }
            while (highend <= (int)x)
            {
                x = new Note((int)x / 2);
            }
            return x;
        }
    
        private void InitializeMatrix(Note[] firstRow)
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
            Console.WriteLine(ToString());
        }

        public Note[] Row(int i)
        {
            return notes[i].ToArray();
        }

        public Note[] Column(int i)
        {
            Note[] column = new Note[12];
            for (int j = 0; i < 12; i++)
            {
                column[j] = notes[j][i];
            }
            return column;
        }

        public override string ToString()
        {
            return string.Join("\r\n", notes.Select(x => string.Join(" ", x.Select(n => n.ToString().PadRight(2)))));
        }

        internal Note Note(int i, int j)
        {
            return notes[i][j];
        }
    }
}

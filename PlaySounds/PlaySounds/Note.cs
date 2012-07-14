using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
namespace PlaySounds
{
    [DebuggerDisplay("{ToString()}")]
    public class Note
    {
        private readonly int frequency;
        private readonly string name;

        public Note(int frequency)
        {
            this.frequency = frequency;
        }

        private Note(int frequency, string name)
            :this(frequency)
        {
            this.name = name;
        }

        public Note Octave(int up)
        {
            return new Note(this.frequency * ((int)Math.Pow(2, up)));
        }

        public override string ToString()
        {
            if (!frequencyToNameLookup.ContainsKey(frequency))
            {
                frequencyToNameLookup[frequency] = GetFrequencyName();
            }
            return frequencyToNameLookup[frequency];
        }

        private string GetFrequencyName()
        {
            int tmp = frequency;
            while (tmp < 440)
            {
                tmp *= 2;
            }
            while (tmp >= 880)
            {
                tmp /= 2;
            }
            var namedNote = AllNamedNotes.FirstOrDefault(x => x.frequency == tmp);
            return namedNote.name ?? tmp.ToString();
        }

        public static implicit operator int(Note note)
        {
            int i = (int)Math.Log(440, 2);
            return (int)note.frequency;
        }

        private static Dictionary<int, string> frequencyToNameLookup = new Dictionary<int, string>();

        public static IEnumerable<Note> AllNamedNotes { get { return allNamedNotes; } }

        public static Note A = new Note(440, "A");

        public static Note Bb = new Note(466, "Bb");

        public static Note B = new Note(493, "B");

        public static Note C = new Note(523, "C");

        public static Note Db = new Note(554, "Db");

        public static Note D = new Note(587, "D");

        public static Note Eb = new Note(622, "Eb");

        public static Note E = new Note(659, "E");

        public static Note F = new Note(698, "F");

        public static Note Gb = new Note(739, "Gb");

        public static Note G = new Note(783, "G");

        public static Note Ab = new Note(830, "Ab");

        private static readonly Note[] allNamedNotes = new Note[] { A, Bb, B, C, Db, D, Eb, E, F, Gb, G, Ab };
    }
}
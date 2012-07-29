using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
namespace PlaySounds
{
    [DebuggerDisplay("{ToString()}")]
    public class Tone
    {
        public static int GetFrequency(int x, int frequencyOfFirstNote = 440)
        {
            return (int)(frequencyOfFirstNote * Math.Pow(2, (x / 12.0)));
        }

        private readonly int frequency;
        private readonly string name;

        public Tone(int frequency)
        {
            this.frequency = frequency;
        }

        private Tone(int frequency, string name)
            :this(frequency)
        {
            this.name = name;
        }

        public Tone Octave(int up)
        {
            return new Tone(this.frequency * ((int)Math.Pow(2, up)));
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

        public static implicit operator int(Tone note)
        {
            int i = (int)Math.Log(440, 2);
            return (int)note.frequency;
        }

        private static Dictionary<int, string> frequencyToNameLookup = new Dictionary<int, string>();

        public static IEnumerable<Tone> AllNamedNotes { get { return allNamedNotes; } }

        public static Tone A = new Tone(440, "A");

        public static Tone Bb = new Tone(466, "Bb");

        public static Tone B = new Tone(493, "B");

        public static Tone C = new Tone(523, "C");

        public static Tone Db = new Tone(554, "Db");

        public static Tone D = new Tone(587, "D");

        public static Tone Eb = new Tone(622, "Eb");

        public static Tone E = new Tone(659, "E");

        public static Tone F = new Tone(698, "F");

        public static Tone Gb = new Tone(739, "Gb");

        public static Tone G = new Tone(783, "G");

        public static Tone Ab = new Tone(830, "Ab");

        private static readonly Tone[] allNamedNotes = new Tone[] { A, Bb, B, C, Db, D, Eb, E, F, Gb, G, Ab };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaySounds
{
    public class Note
    {
        public static Note Rest(int milliseconds)
        {
            return new Note(new Tone(0), milliseconds);
        }

        public Note(Tone tone, int milliseconds)
        {
            Tone = tone;
            Milliseconds = milliseconds;
        }

        public Tone Tone { get; private set; }

        public int Milliseconds { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaySounds
{
    public class Note
    {
        public static Note Rest(float seconds)
        {
            return new Note(new Tone(0), seconds);
        }

        public Note(Tone tone, float seconds)
        {
            Tone = tone;
            Seconds = seconds;
        }

        public Tone Tone { get; private set; }

        public float Seconds { get; private set; }
    }
}

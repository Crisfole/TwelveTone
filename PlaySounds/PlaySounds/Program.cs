using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using PlaySounds;
using Matrix = PlaySounds.Matrix;

public class Sample
{
    private static Random rand;
    private static List<List<Note>> notesPlayed = new List<List<Note>>();

    public static void Main()
    {
        rand = new Random();
        var schoenOp25 = new Tone[] { Tone.E, Tone.F, Tone.G, Tone.Db, Tone.Gb, Tone.Eb, Tone.Ab, Tone.D, Tone.B, Tone.C, Tone.A, Tone.Bb };

        Matrix m = new Matrix(schoenOp25);
        
        Console.WriteLine(m);
        Console.WriteLine("");

        PlaySonata(m);

        Console.WriteLine("");
        Console.WriteLine("Finé");

        Console.ReadLine();
    }

    private static void PlayBinary(Matrix m)
    {
        int quarterNoteLength = 250;

        PlayBinarySection(m, quarterNoteLength);

        quarterNoteLength = 150;
        PlayBinarySection(m, quarterNoteLength);
    }

    private static void PlayBinarySection(Matrix m, int quarterNoteLength)
    {
        for (int i = 0; i < 2; i++)
        {
            Tone[] tones;

            if (i == 0)
            {
                tones = m.Row(0);
            }
            else
            {
                tones = RandomSequence(m);
            }

            PlayTones(tones, quarterNoteLength);
        }
        foreach (Note n in notesPlayed.SelectMany(nl => nl)) PlayNote(n);
        notesPlayed.Clear();
        Console.WriteLine("");
    }

    private static void PlaySonata(Matrix m)
    {
        int quarterNoteLength = 250;
        for (int i = 0; i < 24; i++)
        {
            Tone[] tones;
            
            if (i == 0)
            {
                tones = m.Row(0);
            }
            else
            {
                tones = RandomSequence(m);
            }
            if (i > 7)
            {
                quarterNoteLength = 750;
            }
            if (i > 11)
            {
                quarterNoteLength = 150;
            }

            PlayTones(tones, quarterNoteLength);
        }
    }

    private static Tone[] RandomSequence(Matrix m)
    {
        Tone[] tones;
        var playColumn = rand.Next(0, 2) == 0;
        var backwards = rand.Next(0, 2) == 0;
        int index = rand.Next(0, 12);

        if (playColumn)
        {
            tones = m.Column(index).ToArray();
        }
        else
        {
            tones = m.Row(index);
        }
        if (backwards)
        {
            tones = tones.Reverse().ToArray();
        }
        return tones;
    }

    private static void PlayTones(Tone[] tones, int quarterNoteLength)
    {
        notesPlayed.Add(new List<Note>());

        foreach (var t in tones.Select((tone, idx) => new { tone, idx }))
        {
            Console.Write(t.tone.ToString());
            if(t.idx == 11)
            {
                Console.WriteLine("");
            }
            else
            {
                Console.Write(",");
            }

            Tone tone = t.tone;
            int length = rand.Next(1, 5) * quarterNoteLength;
            notesPlayed.Last().Add(new Note(tone, length));

            PlayNote(notesPlayed.Last().Last());
        }
    }

    private static void PlayNote(Note note)
    {
        Console.Beep(note.Tone, note.Milliseconds);
    }
}
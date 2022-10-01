using System;
using System.Collections.Generic;
using System.Linq;

namespace PianoSimulation
{
    public class Piano : IPiano
    {
        private readonly List<KeyControl> _pianoKeys = new List<KeyControl>();
        public string Keys { get; }

        public Piano(string keys, int samplingRate, double decay = 0.996, double releaseDecay = 0.9)
        {
            Keys = keys;
            for (int i = 0; i < keys.Length; i++)
            {
                var noteFrequency = Math.Pow(2, (i - 24) / 12d) * 440;
                var musicalString = new PianoWire(noteFrequency, samplingRate);
                _pianoKeys.Add(new KeyControl(musicalString, decay, releaseDecay));
            }
        }

        public int StrikeKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1) return -1;
            _pianoKeys[keyIndex].Strike();
            return 0;
        }

        public int RemoveKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1) return -1;
            _pianoKeys[keyIndex].ReleaseKey();
            return 0;
        }

        public double Play()
        {
            return _pianoKeys.Sum(key => key.Sample());
        }

        public List<string> GetPianoKeys()
        {
            return _pianoKeys.Select(pianoKey => pianoKey.ToString()).ToList();
        }
    }
}
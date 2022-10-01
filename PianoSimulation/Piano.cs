using System;
using System.Collections.Generic;
using System.Linq;

namespace PianoSimulation
{
    public class Piano : IPiano
    {
        private readonly double _decay;
        private readonly List<IMusicalString> _pianoKeys = new List<IMusicalString>();
        public IMusicalString[] PressedKeys { get; }
        public string Keys { get; }
        
        public Piano(string keys, int samplingRate, double decay = 0.996)
        {
            Keys = keys;
            _decay = decay;
            for (int i = 0; i < keys.Length; i++)
            {
                var noteFrequency = Math.Pow(2, (i - 24) / 12d) * 440;
                var musicalString = new PianoWire(noteFrequency, samplingRate);
                _pianoKeys.Add(musicalString);
            }

            PressedKeys = new IMusicalString[_pianoKeys.Capacity];
        }
            
        public int StrikeKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1) return -1;
            PressedKeys[keyIndex] = _pianoKeys[keyIndex];
            PressedKeys[keyIndex].Strike();
            return 0;
        }
        
        public int RemoveKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1) return -1;
            PressedKeys[keyIndex] = null;
            return 0;
        }

        public double Play()
        {
            double sampleSum = 0;
            foreach (var pianoKey in PressedKeys)
            {
                if (pianoKey != null)
                {
                    sampleSum += pianoKey.Sample(_decay);
                }
            }

            return sampleSum;
        }

        public List<string> GetPianoKeys()
        {
            return _pianoKeys.Select(pianoKey => pianoKey.ToString()).ToList();
        }
    }
}
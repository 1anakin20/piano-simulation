using System;
using System.Collections.Generic;
using System.Linq;

namespace PianoSimulation
{
    public class Piano : IPiano
    {
        private readonly double _decay;
        private List<IMusicalString> _pianoKeys = new List<IMusicalString>();
        public IMusicalString[] _pressedKeys;
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

            _pressedKeys = new IMusicalString[_pianoKeys.Capacity];
        }
            
        public void StrikeKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1) return;
            _pressedKeys[keyIndex] = _pianoKeys[keyIndex];
            _pressedKeys[keyIndex].Strike();
        }
        
        public void RemoveKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1)
            {
                return;
            }
            _pressedKeys[keyIndex] = null;
        }

        public double Play()
        {
            double sampleSum = 0;
            foreach (var pianoKey in _pressedKeys)
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
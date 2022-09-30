using System;
using System.Collections.Generic;
using System.Linq;

namespace PianoSimulation
{
    public class Piano : IPiano
    {
        private readonly double _decay;
        private List<IMusicalString> _pianoKeys = new List<IMusicalString>();
        public string Keys { get; }
        
        public Piano(string keys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ", int samplingRate = 44100, double decay = 0.996)
        {
            Keys = keys;
            _decay = decay;
            for (int i = 0; i < keys.Length; i++)
            {
                var noteFrequency = Math.Pow(2, (i - 24) / 12d) * 440;
                var musicalString = new PianoWire(noteFrequency, samplingRate);
                _pianoKeys.Add(musicalString);
            }
        }
            
        public void StrikeKey(char key)
        {
            var keyIndex = Keys.IndexOf(key);
            if (keyIndex == -1) throw new PianoKeyDoesNotExistsException($"{key} key does not exist");
            _pianoKeys[keyIndex].Strike();
        }

        public double Play()
        {
            double sampleSum = 0;
            foreach (var pianoKey in _pianoKeys)
            {
                sampleSum += pianoKey.Sample(_decay);
            }

            return sampleSum;
        }

        public List<string> GetPianoKeys()
        {
            return _pianoKeys.Select(pianoKey => pianoKey.ToString()).ToList();
        }
    }
}
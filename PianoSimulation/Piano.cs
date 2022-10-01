using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PianoSimulation
{
    public class Piano : IPiano
    {
        private readonly KeyControl[] _pianoKeys;

        public Piano(int numberOfKeys, int samplingRate, double decay = 0.996, double releaseDecay = 0.9,
            double startingFrequency = 440)
        {
            _pianoKeys = new KeyControl[numberOfKeys];
            for (int i = 0; i < numberOfKeys; i++)
            {
                var noteFrequency = Math.Pow(2, (i - 24) / 12d) * startingFrequency;
                var musicalString = new PianoWire(noteFrequency, samplingRate);
                _pianoKeys[i] = new KeyControl(musicalString, decay, releaseDecay);
            }
        }

        public int StrikeKey(int key)
        {
            if (IsValidKey(key))
            {
                _pianoKeys[key].Strike();
                return 0;
            }

            return -1;
        }

        public int RaiseKey(int key)
        {
            if (IsValidKey(key))
            {
                _pianoKeys[key].ReleaseKey();
                return 0;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsValidKey(int key)
        {
            return key >= 0 && key < _pianoKeys.Length;
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
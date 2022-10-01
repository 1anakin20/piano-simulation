using System;

namespace PianoSimulation
{
    public class PianoWire : IMusicalString
    {
        
        private const double MaxAmplitude = 0.5; 
        private readonly CircularArray _frequenciesBuffer;
        public double NoteFrequency { get; }
        public int NumberOfSamples { get; }
        
        public PianoWire(double noteFrequency, int samplingRate)
        {
            NoteFrequency = noteFrequency;
            NumberOfSamples = (int) Math.Round(samplingRate / noteFrequency);
            _frequenciesBuffer = new CircularArray(NumberOfSamples);
        }
        
        public void Strike()
        {
            var random = new Random();
            for (var i = 0; i < NumberOfSamples; i++)
            {
                var value = random.NextDouble() - MaxAmplitude;
                _frequenciesBuffer.Shift(value);
            }
        }

        public double Sample(double decay = 0.996)
        {
            var firstValue = _frequenciesBuffer[0];
            var secondValue = _frequenciesBuffer[1];
            var newValue = (firstValue + secondValue) / 2 * decay;
            _frequenciesBuffer.Shift(newValue);
            return newValue;
        }

        public override string ToString()
        {
            return $"NoteFrequency: {NoteFrequency}, NumberOfSamples: {NumberOfSamples}";
        }
    }
}
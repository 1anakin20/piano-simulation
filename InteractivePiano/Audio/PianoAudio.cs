using System;
using NAudio.Wave;
using PianoSimulation;

namespace InteractivePiano.Audio
{
    public class PianoAudio : ISampleProvider, IDisposable
    {
        private readonly WaveOut _waveOut;
        public WaveFormat WaveFormat { get; }
        private readonly Piano _piano;

        public PianoAudio(Piano piano, int sampleRate)
        {
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate * 3, 1);
            _waveOut = new WaveOut();
            _waveOut.Init(this);
            _piano = piano;
            _waveOut.Play();
        }

        public void AddNote(char key)
        {
            _piano.StrikeKey(key);
        }

        public void RemoveNote(char key)
        {
            _piano.RaiseKey(key);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            for (var i = offset; i < count; i++)
            {
                buffer[i] = (float)_piano.Play();
            }
            return count;
        }

        public void Dispose() => _waveOut.Dispose();
    }
}
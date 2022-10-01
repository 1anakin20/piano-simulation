using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using PianoSimulation;

namespace InteractivePiano.Audio
{
    public class PianoAudio : ISampleProvider, IDisposable
    {
        private readonly WaveOut _waveOut;
        public WaveFormat WaveFormat { get; }
        private readonly Piano _piano;
        private List<char> _keysPressed;

        public PianoAudio(Piano piano, int sampleRate)
        {
            _keysPressed = new List<char>();
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1);
            _waveOut = new WaveOut();
            _waveOut.Init(this);
            _piano = piano;
            _waveOut.Play();
        }

        public void AddNote(char key)
        {
            _keysPressed.Add(key);
            _piano.StrikeKey(key);
        }

        public void RemoveNote(char key)
        {
            _keysPressed.Remove(key);
            _piano.RemoveKey(key);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (!_keysPressed.Any())
            {
                Array.Clear(buffer, offset, count);
                return count;
            }

            for (int i = offset; i < count; i++)
            {
                // var playLength = _sampleRate * 4;
                buffer[i] = (float)_piano.Play();
            }
            return count;
        }

        public void Dispose() => _waveOut.Dispose();
    }
}
using System;
using NAudio.Wave;
using PianoSimulation;

namespace InteractivePiano.Audio
{
    /// <summary>
    /// Plays the piano sounds
    /// It samples the sounds continually to play them
    /// See <see href="https://github.com/naudio/NAudio/blob/master/Docs/WaveProviders.md">explanation of WaveProviders</see>
    /// See <seealso cref="ISampleProvider"/>
    ///
    /// Note for the assignment:
    /// This replaces the original Audio.cs class. The starting class had many problems with playback.
    /// It didn't allow to dynamically sample the piano when new keys were pressed or released.
    /// A critical section and this being ran in a thread are requirements stated on the instructions.
    /// However, this new implementation does not require a thread nor a critical section as it handled by <see cref="WaveOut"/>.
    /// </summary>
    public class PianoAudio : ISampleProvider, IDisposable
    {
        private readonly WaveOut _waveOut;
        public WaveFormat WaveFormat { get; }
        private readonly Piano _piano;
        private static Object _instanceLock = new Object();
        private static PianoAudio _instance;

        /// <summary>
        /// Construct a new <see cref="PianoAudio"/> object
        /// </summary>
        /// <param name="piano">An instance of <see cref="PianoSimulation.Piano"/></param>
        /// <param name="sampleRate">The audio sampling rate, recommended is 44100. It will be multiplied by 3,
        /// As a small sample rate will be too short to hear</param>
        private PianoAudio(Piano piano, int sampleRate)
        {
            if (piano == null) throw new ArgumentNullException(nameof(piano));
            if (sampleRate < 0) throw new ArgumentOutOfRangeException(nameof(sampleRate));

            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate * 3, 1);
            _waveOut = new WaveOut();
            _waveOut.Init(this);
            _piano = piano;
            _waveOut.Play();
        }

        /// <summary>
        /// Gets the instance of the <see cref="PianoAudio"/> object
        /// </summary>
        /// <param name="piano">Piano instance that will be played</param>
        /// <param name="sampleRate">Sample rate of the piano</param>
        /// <returns><see cref="PianoAudio"/> instance</returns>
        public static PianoAudio GetInstance(Piano piano, int sampleRate)
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                {
                    _instance ??= new PianoAudio(piano, sampleRate);
                }
            }

            return _instance;
        }

        /// <summary>
        /// Strikes a note
        /// </summary>
        /// <param name="key">The number of the key to play</param>
        /// <returns>
        /// Returns -1 if the key is invalid, else returns 0
        /// Doesn't throw an exception due to performance reasons
        /// </returns>
        public int AddNote(int key)
        {
            return _piano.StrikeKey(key);
        }

        /// <summary>
        /// Releases a note. Doesn't care if the key is already released
        /// </summary>
        /// <param name="key">The number of the key to release</param>
        /// <returns></returns>
        public int RemoveNote(int key)
        {
            return _piano.RaiseKey(key);
        }

        /// <summary>
        /// This is an explanation of how it works for understanding purposes, not for using it
        /// It is not expected to be called by other than the <see cref="WaveOut"/> class
        /// This is called by <see cref="WaveOut"/> to get wave values to play
        /// </summary>
        /// <param name="buffer">The buffer to fill of the sound values</param>
        /// <param name="offset">Offset from the beginning of the </param>
        /// <param name="count">The count of values to fill</param>
        /// <returns>The number of values added to the buffer</returns>
        public int Read(float[] buffer, int offset, int count)
        {
            for (var i = offset; i < count; i++)
            {
                buffer[i] = (float)_piano.Play();
            }

            return count;
        }

        public void Dispose() => _waveOut?.Dispose();
    }
}
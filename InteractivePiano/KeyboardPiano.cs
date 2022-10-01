using InteractivePiano.Audio;

namespace InteractivePiano
{
    public class KeyboardPiano
    {
        private PianoAudio _audio;
        private string _keys;

        public KeyboardPiano(PianoAudio pianoAudio, string keys)
        {
            _audio = pianoAudio;
            _keys = keys;
        }

        public int StrikeKey(char key)
        {
            var keyIndex = _keys.IndexOf(key);
            if (keyIndex == -1)
            {
                return -1;
            }

            _audio.AddNote(keyIndex);
            return 0;
        }

        public int RaiseKey(char key)
        {
            var keyIndex = _keys.IndexOf(key);
            if (keyIndex == -1)
            {
                return -1;
            }

            _audio.RemoveNote(keyIndex);
            return 0;
        }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private bool IsValidKey(char key)
        // {
        //     return _keys.IndexOf(key) == -1;
        // }
    }
}
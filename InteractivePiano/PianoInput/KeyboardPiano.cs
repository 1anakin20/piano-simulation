using InteractivePiano.Game;

namespace InteractivePiano.PianoInput
{
    public class KeyboardPiano : PianoInput
    {
        private string _keys;

        public KeyboardPiano(string keys, KeyboardKeysEvents keyboardKeysEvents)
        {
            _keys = keys;
            keyboardKeysEvents.KeyboardKeysPressed += OnKeyboardKeysPressed;
            keyboardKeysEvents.KeyboardKeysReleased += OnKeyboardKeysReleased;
        }

        private void OnKeyboardKeysPressed(object sender, KeysEventArgs args)
        {
            var keys = GetKeysNumbers(args);
            OnPianoKeyPressed(new PianoInputEventArgs(keys));
        }

        private void OnKeyboardKeysReleased(object sender, KeysEventArgs args)
        {
            var keys = GetKeysNumbers(args);
            OnPianoKeyReleased(new PianoInputEventArgs(keys));
        }

        private int[] GetKeysNumbers(KeysEventArgs args)
        {
            int[] keys = new int[args.Keys.Count];
            for (var i = 0; i < args.Keys.Count; i++)
            {
                var key = args.Keys[i];
                var keyIndex = _keys.IndexOf(char.ToLower((char)key));
                if (keyIndex != -1)
                {
                    keys[i] = keyIndex;
                }
            }

            return keys;
        }
    }
}
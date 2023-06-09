﻿using System;
using System.Collections.Generic;
using InteractivePiano.Game;
using Microsoft.Xna.Framework.Input;

namespace InteractivePiano.PianoInput
{
    /// <summary>
    /// Plays the piano with a computer keyboard
    /// </summary>
    public class KeyboardPiano : PianoInput
    {
        private readonly string _keys;
        private readonly KeyboardConverter _keyboardConverter;

        public KeyboardPiano(string keys, KeyboardKeysEvents keyboardKeysEvents)
        {
            _keys = keys;
            _keyboardConverter = new KeyboardConverter(keys);
            keyboardKeysEvents.KeyboardKeysPressed += OnKeyboardKeysPressed;
            keyboardKeysEvents.KeyboardKeysReleased += OnKeyboardKeysReleased;
        }

        private void OnKeyboardKeysPressed(object sender, KeysEventArgs args)
        {
            var keys = GetKeysNumbers(args.Keys);
            OnPianoKeyPressed(new PianoInputEventArgs(keys));
        }

        private void OnKeyboardKeysReleased(object sender, KeysEventArgs args)
        {
            var keys = GetKeysNumbers(args.Keys);
            OnPianoKeyReleased(new PianoInputEventArgs(keys));
        }

        /// <summary>
        /// Convert the keyboard keys to the corresponding piano keys numbers
        /// </summary>
        /// <param name="inputKeys"><seealso cref="Keys"/> List of keys to convert</param>
        /// <returns>The piano key number of all the keys in the same order</returns>
        private int[] GetKeysNumbers(List<Keys> inputKeys)
        {
            if (inputKeys == null) throw new ArgumentNullException(nameof(inputKeys));
            var numberOfKeys = inputKeys.Count;
            var keys = new int?[numberOfKeys];
            int validKeysCounter = 0;
            for (var i = 0; i < numberOfKeys; i++)
            {
                var key = inputKeys[i];
                var charKey = _keyboardConverter.KeyToChar(key);
                if (charKey != null)
                {
                    var keyIndex = _keys.IndexOf((char)charKey!);
                    if (keyIndex != -1)
                    {
                        keys[i] = keyIndex;
                        ++validKeysCounter;
                    }
                }
            }

            var validKeys = new int[validKeysCounter];
            var validKeysIndex = 0;
            foreach (var key in keys)
            {
                if (key != null)
                {
                    validKeys[validKeysIndex] = (int)key!;
                    ++validKeysIndex;
                }
            }

            return validKeys;
        }
    }
}
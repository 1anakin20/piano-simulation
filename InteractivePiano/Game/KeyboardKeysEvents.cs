﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace InteractivePiano.Game
{
    public class KeyboardKeysEvents
    {
        public event KeyEventDelegate KeyboardKeysPressed;
        public event KeyEventDelegate KeyboardKeysReleased;
        private List<Keys> _previousPressedKeys = new List<Keys>();

        public void Update()
        {
            // Get the current state of the keyboard
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();
            var newPressedKeys = new List<Keys>();
            foreach (var key in pressedKeys)
            {
                if (!_previousPressedKeys.Contains(key))
                {
                    newPressedKeys.Add(key);
                }
            }

            OnKeysPressed(new KeysEventArgs(newPressedKeys));

            var releasedKeys = new List<Keys>();
            foreach (var key in _previousPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    releasedKeys.Add(key);
                }
            }

            OnKeysReleased(new KeysEventArgs(releasedKeys));

            _previousPressedKeys = new List<Keys>(pressedKeys);
        }

        protected virtual void OnKeysPressed(KeysEventArgs args)
        {
            KeyboardKeysPressed?.Invoke(this, args);
        }

        protected virtual void OnKeysReleased(KeysEventArgs args)
        {
            KeyboardKeysReleased?.Invoke(this, args);
        }
    }

    public delegate void KeyEventDelegate(object sender, KeysEventArgs args);

    public class KeysEventArgs : EventArgs
    {
        public List<Keys> Keys { get; }

        public KeysEventArgs(List<Keys> keys)
        {
            Keys = keys;
        }
    }
}
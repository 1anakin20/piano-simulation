using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace InteractivePiano.Game
{
    /// <summary>
    /// Sends events when the user presses a key on the keyboard.
    /// </summary>
    public class KeyboardKeysEvents
    {
        /// <summary>
        /// Event that is fired when a new key is pressed.
        /// </summary>
        public event KeyEventDelegate KeyboardKeysPressed;

        /// <summary>
        /// Event that is fired when a key is released.
        /// </summary>
        public event KeyEventDelegate KeyboardKeysReleased;

        private List<Keys> _previousPressedKeys = new List<Keys>();

        /// <summary>
        /// Checks if any key was pressed or released and sends the appropriate event.
        /// Recommended to call this method in the Update method of the game.
        /// </summary>
        public void Update()
        {
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();
            var newPressedKeys = new List<Keys>();
            // Check for newly pressed keys
            foreach (var key in pressedKeys)
            {
                if (!_previousPressedKeys.Contains(key))
                {
                    newPressedKeys.Add(key);
                }
            }

            OnKeysPressed(new KeysEventArgs(newPressedKeys));

            // Check for newly released keys
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

        /// <summary>
        /// Sends the <see cref="KeyboardKeysPressed"/> event.
        /// </summary>
        /// <param name="args"><see cref="KeysEventArgs"/> Sets the arguments to send</param>
        protected virtual void OnKeysPressed(KeysEventArgs args)
        {
            KeyboardKeysPressed?.Invoke(this, args);
        }

        /// <summary>
        /// Sends the <see cref="KeyboardKeysReleased"/> event.
        /// </summary>
        /// <param name="args"><see cref="KeysEventArgs"/> Sets the arguments to send</param>
        protected virtual void OnKeysReleased(KeysEventArgs args)
        {
            KeyboardKeysReleased?.Invoke(this, args);
        }
    }

    /// <summary>
    /// Delegate for the <see cref="KeyboardKeysEvents"/> events.
    /// </summary>
    public delegate void KeyEventDelegate(object sender, KeysEventArgs args);

    /// <summary>
    /// Arguments for the <see cref="KeyboardKeysEvents"/> events.
    /// </summary>
    public class KeysEventArgs : EventArgs
    {
        /// <summary>
        /// List of keys that were pressed or released.
        /// </summary>
        public List<Keys> Keys { get; }

        /// <summary>
        /// Constructs the <see cref="KeysEventArgs"/> class.
        /// </summary>
        /// <param name="keys"><see cref="Keys"/> List of keys pressed or released</param>
        public KeysEventArgs(List<Keys> keys)
        {
            Keys = keys;
        }
    }
}
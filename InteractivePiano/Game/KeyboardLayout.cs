using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace InteractivePiano.Game
{
    /// <summary>
    /// Converts the monogame <see cref="Keys"/> to a valid piano keyboard char
    /// </summary>
    public class KeyboardConverter
    {
        private readonly Dictionary<Keys, char> _bindings = new Dictionary<Keys, char>();
        private readonly string _keys;

        /// <summary>
        /// Converts the monogame <see cref="Keys"/> to a character
        /// </summary>
        /// <param name="keys">Piano keys</param>
        public KeyboardConverter(string keys)
        {
            _keys = keys;
            MakeBindings();
        }

        /// <summary>
        /// Sets the binding of keys in a dictionary
        /// </summary>
        private void MakeBindings()
        {
            foreach (var key in _keys)
            {
                switch (key)
                {
                    case '-':
                        _bindings.Add(Keys.OemMinus, '-');
                        break;
                    case '[':
                        _bindings.Add(Keys.OemOpenBrackets, '[');
                        break;
                    case '=':
                        _bindings.Add(Keys.OemPlus, '=');
                        break;
                    case ',':
                        _bindings.Add(Keys.OemComma, ',');
                        break;
                    case '.':
                        _bindings.Add(Keys.OemPeriod, '.');
                        break;
                    case ';':
                        _bindings.Add(Keys.OemSemicolon, ';');
                        break;
                    case '/':
                        _bindings.Add(Keys.OemQuestion, '/');
                        break;
                    case '\\':
                        _bindings.Add(Keys.OemQuotes, '\\');
                        break;
                    case ' ':
                        _bindings.Add(Keys.Space, ' ');
                        break;
                    case '2':
                    {
                        var keyStr = "D" + key;
                        _bindings.Add((Keys)System.Enum.Parse(typeof(Keys), keyStr), key);
                        break;
                    }
                    case '4':
                    {
                        var keyStr = "D" + key;
                        _bindings.Add((Keys)System.Enum.Parse(typeof(Keys), keyStr), key);
                        break;
                    }
                    case '5':
                    {
                        var keyStr = "D" + key;
                        _bindings.Add((Keys)System.Enum.Parse(typeof(Keys), keyStr), key);
                        break;
                    }
                    case '7':
                    {
                        var keyStr = "D" + key;
                        _bindings.Add((Keys)System.Enum.Parse(typeof(Keys), keyStr), key);
                        break;
                    }
                    case '8':
                    {
                        var keyStr = "D" + key;
                        _bindings.Add((Keys)System.Enum.Parse(typeof(Keys), keyStr), key);
                        break;
                    }
                    case '9':
                    {
                        var keyStr = "D" + key;
                        _bindings.Add((Keys)System.Enum.Parse(typeof(Keys), keyStr), key);
                        // keyLists.Add(key, key);
                        break;
                    }
                    default:
                    {
                        if (key > 'a' && key < 'z')
                        {
                            _bindings.Add((Keys)Enum.Parse(typeof(Keys), key.ToString().ToUpper()), key);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Convert a <see cref="Keys"/> to a char
        /// </summary>
        /// <param name="key">A <see cref="Keys"/></param>
        /// <returns>The corresponding character, or null if there is none</returns>
        public char? KeyToChar(Keys key)
        {
            if (_bindings.ContainsKey(key))
            {
                return _bindings[key];
            }

            return null;
        }
    }
}
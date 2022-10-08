#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractivePiano.Audio;
using InteractivePiano.Midi;
using InteractivePiano.PianoInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using PianoSimulation;

namespace InteractivePiano.Game
{
    /// <summary>
    /// Main game class. Handles the game logic
    /// </summary>
    public class InteractivePiano : Microsoft.Xna.Framework.Game
    {
        private const string Keys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ";
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PianoAudio _audio;
        private const int SampleRate = 44100;
        private KeyboardKeysEvents _keyboardKeysEvents;
        private PianoInput.PianoInput _pianoInput;
        private GameState _gameState;
        private Desktop _desktop;
        private GameObject.Piano _virtualPiano;
        private SpriteFont _font;
        private readonly List<Keys> _pressedKeys;

        public InteractivePiano()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _gameState = GameState.Menu;
            _pressedKeys = new List<Keys>();
        }

        /// <summary>
        /// Creates a new <see cref="Piano"/>
        /// Warning: It is important to call this method before using the piano
        /// </summary>
        /// <param name="keys">Tbe number of keys of the piano</param>
        /// <param name="startingFrequency">The frequency of the first keys of the piano</param>
        private void InitialisePiano(int keys, double startingFrequency)
        {
            var piano = new Piano(keys, SampleRate, startingFrequency: startingFrequency);
            _audio = new PianoAudio(piano, SampleRate);
            var blackKeyTexture = Content.Load<Texture2D>("black_key");
            var whiteKeyTexture = Content.Load<Texture2D>("white_key");
            _virtualPiano = new GameObject.Piano(_spriteBatch, whiteKeyTexture, blackKeyTexture, Vector2.Zero, keys, 0);
        }

        #region game logic

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("font");
            MyraEnvironment.Game = this;
            _desktop = new Desktop();
            DrawMenu();
        }

        protected override void Update(GameTime gameTime)
        {
            // show the menu when a key to exit is pressed, if already on the menu quit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Exit();
            }

            if (_gameState == GameState.Playing)
            {
                // It is only necessary to get the keyboard inputs if we are playing the piano with a computer keyboard
                if (_pianoInput is PianoInput.KeyboardPiano)
                {
                    _keyboardKeysEvents.Update();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (_gameState == GameState.Menu)
            {
                _desktop.Render();
            }
            else
            {
                _spriteBatch.Begin();
                _virtualPiano.DrawKeys();

                // Shows the current note being played
                var pressedKeysBuilder = new StringBuilder();
                foreach (var key in _pressedKeys)
                {
                    pressedKeysBuilder.Append(key);
                }

                _spriteBatch.DrawString(_font, pressedKeysBuilder.ToString(), new Vector2(100, 100), Color.Black);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        #endregion


        #region menu

        /// <summary>
        /// Shows the menu to select the playing mode. Computer keyboard <seealso cref="KeyboardPiano"/> or midi <seealso cref="MidiPiano"/>
        /// </summary>
        private void DrawMenu()
        {
            var grid = new Grid
            {
                RowSpacing = 10,
                ColumnSpacing = 10
            };

            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            var keyboardButton = new TextButton
            {
                Text = "Keyboard",
                GridColumn = 0,
                GridRow = 0
            };


            // Setup the input type to keyboard
            keyboardButton.Click += (sender, args) =>
            {
                InitialisePiano(Keys.Length, 440);
                _keyboardKeysEvents = new KeyboardKeysEvents();
                _keyboardKeysEvents.KeyboardKeysPressed += OnKeyboardKeysPressed;
                _keyboardKeysEvents.KeyboardKeysReleased += OnKeyboardKeysReleased;
                _pianoInput = new PianoInput.KeyboardPiano(Keys, _keyboardKeysEvents);
                _pianoInput.PianoKeyPressed += OnPianoInputOnPianoInputKeyPressed;
                _pianoInput.PianoKeyReleased += OnPianoInputOnPianoInputKeyReleased;
                _gameState = GameState.Playing;
            };
            grid.Widgets.Add(keyboardButton);

            var midiButton = new TextButton
            {
                Text = "MIDI",
                GridColumn = 1,
                GridRow = 0
            };

            midiButton.Click += (sender, args) => { ShowMidiMenu(); };
            grid.Widgets.Add(midiButton);
            _desktop.Root = grid;
        }

        /// <summary>
        /// Shows the menu to select the midi input
        /// </summary>
        private void ShowMidiMenu()
        {
            var midiDevices = MidiUtils.ListDevicesNames();
            var stack = new VerticalStackPanel
            {
                Spacing = 10
            };

            var midiDevicesCombo = new ComboBox()
            {
                GridColumn = 0,
                GridRow = 0,
                GridColumnSpan = 2
            };
            // In case there aren't any Midi devices let the user know, and just show a back button
            if (!midiDevices.Any())
            {
                midiDevicesCombo.Items.Add(new ListItem("No MIDI devices found"));
            }
            else
            {
                foreach (var deviceName in midiDevices)
                {
                    midiDevicesCombo.Items.Add(new ListItem(deviceName));
                }

                midiDevicesCombo.SelectedIndex = 0;

                var acceptButton = new TextButton
                {
                    Text = "accept",
                };

                acceptButton.Click += (sender, args) =>
                {
                    InitialisePiano(127, 8.18);
                    var selectedDevice = midiDevicesCombo.SelectedIndex ?? 0;
                    _pianoInput = new MidiPiano(selectedDevice);
                    _pianoInput.PianoKeyPressed += OnPianoInputOnPianoInputKeyPressed;
                    _pianoInput.PianoKeyReleased += OnPianoInputOnPianoInputKeyReleased;
                    _gameState = GameState.Playing;
                };
                stack.Widgets.Add(acceptButton);
            }

            stack.Widgets.Add(midiDevicesCombo);

            var backButton = new TextButton
            {
                Text = "back",
            };

            backButton.Click += (sender, args) => { DrawMenu(); };
            stack.Widgets.Add(backButton);

            _desktop.Root = stack;
        }

        #endregion

        #region event delegates

        private void OnKeyboardKeysReleased(object sender, KeysEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _pressedKeys.Remove(key);
            }
        }

        private void OnKeyboardKeysPressed(object sender, KeysEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _pressedKeys.Add(key);
            }
        }

        private void OnPianoInputOnPianoInputKeyReleased(object sender, PianoInputEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _audio.RemoveNote(key);
                _virtualPiano.ReleaseKey(key);
            }
        }

        private void OnPianoInputOnPianoInputKeyPressed(object sender, PianoInputEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _audio.AddNote(key);
                _virtualPiano.PressKey(key);
            }
        }

        #endregion
    }
}
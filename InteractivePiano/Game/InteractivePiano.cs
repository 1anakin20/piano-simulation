using System.Linq;
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

        public InteractivePiano()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _gameState = GameState.MENU;
        }

        private void OnPianoInputOnPianoInputKeyReleased(object sender, PianoInputEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _audio.RemoveNote(key);
            }
        }

        private void OnPianoInputOnPianoInputKeyPressed(object sender, PianoInputEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _audio.AddNote(key);
            }
        }

        private void InitialisePiano(int keys, double startingFrequency)
        {
            var piano = new Piano(keys, SampleRate, startingFrequency: startingFrequency);
            _audio = new PianoAudio(piano, SampleRate);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MyraEnvironment.Game = this;
            _desktop = new Desktop();
            DrawMenu();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // show the menu when a key to exit is pressed, if already on the menu quit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Exit();
            }

            if (_gameState == GameState.PLAYING)
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
            if (_gameState == GameState.MENU)
            {
                _desktop.Render();
            }

            base.Draw(gameTime);
        }

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
                _pianoInput = new PianoInput.KeyboardPiano(Keys, _keyboardKeysEvents);
                _pianoInput.PianoKeyPressed += OnPianoInputOnPianoInputKeyPressed;
                _pianoInput.PianoKeyReleased += OnPianoInputOnPianoInputKeyReleased;
                _gameState = GameState.PLAYING;
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
                    _gameState = GameState.PLAYING;
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
    }
}
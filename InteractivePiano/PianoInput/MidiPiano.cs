using NAudio.Midi;

namespace InteractivePiano.PianoInput
{
    /// <summary>
    /// Plays the piano by using a midi device
    /// </summary>
    public class MidiPiano : PianoInput
    {
        /// <summary>
        /// Constructs a new <see cref="MidiPiano"/>
        /// </summary>
        /// <param name="deviceNumber">The number of the midi device index. Starts at zero</param>
        public MidiPiano(int deviceNumber)
        {
            var midiIn = new MidiIn(deviceNumber);
            midiIn.MessageReceived += MidiInOnMessageReceived;
            midiIn.Start();
        }

        private void MidiInOnMessageReceived(object sender, MidiInMessageEventArgs e)
        {
            // After receiving a midi message, send the appropriate note event to the piano
            if (e.MidiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
                var noteOnEvent = (NoteEvent)e.MidiEvent;
                OnPianoKeyPressed(new PianoInputEventArgs(new[] { noteOnEvent.NoteNumber }));
            }
            else if (e.MidiEvent.CommandCode == MidiCommandCode.NoteOff)
            {
                var noteOffEvent = (NoteEvent)e.MidiEvent;
                OnPianoKeyReleased(new PianoInputEventArgs(new[] { noteOffEvent.NoteNumber }));
            }
        }
    }
}
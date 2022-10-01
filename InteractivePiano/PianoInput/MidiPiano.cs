using NAudio.Midi;

namespace InteractivePiano.PianoInput
{
    public class MidiPiano : PianoInput
    {
        private MidiIn _midiIn;

        public MidiPiano(int deviceNumber)
        {
            _midiIn = new MidiIn(deviceNumber);
            _midiIn.MessageReceived += MidiInOnMessageReceived;
            _midiIn.Start();
        }

        private void MidiInOnMessageReceived(object sender, MidiInMessageEventArgs e)
        {
            if (e.MidiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
                var noteOnEvent = (NoteEvent)e.MidiEvent;
                OnPianoKeyPressed(new PianoInputEventArgs(new int[] { noteOnEvent.NoteNumber }));
            }
            else if (e.MidiEvent.CommandCode == MidiCommandCode.NoteOff)
            {
                var noteOffEvent = (NoteEvent)e.MidiEvent;
                OnPianoKeyReleased(new PianoInputEventArgs(new int[noteOffEvent.NoteNumber]));
            }
        }
    }
}
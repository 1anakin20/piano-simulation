using System.Collections.Generic;
using NAudio.Midi;

namespace InteractivePiano.Midi
{
    /// <summary>
    /// Utilities to help with MIDI
    /// </summary>
    public static class MidiUtils
    {
        /// <summary>
        /// Lists all MIDI devices available
        /// </summary>
        /// <returns>List of names of all the midi devices. It is in the same order of their midi ID</returns>
        public static List<string> ListDevicesNames()
        {
            var names = new List<string>();
            for (int i = 0; i < MidiIn.NumberOfDevices; i++)
            {
                names.Add(MidiIn.DeviceInfo(i).ProductName);
            }

            return names;
        }
    }
}
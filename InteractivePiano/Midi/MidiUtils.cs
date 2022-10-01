using System.Collections.Generic;
using NAudio.Midi;

namespace InteractivePiano.Midi
{
    public static class MidiUtils
    {
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
# piano-simulation
Done for a Computer Science college program.

# Why is this better than the others
Code was given to play audio using the NAudio library. However it was glitchy. It did not allow playing the audio in real time properly.
A key was pressed, then it was played. If another key was pressed during the playback of a note, it was necessary to wait until the audio stopped playing.
It also suffered from glitches such as notes cut early.

This project rewrote the audio code to allow smooth real time playback and be able to play multiple notes on top of already playing notes.
On top, this project can read from a MIDI device to play the piano. So, you can connect a real piano to your computer and play it!

# Requirements
* Windows only
* Dotnet SDK 3.1

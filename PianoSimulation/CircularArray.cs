namespace PianoSimulation
{
    public class CircularArray : IRingBuffer
    {
        public int Length { get; }
        private readonly double[] _buffer;
        private int _head;
        
        public CircularArray(int length)
        {
            Length = length;
            _buffer = new double[length];
            _head = 0;
        }
        
        public double Shift(double value)
        {
            var oldValue = _buffer[_head];
            _buffer[_head] = value;
            _head = (_head + 1) % Length;
            return oldValue;
        }

        public double this[int index] => _buffer[(index + _head) % Length];

        public void Fill(double[] array)
        {
            for (int i = 0; i < Length; i++) {
                _buffer[i] = array[i];
            }
        }
    }
}
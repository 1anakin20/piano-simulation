namespace PianoSimulation
{
    public class KeyControl
    {
        private readonly double _decay;
        private readonly double _releaseDecay;
        private readonly IMusicalString _key;
        private bool _isPlaying;
        private bool _isFading;
        private const float TotalFadeCycles = 44100 * 5;
        private int _fadeCycles;

        public KeyControl(PianoWire pianoWire, double decay = 0.996, double releaseDecay = 0.9)
        {
            _key = pianoWire;
            _decay = decay;
            _releaseDecay = releaseDecay;
            _isPlaying = false;
            _isFading = false;
            _fadeCycles = 0;
        }

        public void Strike()
        {
            _isPlaying = true;
            _isFading = false;
            _key.Strike();
        }

        public void ReleaseKey()
        {
            _isPlaying = false;
            _isFading = true;
        }

        public double Sample()
        {
            if (_isPlaying)
            {
                return _key.Sample(_decay);
            }

            if (_isFading)
            {
                double sample = _key.Sample(_releaseDecay);
                _fadeCycles++;
                if (_fadeCycles >= TotalFadeCycles)
                {
                    _isFading = false;
                    _isPlaying = false;
                    _fadeCycles = 0;
                }

                return sample;
            }

            return 0;
        }
    }
}
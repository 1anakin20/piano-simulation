namespace PianoSimulation
{
    public class KeyControl
    {
        private readonly double _decay;
        private readonly double _releaseDecay;
        private readonly IMusicalString _key;
        public bool IsPlaying { get; private set; }
        private bool _isFading;
        private const float TotalFadeCycles = 44100 * 5;
        private int _fadeCycles;

        public KeyControl(PianoWire pianoWire, double decay = 0.996, double releaseDecay = 0.9)
        {
            _key = pianoWire;
            _decay = decay;
            _releaseDecay = releaseDecay;
            IsPlaying = false;
            _isFading = false;
            _fadeCycles = 0;
        }

        public void Strike()
        {
            IsPlaying = true;
            _isFading = false;
            _key.Strike();
        }

        public void ReleaseKey()
        {
            IsPlaying = false;
            _isFading = true;
        }

        public double Sample()
        {
            if (IsPlaying)
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
                    IsPlaying = false;
                    _fadeCycles = 0;
                }

                return sample;
            }

            return 0;
        }
    }
}
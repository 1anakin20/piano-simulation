using System;

namespace PianoSimulation
{
    public class KeyControl
    {
        private readonly double _decay;
        private readonly double _releaseDecay;
        private readonly IMusicalString _key;
        public bool IsPlaying { get; private set; }
        public bool IsFading { get; private set; }

        public KeyControl(PianoWire pianoWire, double decay = 0.996, double releaseDecay = 0.96)
        {
            _key = pianoWire;
            _decay = decay;
            _releaseDecay = releaseDecay;
            IsPlaying = false;
            IsFading = false;
        }

        public void Strike()
        {
            IsPlaying = true;
            IsFading = false;
            _key.Strike();
        }

        public void ReleaseKey()
        {
            IsPlaying = false;
            IsFading = true;
        }

        public double Sample()
        {
            if (IsPlaying)
            {
                return _key.Sample(_decay);
            }

            if (IsFading)
            {
                double sample = _key.Sample(_releaseDecay);
                if (Math.Abs(sample) < 0.000001)
                {
                    IsFading = false;
                }

                return sample;
            }

            return 0;
        }
    }
}
namespace Pentagon.Extensions.IO.Wav.Sampling.Samples {
    using System;

    public class Sample8Bit : ISample
    {
        readonly double _sample;

        public Sample8Bit(double sample)
        {
            _sample = sample;
        }

        /// <inheritdoc />
        public int Length => 1;

        /// <inheritdoc />
        public byte[] GetBytes() => BitConverter.GetBytes((byte) _sample);
    }
}
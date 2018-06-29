namespace Pentagon.Extensions.IO.Wav.Sampling.Samples {
    using System;

    public class Sample32Bit : ISample
    {
        readonly double _sample;

        public Sample32Bit(double sample)
        {
            _sample = sample;
        }

        /// <inheritdoc />
        public int Length => 4;

        /// <inheritdoc />
        public byte[] GetBytes() => BitConverter.GetBytes((int) _sample);
    }
}
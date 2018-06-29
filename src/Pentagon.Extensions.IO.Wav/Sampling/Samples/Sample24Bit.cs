// -----------------------------------------------------------------------
//  <copyright file="Sample24Bit.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling.Samples
{
    public class Sample24Bit : ISample
    {
        readonly double _sample;

        public Sample24Bit(double sample)
        {
            _sample = sample;
        }

        /// <inheritdoc />
        public int Length => 3;

        /// <inheritdoc />
        public byte[] GetBytes()
        {
            var num = (Signed24BitNumber) (short) _sample;
            return num.GetBytes();
        }
    }
}
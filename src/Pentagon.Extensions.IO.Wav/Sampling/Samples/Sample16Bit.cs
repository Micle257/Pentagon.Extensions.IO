// -----------------------------------------------------------------------
//  <copyright file="Sample16Bit.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling.Samples
{
    using System;

    public class Sample16Bit : ISample
    {
        readonly double _sample;

        public Sample16Bit(double sample)
        {
            _sample = sample;
        }

        /// <inheritdoc />
        public int Length => 2;

        /// <inheritdoc />
        public byte[] GetBytes() => BitConverter.GetBytes((short) _sample);
    }
}
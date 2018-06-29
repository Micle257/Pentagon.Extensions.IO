// -----------------------------------------------------------------------
//  <copyright file="Sample64BitFloating.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling.Samples
{
    using System;

    public class Sample64BitFloating : ISample
    {
        readonly double _sample;

        public Sample64BitFloating(double sample)
        {
            _sample = sample;
        }

        /// <inheritdoc />
        public int Length => 8;

        /// <inheritdoc />
        public byte[] GetBytes() => BitConverter.GetBytes(_sample);
    }
}
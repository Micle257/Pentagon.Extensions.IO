// -----------------------------------------------------------------------
//  <copyright file="Sample32BitFloating.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling.Samples
{
    using System;

    public class Sample32BitFloating : ISample
    {
        readonly double _sample;

        public Sample32BitFloating(double sample)
        {
            _sample = sample;
        }

        /// <inheritdoc />
        public int Length => 4;

        /// <inheritdoc />
        public byte[] GetBytes() => BitConverter.GetBytes((float) _sample);
    }
}
// -----------------------------------------------------------------------
//  <copyright file="ZeroSample.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling.Samples
{
    using System.Linq;

    public class ZeroSample : ISample
    {
        /// <inheritdoc />
        public ZeroSample(int length)
        {
            Length = length;
        }

        /// <inheritdoc />
        public int Length { get; }

        /// <inheritdoc />
        public byte[] GetBytes() => Enumerable.Repeat(new byte(), Length).ToArray();
    }
}
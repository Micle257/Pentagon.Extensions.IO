// -----------------------------------------------------------------------
//  <copyright file="SampleSize.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    /// <summary> Represents the size of a sample. </summary>
    public enum SampleSize
    {
        Unspecified,

        /// <summary> The 8 bit size. </summary>
        Bit8 = 8,

        /// <summary> The 16 bit size. </summary>
        Bit16 = 16,

        /// <summary> The 24 bit size. </summary>
        Bit24 = 24,

        Bit32 = 32,

        Bit64 = 64
    }
}
// -----------------------------------------------------------------------
//  <copyright file="FormatCode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    public enum FormatCode : ushort
    {
        Unspecified = 0,
        PCM = 1,
        IEEE = 3,
        ALaw = 6,
        MuLaw = 7,
        Extensible = 0xFFFE
    }
}
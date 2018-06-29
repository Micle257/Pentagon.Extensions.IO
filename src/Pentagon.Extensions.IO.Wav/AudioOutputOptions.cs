// -----------------------------------------------------------------------
//  <copyright file="AudioOutputOptions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using Sampling;

    public class AudioOutputOptions
    {
        public double? SamplingFrequency { get; set; }

        public int BitDepth { get; set; } = 32;

        public FormatCode Type { get; set; } = FormatCode.IEEE;

        public double OffsetBeginningDuration { get; set; } = 0;

        public SoundReproduction Reproduction { get; set; } = SoundReproduction.Stereo;
    }
}
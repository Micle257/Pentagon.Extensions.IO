// -----------------------------------------------------------------------
//  <copyright file="SoundSampleType.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    public struct SoundSampleType
    {
        public SoundSampleType(SampleSize sampleSize, FormatCode formatCode)
        {
            SampleSize = sampleSize;
            FormatCode = formatCode;
        }

        public SampleSize SampleSize { get; }

        public FormatCode FormatCode { get; }

        public int ByteLength => (int) SampleSize / 2;
    }
}
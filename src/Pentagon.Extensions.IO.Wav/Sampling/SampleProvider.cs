// -----------------------------------------------------------------------
//  <copyright file="SampleProvider.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    using Microsoft.Extensions.Options;
    using Samples;

    public class SampleProvider : IByteSampleProvider
    {
        readonly SoundSampleType _sampleType;

        public SampleProvider(IOptions<AudioOutputOptions> options)
        {
            var value = options?.Value ?? new AudioOutputOptions();
            _sampleType = new SoundSampleType((SampleSize) value.BitDepth, value.Type);
        }

        public ISample Create(double sample)
        {
            if (_sampleType.FormatCode == FormatCode.PCM)
            {
                switch (_sampleType.SampleSize)
                {
                    case SampleSize.Bit8:
                        return new Sample8Bit(sample);

                    case SampleSize.Bit16:
                        return new Sample16Bit(sample);

                    case SampleSize.Bit24:
                        return new Sample24Bit(sample);

                    case SampleSize.Bit32:
                        return new Sample32Bit(sample);
                }
            }
            else if (_sampleType.FormatCode == FormatCode.IEEE)
            {
                switch (_sampleType.SampleSize)
                {
                    case SampleSize.Bit32:
                        return new Sample32BitFloating(sample);

                    case SampleSize.Bit64:
                        return new Sample64BitFloating(sample);
                }
            }

            return null;
        }
    }
}
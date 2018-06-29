// -----------------------------------------------------------------------
//  <copyright file="SampleValueModifier.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    using System;
    using System.Linq;
    using Maths;
    using Microsoft.Extensions.Options;

    public class SampleValueModifier : ISampleValueModifier
    {
        readonly SoundSampleType _sampleType;

        public SampleValueModifier(IOptions<AudioOutputOptions> options)
        {
            var value = options?.Value ?? new AudioOutputOptions();
            _sampleType = new SoundSampleType((SampleSize) value.BitDepth, value.Type);
        }

        public double[] Adjust(double[] samples, double factor = 0.9)
        {
            if (_sampleType.FormatCode == FormatCode.PCM)
            {
                switch (_sampleType.SampleSize)
                {
                    //case SampleSize.Bit8:
                    //    return new IWavFileSample<byte>();

                    case SampleSize.Bit16:
                        return Adjust16bit(samples, factor);

                    //case SampleSize.Bit24:
                    //    return new IWavFileSample<Signed24BitNumber>();

                    //case SampleSize.Bit32:
                    //    return new IWavFileSample<int>();
                }
            }
            else if (_sampleType.FormatCode == FormatCode.IEEE)
            {
                switch (_sampleType.SampleSize)
                {
                    case SampleSize.Bit32:
                        return Adjust32bitFloating(samples, factor);

                    case SampleSize.Bit64:
                        return Adjust64bitFloating(samples, factor);
                }
            }

            throw new NotSupportedException();
        }

        public double[] Adjust64bitFloating(double[] samples, double factor)
        {
            var max = samples.Abs().Max();

            var result = new double[samples.Length];

            for (var i = 0; i < samples.Length; i++)
                result[i] = samples[i] / max * factor;

            return result;
        }

        public double[] Adjust32bitFloating(double[] samples, double factor)
        {
            var max = samples.Abs().Max();

            var result = new double[samples.Length];

            for (var i = 0; i < samples.Length; i++)
                result[i] = (float) (samples[i] / max * factor);

            return result;
        }

        public double[] Adjust16bit(double[] samples, double factor)
        {
            var max = samples.Abs().Max();

            var result = new double[samples.Length];

            for (var i = 0; i < samples.Length; i++)
                result[i] = (short) (samples[i] / max * Math.Pow(2, 15) * factor);

            return result;
        }
    }
}
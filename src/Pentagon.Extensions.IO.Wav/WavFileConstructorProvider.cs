// -----------------------------------------------------------------------
//  <copyright file="WavFileConstructorProvider.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using System;
    using Microsoft.Extensions.Options;
    using Sampling;

    public class WavFileConstructorProvider : IWavFileConstructorProvider
    {
        readonly AudioOutputOptions _options;

        public WavFileConstructorProvider(IOptions<AudioOutputOptions> options)
        {
            _options = options?.Value ?? new AudioOutputOptions();
        }

        /// <inheritdoc />
        public IWavFileConstructor Create(int sampleRate, TimeSpan duration)
        {
            var type = new SoundSampleType((SampleSize) _options.BitDepth, _options.Type);

            var constructor = new WavFileConstructor(sampleRate, type, _options.Reproduction, duration);
            return constructor;
        }
    }
}
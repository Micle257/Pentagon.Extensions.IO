// -----------------------------------------------------------------------
//  <copyright file="IWavFileConstructor.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using System.Collections.Generic;
    using Sampling;

    public interface IWavFileConstructor
    {
        FormatCode FormatCode { get; }
        uint FormatChunkSize { get; }
        SoundReproduction Reproduction { get; }
        ushort ChannelsCount { get; }
        SoundSampleType SampleType { get; }
        int SampleRate { get; }
        uint SamplesCount { get; }
        ushort BitsPerSample { get; }
        SampleSize SampleSize { get; }
        uint DataChunkSize { get; }
        IDictionary<ushort, IList<ISample>> ChannelSampleMap { get; }
        byte[] GetFileBytes();
        void SetChannelSamples(ushort channelNumber, IList<ISample> byteSamples);
    }
}
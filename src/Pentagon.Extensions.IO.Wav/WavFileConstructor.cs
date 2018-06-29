// -----------------------------------------------------------------------
//  <copyright file="WavFileConstructor.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Sampling;
    using Sampling.Samples;

    public class WavFileConstructor : IWavFileConstructor
    {
        public WavFileConstructor(int sampleRate, SoundSampleType sampleType, SoundReproduction reproduction, TimeSpan duration)
        {
            SampleRate = sampleRate;
            Reproduction = reproduction;
            SampleType = sampleType;
            ValidateFormatCode(sampleType.FormatCode);
            SamplesCount = (uint) (SampleRate * duration.TotalSeconds);
        }

        public FormatCode FormatCode => SampleType.FormatCode;

        public uint FormatChunkSize
        {
            get
            {
                switch (FormatCode)
                {
                    case FormatCode.PCM:
                        return 16;
                    case FormatCode.IEEE:
                        return 18;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public SoundReproduction Reproduction { get; }
        public ushort ChannelsCount => (ushort) Reproduction;

        public SoundSampleType SampleType { get; }
        public int SampleRate { get; }

        public uint SamplesCount { get; }
        public ushort BitsPerSample => (ushort) SampleSize;

        public SampleSize SampleSize => SampleType.SampleSize;
        public uint DataChunkSize => SamplesCount * ChannelsCount * BitsPerSample / 8;

        public IDictionary<ushort, IList<ISample>> ChannelSampleMap { get; } = new ConcurrentDictionary<ushort, IList<ISample>>();

        public byte[] GetFileBytes()
        {
            var list = new List<byte>(ChunkDescriptor());
            list.AddRange(GetFormatSubChunk());
            list.AddRange(GetFactSubChunk());
            list.AddRange(DataSubChunk());
            return list.ToArray();
        }

        public void SetChannelSamples(ushort channelNumber, IList<ISample> byteSamples)
        {
            if (byteSamples.Count > SamplesCount)
                throw new ArgumentOutOfRangeException(nameof(byteSamples), message: "The number of data samples is greaten than allocated size.");

            if (channelNumber < 1 || channelNumber > ChannelsCount)
                throw new ArgumentOutOfRangeException(nameof(channelNumber));

            if (!ChannelSampleMap.ContainsKey(channelNumber))
                ChannelSampleMap.Add(channelNumber, byteSamples);
            else
                ChannelSampleMap[channelNumber] = byteSamples;
        }

        void ValidateFormatCode(FormatCode formatCode)
        {
            if (!formatCode.IsAnyEqual(FormatCode.PCM, FormatCode.IEEE))
                throw new NotSupportedException(message: "Only the PCM and IEEE format code for wav file construction are supported.");
        }

        List<byte> ChunkDescriptor()
        {
            var wr = new List<byte>();

            wr.AddRange(Encoding.UTF8.GetBytes(s: "RIFF"));
            wr.AddRange(BitConverter.GetBytes(20 + FormatChunkSize + DataChunkSize));
            wr.AddRange(Encoding.UTF8.GetBytes(s: "WAVE"));
            return wr;
        }

        List<byte> GetFormatSubChunk()
        {
            var wr = new List<byte>();
            wr.AddRange(Encoding.UTF8.GetBytes(s: "fmt "));
            wr.AddRange(BitConverter.GetBytes(FormatChunkSize)); // Subchunk1Size 4
            wr.AddRange(BitConverter.GetBytes((ushort) FormatCode)); // AudioFormat 2
            wr.AddRange(BitConverter.GetBytes(ChannelsCount)); // NumChannels 2
            wr.AddRange(BitConverter.GetBytes(SampleRate)); // SampleRate 4
            wr.AddRange(BitConverter.GetBytes((uint) (ChannelsCount * SampleRate * BitsPerSample / 8))); // ByteRate 4
            wr.AddRange(BitConverter.GetBytes((ushort) (BitsPerSample / 8 * ChannelsCount))); // BlockAlign 2
            wr.AddRange(BitConverter.GetBytes(BitsPerSample)); // BitsPerSample 2
            if (FormatCode == FormatCode.IEEE)
                wr.AddRange(BitConverter.GetBytes((ushort) 0));
            return wr;
        }

        List<byte> GetFactSubChunk()
        {
            var chunk = new List<byte>();
            if (FormatCode == FormatCode.IEEE)
            {
                chunk.AddRange(Encoding.UTF8.GetBytes(s: "fact"));
                chunk.AddRange(BitConverter.GetBytes((uint) 4));
                chunk.AddRange(BitConverter.GetBytes(ChannelsCount * SamplesCount));
            }

            return chunk;
        }

        List<byte> DataSubChunk()
        {
            var wr = new List<byte>();
            wr.AddRange(Encoding.UTF8.GetBytes(s: "data"));
            wr.AddRange(BitConverter.GetBytes(DataChunkSize));

            var unifiedData = GetUnifiedDataSampled();
            for (var i = 0; i < SamplesCount; i++)
            {
                var bytes = unifiedData[i];
                foreach (var channelSample in bytes)
                {
                    foreach (var b in channelSample.GetBytes())
                        wr.Add(b);
                }
            }

            if (wr.Count % 2 == 1)
                wr.Add(new byte());

            return wr;
        }

        IList<ISample[]> GetUnifiedDataSampled()
        {
            if (ChannelSampleMap.Count != ChannelsCount)
                throw new ArgumentException(message: "Provided channel sample data doesn't match channel count.");

            var result = new List<ISample[]>();

            for (var i = 0; i < SamplesCount; i++)
            {
                var array = new ISample[ChannelsCount];
                result.Add(array);
            }

            var index = 0;
            foreach (var samples in ChannelSampleMap.OrderBy(pair => pair.Key).Select(a => a.Value))
            {
                for (var i = 0; i < SamplesCount; i++)
                {
                    if (i < samples.Count)
                        result[i][index] = samples[i];
                    else
                        result[i][index] = new ZeroSample(SampleType.ByteLength);
                }

                index++;
            }

            return result;
        }
    }
}
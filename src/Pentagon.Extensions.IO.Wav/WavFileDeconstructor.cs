// -----------------------------------------------------------------------
//  <copyright file="WavFileDeconstructor.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    public class WavFileDeconstructor
    {
        // ReSharper disable once NotAccessedField.Local
        readonly byte[] _bytes;

        public WavFileDeconstructor(byte[] bytes)
        {
            _bytes = bytes;
        }

        //{

        //public double[] GetSamples()

        //    int chunkID = reader.ReadInt32();
        //    int fileSize = reader.ReadInt32();
        //    int riffType = reader.ReadInt32();
        //    int fmtID = reader.ReadInt32();
        //    int fmtSize = reader.ReadInt32();
        //    int fmtCode = reader.ReadInt16();
        //    int channels = reader.ReadInt16();
        //    int sampleRate = reader.ReadInt32();
        //    int fmtAvgBPS = reader.ReadInt32();
        //    int fmtBlockAlign = reader.ReadInt16();
        //    int bitDepth = reader.ReadInt16();

        //    if (fmtSize == 18)
        //    {
        //        // Read any extra values
        //        int fmtExtraSize = reader.ReadInt16();
        //        reader.ReadBytes(fmtExtraSize);
        //    }

        //    int dataID = reader.ReadInt32();
        //    int dataSize = reader.ReadInt32();

        //    // Store the audio data of the wave file to a byte array. 

        //  var  byteArray = reader.ReadBytes(dataSize);
        //    var data = _bytes.ToList().GetRange(44, _bytes.Length - 44);
        //}
    }
}
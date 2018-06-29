// -----------------------------------------------------------------------
//  <copyright file="Signed24BitNumber.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling.Samples
{
    using System;

    public struct Signed24BitNumber
    {
        byte _firstByte, _secondByte, _thirdByte;

        #region Operators

        public static explicit operator Signed24BitNumber(short value)
        {
            var s = new Signed24BitNumber();
            var by = BitConverter.GetBytes(value);
            s._secondByte = by[0];
            s._thirdByte = by[1];
            return s;
        }

        #endregion

        public byte[] GetBytes() => new[] {_firstByte, _secondByte, _thirdByte};
    }
}
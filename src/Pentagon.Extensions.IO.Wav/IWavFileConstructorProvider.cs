// -----------------------------------------------------------------------
//  <copyright file="IWavFileConstructorProvider.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using System;

    public interface IWavFileConstructorProvider
    {
        IWavFileConstructor Create(int sampleRate, TimeSpan duration);
    }
}
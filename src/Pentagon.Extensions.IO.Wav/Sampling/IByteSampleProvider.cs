// -----------------------------------------------------------------------
//  <copyright file="IByteSampleProvider.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    public interface IByteSampleProvider
    {
        ISample Create(double sample);
    }
}
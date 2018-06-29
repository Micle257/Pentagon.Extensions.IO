// -----------------------------------------------------------------------
//  <copyright file="ISample.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    public interface ISample
    {
        int Length { get; }
        byte[] GetBytes();
    }
}
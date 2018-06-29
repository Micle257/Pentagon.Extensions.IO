// -----------------------------------------------------------------------
//  <copyright file="ISampleValueModifier.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    public interface ISampleValueModifier
    {
        double[] Adjust(double[] samples, double factor = 0.9);
    }
}
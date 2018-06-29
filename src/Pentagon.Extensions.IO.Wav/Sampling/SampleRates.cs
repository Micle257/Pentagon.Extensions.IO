// -----------------------------------------------------------------------
//  <copyright file="SampleRates.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    /// <summary> Provides sample rates values. </summary>
    public static class SampleRates
    {
        /// <summary> The cd quarter quality (11025). </summary>
        public const int CdQuarter = 11025;

        /// <summary> The cd half quality (22050). </summary>
        public const int CdHalf = 22050;

        /// <summary> The cd quality (44100). </summary>
        public const int Cd = 44100;

        /// <summary> The standard quality (48000). </summary>
        public const int Standard = 48000;

        /// <summary> The standard double quality (96000). </summary>
        public const int StandardDouble = 96000;

        /// <summary> The standard quadruple quality (192000). </summary>
        public const int StandardQuadruple = 192000;
    }
}
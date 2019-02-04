// -----------------------------------------------------------------------
//  <copyright file="IServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav.Sampling
{
    using Microsoft.Extensions.DependencyInjection;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWavServices(this IServiceCollection services)
        {
            services.AddTransient<IByteSampleProvider, SampleProvider>();
            services.AddTransient<ISampleValueModifier, SampleValueModifier>();
            services.AddTransient<IWavFileConstructorProvider, WavFileConstructorProvider>();

            return services;
        }
    }
}
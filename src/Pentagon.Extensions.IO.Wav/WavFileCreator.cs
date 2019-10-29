// -----------------------------------------------------------------------
//  <copyright file="WavFileCreator.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using System.IO;
    using Sampling;

    public class WavFileCreator : FileCreator
    {
        readonly ISoundPlayer _soundPlayer;

        /// <inheritdoc />
        public WavFileCreator(string name,
                              IWavFileConstructor constructor,
                              WavFileDeconstructor deconstructor,
                              ISoundPlayer soundPlayer) : base(name, suffix: "wav")
        {
            Constructor = constructor;
            Deconstructor = deconstructor;
            _soundPlayer = soundPlayer;
        }

        public IWavFileConstructor Constructor { get; }

        public WavFileDeconstructor Deconstructor { get; private set; }

        public void Create()
        {
            var bytes = Constructor.GetFileBytes();
            Create(bytes);
        }

        public void Open()
        {
            Deconstructor = new WavFileDeconstructor(File.ReadAllBytes($"{Name}.wav"));
        }

        public void Play()
        {
            Stream.Position = 0;
            _soundPlayer.Play();
        }
    }
}
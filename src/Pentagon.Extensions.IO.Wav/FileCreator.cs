// -----------------------------------------------------------------------
//  <copyright file="FileCreator.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Wav
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileCreator : IDisposable
    {
        bool _isCreated;

        /// <summary> Initializes a new instance of the <see cref="T:System.Object" /> class. </summary>
        public FileCreator(string name, string suffix)
        {
            Name = name;
            Suffix = suffix;
            Stream = new FileStream($"{Name}.{suffix}", FileMode.Create);
        }

        public FileStream Stream { get; }

        public string Name { get; }

        public string Suffix { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Create(IEnumerable<byte> bytes)
        {
            if (_isCreated)
                return;
            var b = new BinaryWriter(Stream);
            b.Write(bytes.ToArray());
            b.Dispose();
            _isCreated = true;
        }

        public void Remove()
        {
            if (!_isCreated)
                return;

            File.Delete($"{Name}.{Suffix}");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Stream?.Dispose();
        }
    }
}
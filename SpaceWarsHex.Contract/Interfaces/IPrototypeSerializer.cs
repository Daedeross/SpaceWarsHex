using System;
using System.IO;

namespace SpaceWarsHex.Interfaces
{
    public interface IPrototypeSerializer
    {
        /// <summary>
        /// Serialize a prototye (or anything serializable) to a stream.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="prototype">The object to serialize.</param>
        /// <exception cref="IOException">If the stream is not writable.</exception>
        void Serialize<T>(Stream stream, T prototype);

        /// <summary>
        /// Serialize a prototye (or anything serializable) to a file.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="path">The file path to write to.</param>
        /// <param name="prototype"></param>
        /// <exception cref="IOException">If the file is not writable.</exception>
        void Serialize<T>(string path, T prototype);

        /// <summary>
        /// Deserialize a prototye (or anything serializable) from a stream.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The deseialized <typeparamref name="T"/>/</returns>
        /// <exception cref="IOException">If the stream is not readable.</exception>
        /// <exception cref="InvalidCastException">If the object is not a <typeparamref name="T"/></exception>
        T Deserialize<T>(Stream stream);

        /// <summary>
        /// Deserialize a prototye (or anything serializable) from a file.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize.</typeparam>
        /// <param name="path">The file path to read from.</param>
        /// <returns>The deseialized <typeparamref name="T"/>/</returns>
        /// <exception cref="FileNotFoundException">If the file does not exist.</exception>
        /// <exception cref="IOException">If the file is not readable.</exception>
        /// <exception cref="InvalidCastException">If the object is not a <typeparamref name="T"/></exception>
        T Deserialize<T>(string path);
    }
}

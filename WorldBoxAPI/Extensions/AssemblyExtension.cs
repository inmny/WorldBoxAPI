using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WorldBoxAPI.Extensions {
    internal static class AssemblyExtension {
        /// <summary>
        /// Finds an embedded resource by name.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="resourceName"></param>
        /// <returns>Bytes of the embedded resource.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] GetResourceBytes(this Assembly a, string resourceName) {
            _ = resourceName ?? throw new ArgumentNullException(nameof(resourceName));
            string[] resourceNames = a.GetManifestResourceNames();
            string resourceFullName = resourceNames.Where(r => r.Contains(resourceName)).FirstOrDefault();
            _ = resourceFullName ?? throw new ArgumentException($"Resource \"{resourceName}\" was not found.", nameof(resourceName));

            using (Stream stream = a.GetManifestResourceStream(resourceFullName)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Finds an embedded resource by name.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="resourceName"></param>
        /// <returns>Raw text of the embedded resource.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string GetResourceText(this Assembly a, string resourceName) {
            _ = resourceName ?? throw new ArgumentNullException(nameof(resourceName));
            string[] resourceNames = a.GetManifestResourceNames();
            string resourceFullName = resourceNames.Where(r => r.Contains(resourceName)).FirstOrDefault();
            _ = resourceFullName ?? throw new ArgumentException($"Resource \"{resourceName}\" was not found.", nameof(resourceName));

            using (Stream stream = a.GetManifestResourceStream(resourceFullName)) {
                using (StreamReader reader = new StreamReader(stream)) {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}

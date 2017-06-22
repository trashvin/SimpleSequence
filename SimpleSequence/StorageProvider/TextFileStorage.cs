using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleSequence.StorageProvider
{
    /// <summary>
    /// The storage class that use a text file.
    /// </summary>
    public class TextFileStorage : IIDStorage
    {
        /// <summary>
        /// The complete file name of the file storage accepting source as parameter.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="source">
        /// The storage source.
        /// </param>
        public TextFileStorage(string source)
        {
            Source = source;
        }
        /// <summary>
        /// This method generates the sequence ID and increments the storage.
        /// </summary>
        /// <returns>Returns a long value.</returns>
        public long GetNextSequenceID()
        {
            Task<string> result = OpenAsync();
            result.Wait();

            long sequence = 0;
            Int64.TryParse(result.Result, out sequence);
            UpdateSequence(sequence + 1);
            return sequence;
        }
        private void UpdateSequence(long next)
        {
            WriteAsync(next.ToString());
        }

        private async Task<string> OpenAsync()
        {
            try
            {
                using (FileStream stream = new FileStream(Source, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            catch
            {
                await Task.Run(() => WriteAsync("0"));
                return "0";
            }
        }

        private async Task WriteAsync(string data)
        {
            try
            {
                using (FileStream stream = new FileStream(Source, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        await writer.WriteLineAsync(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}

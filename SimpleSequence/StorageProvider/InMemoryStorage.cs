using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSequence.StorageProvider
{
    /// <summary>
    /// The class for InMemory storage, sequence not saved.
    /// </summary>
    public class InMemoryStorage : IIDStorage
    {
        /// <summary>
        /// The method that generates the sequence.
        /// </summary>
        /// <returns>The generated sequence of type long.</returns>
        public long GetNextSequenceID()
        {
            long id = _internalCounter;
            _internalCounter++;
            return id;
        }

        private long _internalCounter;

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="startSequnce">An optional value for the starting sequence. If not provided, the sequence starts with 1.</param>
        public InMemoryStorage(long startSequnce = 1)
        {
            _internalCounter = startSequnce;
        }
    }
}

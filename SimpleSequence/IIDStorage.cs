using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSequence

{
    /// <summary>
    /// The interface for the target storage
    /// </summary>
    public interface IIDStorage
    {
        /// <summary>
        /// This is the storage source. If it is a file storage, this will be the complete file name. 
        /// For database storage, this will contain the connection string.
        /// </summary>
        string Source { get; set; }
        /// <summary>
        /// This is the moethod that returns the sequence ID.
        /// </summary>
        /// <returns>The sequence ID.</returns>
        long GetNextSequenceID();
    }
}

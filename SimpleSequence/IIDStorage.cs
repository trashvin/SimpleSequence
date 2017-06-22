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
        /// This is the moethod that returns the sequence ID.
        /// </summary>
        /// <returns>The sequence ID.</returns>
        long GetNextSequenceID();
    }
}

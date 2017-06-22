using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSequence
{
    /// <summary>
    /// The configuration class for the Generator.
    /// </summary>
    public class SequenceConfiguration
    {
        /// <summary>
        /// The ID format.
        /// Sample :
        /// N - It will return number sequence
        /// Y - System Year
        /// M - Month
        /// D - Day
        /// 1...9 - Custom Value [Passed as parameter ]
        /// {X} - Custom Fix Value
        /// Sample Usage (Seq Lenght = 5 ; Filler = 0)
        /// 1. N = 00001
        /// 2. Y-N = 2017-00002
        /// 3. MYN = 06201700003
        /// 4. 1N2 = AAA00004XXX(Assume Custom Value 1 = AAA ; Custom Value 2 = XXX )
        /// 5. {ABC}-N = ABC-00004
        /// </summary>
        public string IDFormat { get; set; }
        /// <summary>
        /// The name of application using the assembly.
        /// </summary>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// The lenght of the generated string sequence.
        /// </summary>
        public int SequenceLenght { get; set; }
        /// <summary>
        /// The character to be used to pad the generated string.
        /// </summary>
        public char FillerCharacter { get; set;  }
        /// <summary>
        /// The Date to beused. Defaults to current sytem date.
        /// </summary>
        public DateTime Date
        {
            get
            {
                if ( _date == null )
                {
                    return DateTime.Now;
                }
                else
                {
                    return _date;
                }
            }
            set
            {
                _date = value;
            }
        }
        private DateTime _date;
    }
}

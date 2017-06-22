using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace SimpleSequence
{
    /// <summary>
    /// The Generator class.
    /// </summary>
    public class Generator
    {
        private string _appName;
        private SequenceConfiguration _genConfiguration;
        private IIDStorage _storage;
        /// <summary>
        /// The constructor accepting the storage and configuration.
        /// </summary>
        /// <param name="storage">The storage of type IIDStorage.</param>
        /// <param name="configuration">The configuration class of type IDGenConfiguration.</param>
        public Generator(IIDStorage storage,SequenceConfiguration configuration)
        {
            _storage = storage;
            _genConfiguration = configuration;   
        }
        /// <summary>
        /// The main method that generates sequence ID of type string
        /// </summary>
        /// <returns></returns>
        public string GenerateID(string[] customValues = null)
        {
            try
            {
                string validNumbers = "0123456789";
                bool isConstant = false;
                //Generate format
                StringBuilder generatedID = new StringBuilder();

                foreach (char letter in _genConfiguration.IDFormat.ToUpper())
                {
                    switch (letter)
                    {
                        case 'Y': //Year
                            if (!isConstant)
                            {
                                generatedID.Append(_genConfiguration.Date.Year.ToString());
                            }
                            else generatedID.Append(letter.ToString());
                            break;
                        case 'M': //Month
                            if (!isConstant)
                            {
                                generatedID.Append(_genConfiguration.Date.Month.ToString().PadLeft(2, _genConfiguration.FillerCharacter));
                            }
                            else generatedID.Append(letter.ToString());
                            break;
                        case 'D': //Day
                            if (!isConstant)
                            {
                                generatedID.Append(_genConfiguration.Date.Day.ToString().PadLeft(2, _genConfiguration.FillerCharacter));
                            }
                            else generatedID.Append(letter.ToString());
                            break;
                        case '{':
                            isConstant = true;
                            break;
                        case '}':
                            isConstant = false;
                            break;
                        default:
                            if (!isConstant)
                            {
                                if (validNumbers.Contains(letter.ToString()))
                                {
                                    int index = -1;
                                    int.TryParse(letter.ToString(), out index);
                                    try
                                    {
                                        try
                                        {
                                            generatedID.Append(customValues[index]);
                                        }
                                        catch { }
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    generatedID.Append(letter.ToString());
                                }
                            }
                            else generatedID.Append(letter.ToString());
                            break;
                    }

                }

              
                return generatedID.ToString().Replace("N", GetSequenceNo());

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "";
            }
        }
        private string GetSequenceNo()
        {
            try
            {
                long sequence = _storage.GetNextSequenceID();
                string seqString = sequence.ToString();

                if ( seqString.Length < _genConfiguration.SequenceLenght)
                {
                    seqString = seqString.PadLeft(_genConfiguration.SequenceLenght, _genConfiguration.FillerCharacter);
                }
                else
                {
                    seqString = seqString.Substring(seqString.Length - _genConfiguration.SequenceLenght);
                }

                return seqString;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return string.Empty;
            }
        }


       
        
    }
}
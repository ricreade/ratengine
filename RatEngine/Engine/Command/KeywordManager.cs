using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// This class provides a reference to all possible keywords in the game.  This class
    /// is hydrated at service start up.
    /// </summary>
    public static class KeywordManager
    {
        static KeywordManager()
        {
            _keywords = new ConcurrentDictionary<string, Keyword>();
            LoadKeywords();
        }

        // The full list of keywords in the application
        private static ConcurrentDictionary<string, Keyword> _keywords;

        /// <summary>
        /// GetKeyword
        /// This method returns the keyword appropriate for the command string.  If the
        /// command string does not reference a valid keyword, this method returns null
        /// and throws a new InvalidCommandStringException.
        /// </summary>
        /// <param name="CommandString">[string] The command string referencing the
        /// keyword to be returned.</param>
        /// <returns>[Keyword] The keyword object referenced by the command string, or
        /// null if no matching command string was found.</returns>
        public static Keyword GetKeyword(string CommandString)
        {
            Keyword kywrd = null;

            // The keyword should be the first word in the command string.  Remove any
            // empty entries in case the user entered an extra space (or two, or a dozen)
            // at the beginning of the command string.  We only care about the words.
            string kywrdstr = CommandString.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries)[0];

            // If that keyword string exists in the dictionary, return it.
            if (_keywords.TryGetValue(kywrdstr, out kywrd))
            {
                return kywrd;
            }
            else
                throw new InvalidCommandStringException(
                    "The keyword '" + kywrd + "' was not recognized.");
        }

        /// <summary>
        /// LoadKeywords
        /// Loads all the keyword collection contained in this object.  This method is called
        /// at service start up to hydrate this class.
        /// </summary>
        private static void LoadKeywords()
        {
            RecordManager rm = new RecordManager();
            DataTable dt = null;

            try
            {
                dt = rm.SendReadRequest(Keyword.StoredProcedures.SELECT_ALL, new List<SqlParameter>());
            }
            catch (Exception ex)
            {
                throw;
            }

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Keyword k = new Keyword(null, dr);
                    if (!_keywords.TryAdd(k.Name, k))
                        throw new OperationFailedException("Could not add Keyword " + k.Name + ".");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

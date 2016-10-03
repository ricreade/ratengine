using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// This class references all possible flag comparisons in the game.  This class
    /// is hydrated at service start up.
    /// </summary>
    public static class FlagComparer
    {
        static FlagComparer()
        {
            _flagcomps = new ConcurrentDictionary<string, FlagComparison>();
            LoadComparisons();
        }

        // A collection of all possible flag comparisons.
        private static ConcurrentDictionary<string, FlagComparison> _flagcomps;

        // The type of comparison being made between flags.
        public enum FlagComparisonType { None, Counter, Mitigate, Indeterminate };

        /// <summary>
        /// CompareFlags
        /// Evaluates the flags in the Flags list that might be referenced in the defender 
        /// object.  For each such flag, looks for a counter in the Challenger object.
        /// Returns the string error for the first Flag that was not countered.  If no
        /// Flags were found in Defender, or all flags found are countered, returns an
        /// empty string.
        /// </summary>
        /// <param name="Flags">[IList<Flag>] The list of flags to evaluate.</Flag></param>
        /// <param name="Challenger">[Flaggable] The object attempting to counter any
        /// flags in Defender.</param>
        /// <param name="Defender">[Flaggable] The object with flags to be countered.</param>
        /// <returns>[string] The string error of the first uncounterd flag, or an empty
        /// string if no uncountered flags were found.</returns>
        public static string CompareFlags(IList<Flag> Flags, Flaggable Challenger, Flaggable Defender)
        {
            return "";
        }

        /// <summary>
        /// GetFlagComparison
        /// This method compares two flags and returns the type of comparison that exists
        /// between them.  The method should verify that exactly one comparison exists
        /// between the two flags.  If there is no comparison, the method should return
        /// the value None.  If there is more than one comparison, the method should return
        /// the value Indeterminate.  If either flag is null or another error occurs, the
        /// method should throw a new FlagComparisonFailureException.
        /// </summary>
        /// <param name="FlagFrom">[Flag] The Flag object representing the attacker.</param>
        /// <param name="FlagTo">[Flag] The Flag object representing the defender.</param>
        /// <returns>[FlagComparisonType] The comparison type.</returns>
        public static FlagComparisonType GetFlagComparison(Flag FlagFrom, Flag FlagTo)
        {
            return FlagComparisonType.None;
        }

        /// <summary>
        /// GetFlagByComparisonType
        /// Returns all flags that relate to the specified flag via the specified comparison
        /// type.  The return type ConcurrentDictionary is intended to support thread safety
        /// should the object returned from this method be needed by multiple objects at once.
        /// This method should throw a new FlagComparisonFailureException if an error occurs
        /// or if the specified Flag object is null.  If there are no flags, this method should 
        /// return a fully instantiated collection with no members.
        /// </summary>
        /// <param name="FlagFrom">[Flag] The flag object used as the basis for comparison.</param>
        /// <param name="CompareType">[FlagComparisonType] The type of comparison to use.</param>
        /// <returns>[ConcurrentDictionary<Flag, string>] The collection of matching Flags.</Flag></returns>
        public static ConcurrentDictionary<string, Flag> GetFlagByComparisonType(Flag FlagFrom, FlagComparisonType CompareType)
        {
            return null;
        }

        /// <summary>
        /// LoadComparisons
        /// Loads all flag comparisons from the database.  This method is intended to be run
        /// at the server startup.  The comparisons should remain in memory for the live of the
        /// application instance.
        /// </summary>
        public static void LoadComparisons()
        {

        }
    }
}

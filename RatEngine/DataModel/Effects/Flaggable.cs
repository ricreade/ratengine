using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatEngine.DataModel.World;

namespace RatEngine.DataModel.Effects
{
    /// <summary>
    /// Flaggable
    /// This abstract class is the base class for game objects that can have
    /// flags associated with them.  This class uses the ConcurrentDictionary
    /// type to support fast lookup and thread safety.
    /// </summary>
    public abstract class Flaggable : GameElement
    {
        protected List<string> _flags;
        private object _flaglock;

        public enum SearchMode
        {
            /// <summary>
            /// Search for flags serving the same purpose and having the same values.
            /// </summary>
            Equal,

            /// <summary>
            /// Search for flags serving the same purpose with equal or greater power.
            /// </summary>
            EqualOrGreater,

            /// <summary>
            /// Search for flags serving the same purpose with equal or lesser power.
            /// </summary>
            EqualOrLesser,

            /// <summary>
            /// Search for flags serving the same purpose with greater power.
            /// </summary>
            Greater,

            /// <summary>
            /// Search for flags serving the same purpose with lesser power.
            /// </summary>
            Lesser
        }

        /// <summary>
        /// Returns a read-only version of the internal flags collection.
        /// </summary>
        public IReadOnlyCollection<string> Flags
        {
            get { return _flags.AsReadOnly(); }
        }

        public virtual void AddFlag(Flag NewFlag)
        {
            lock (_flaglock)
            {
                _flags.Add(NewFlag.ToString());
            }
        }

        /// <summary>
        /// Finds the first flag matching the specified search flag and search mode.
        /// </summary>
        /// <param name="SearchFlag"></param>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public virtual string FindFlag(Flag SearchFlag, SearchMode Mode)
        {
            lock (_flaglock)
            {
                string sFlag = SearchFlag.ToString();
                foreach (string flag in _flags)
                {
                    switch (Mode)
                    {
                        case SearchMode.Equal:
                            if (Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalEqual)
                            {
                                return flag;
                            }
                            break;
                        case SearchMode.EqualOrGreater:
                            if (Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalEqual ||
                                Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalGreater)
                            {
                                return flag;
                            }
                            break;
                        case SearchMode.EqualOrLesser:
                            if (Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalEqual ||
                                Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalLesser)
                            {
                                return flag;
                            }
                            break;
                        case SearchMode.Greater:
                            if (Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalGreater)
                            {
                                return flag;
                            }
                            break;
                        case SearchMode.Lesser:
                            if (Flag.CompareFlags(flag, sFlag) == Flag.FlagComparison.IdenticalLesser)
                            {
                                return flag;
                            }
                            break;
                    }
                }
                return null;
            }
        }

        public virtual string RemoveFlag(Flag OldFlag)
        {
            lock (_flaglock)
            {
                string matchflag = FindFlag(OldFlag, SearchMode.Equal);
                if (matchflag != null && _flags.Remove(matchflag))
                {
                    return matchflag;
                }

                return null;
            }
        }

        /// <summary>
        /// Retrieves the flags for the specified parent game element and uses them
        /// to populate the flags collection.
        /// </summary>
        /// <param name="Parent">The parent object that will be used to retrieve
        /// the associated flags.</param>
        protected static void RetrieveFlags(GameElement Parent)
        {

        }
    }
}

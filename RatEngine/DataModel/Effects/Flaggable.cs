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
        protected List<Flag> _flags;

        /// <summary>
        /// Returns a read-only version of the internal flags collection.
        /// </summary>
        public IReadOnlyCollection<Flag> Flags
        {
            get { return _flags.AsReadOnly(); }
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

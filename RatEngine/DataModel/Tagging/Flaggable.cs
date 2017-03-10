using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


using RatEngine.DataModel.World;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Flaggable
    /// This abstract class is the base class for game objects that can have
    /// flags associated with them.  This class uses the ConcurrentDictionary
    /// type to support fast lookup and thread safety.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class Flaggable : IDataObject
    {
        protected ArrayList _flags;
        protected RatDataModelAdapter _adapter;
        
        /// <summary>
        /// The base constructor for this abstract class, which carries forward the requirement to
        /// specify the flaggable object's game ID.
        /// </summary>
        /// <param name="GameID">The game id of this flaggable object, or null if this is a new record.</param>
        public Flaggable(RatDataModelAdapter Adapter)
        {
            _adapter = Adapter;
            _flags = new ArrayList();
        }

        /// <summary>
        /// Returns a read-only version of the internal flags collection.
        /// </summary>
        [DataMember]
        public IEnumerable<Flag> Flags
        {
            get { return ArrayList.Synchronized(_flags).Cast<Flag>(); }
        }

        public abstract RatDataModelAdapter DataAdapter { get; set; }

        public virtual void AddFlag(Flag NewFlag)
        {
            if (!ArrayList.Synchronized(_flags).Contains(NewFlag))
                ArrayList.Synchronized(_flags).Add(NewFlag);
        }

        public virtual bool ContainsFlag(Flag TargetFlag)
        {
            return ArrayList.Synchronized(_flags).Contains(TargetFlag);
        }

        /// <summary>
        /// Retrieves the flags for the specified parent game element and uses them
        /// to populate the flags collection.
        /// </summary>
        /// <param name="Target">The target object from which to copy the flags.</param>
        protected void CopyFlags(GameElement Target)
        {

        }

        public virtual Flag RemoveFlag(Flag OldFlag)
        {
            if (ArrayList.Synchronized(_flags).Contains(OldFlag))
            {
                ArrayList.Synchronized(_flags).Remove(OldFlag);
                return OldFlag;
            }
            return null;
        }

        public virtual void ReplaceFlag(Flag OldFlag, Flag NewFlag)
        {
            if (RemoveFlag(OldFlag) != null)
            {
                AddFlag(NewFlag);
            }
            else
            {
                throw new ArgumentOutOfRangeException("The specific flag does not exist in the collection.");
            }
        }

        public abstract bool Delete();

        public abstract bool Delete(RatDataModelAdapter Adapter);

        public abstract void LoadFromAdapter(RatDataModelAdapter Adapter);

        public abstract bool Save();

        public abstract bool Save(RatDataModelAdapter Adapter);
    }
}

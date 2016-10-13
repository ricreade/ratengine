using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Effectable
    /// This abstract class represents a game object that can be affected by an
    /// Ability.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class Effectable : Flaggable
    {
        // The list of Effects associated with the derived object.
        protected ConcurrentDictionary<string, Effect> _effects;

        /// <summary>
        /// The base constructor for this abstract class, which carries forward the requirement to
        /// specify the effectable object's game ID.
        /// </summary>
        /// <param name="GameID">The game id of this effectable object, or null if this is a new record.</param>
        public Effectable(RatDataModelAdapter Adapter) : base(Adapter) { }

        /// <summary>
        /// Returns a read-only view of the Effects collection.
        /// </summary>
        public IReadOnlyCollection<Effect> Effects
        {
            get { return _effects.Values.ToList().AsReadOnly(); }
        }

        /// <summary>
        /// AddEffect
        /// Adds a new AbilityEffect to the list.
        /// </summary>
        /// <param name="NewEffect">[AbilityEffect] The new AbilityEffect to associate
        /// with the derived object.</param>
        public void AddEffect(Effect NewEffect)
        {

        }

        /// <summary>
        /// AddEffect
        /// Adds a list of AbilityEffect objects to the list.  This provides a convenient
        /// way to add a group of effects at once.
        /// </summary>
        /// <param name="NewEffectList">[IList] A list of effects to associate with the
        /// derived object.</param>
        public void AddEffect(IList<Effect> NewEffectList)
        {

        }

        /// <summary>
        /// RemoveEffect
        /// This method removes the specified ability effect from the derived object.
        /// </summary>
        /// <param name="OldEffect">[AbilityEffect] The effect to remove.</param>
        /// <returns>[AbilityEffect] A reference to the removed effect.  Returns null
        /// if nothing was removed.</returns>
        public AbilityEffect RemoveEffect(Effect OldEffect)
        {
            return null;
        }
    }
}

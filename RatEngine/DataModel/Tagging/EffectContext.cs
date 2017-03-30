using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Helper class that provides configuration rules and requirements to its
    /// associated effect name.  
    /// </summary>
    /// <remarks>This class is used in the <see cref="Trait"/> class to
    /// define the effects and effect organization required to support the trait, 
    /// such as multiplicity, whether the effect must be present in the 
    /// <see cref="Trait"/>, and other arbitrary settings as defined by the
    /// game.  Loose effects (such as those applied directly to most <see cref="GameElement"/> 
    /// derived classes like the <seealso cref="Room"/> class) are not controlled in 
    /// this way and therefore may be applied however the game sees fit.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class EffectContext
    {
        public EffectContext() { }

        public EffectContext(string controlledEffectName)
        {
            ControlledEffectName = controlledEffectName;
        }

        [DataMember]
        public virtual string ControlledEffectName { get; protected set; }

        [DataMember]
        public virtual Guid EffectContextID { get; protected set; }

        [DataMember]
        public virtual bool IsRequired { get; protected set; }
        
        [DataMember]
        public virtual int Multiplicity { get; protected set; }
        
        [DataMember]
        public virtual string Settings { get; protected set; }
        
        /// <summary>
        /// TODO: Figure this out (same as FlagContext)
        /// </summary>
        /// <param name="effects"></param>
        /// <returns></returns>
        public virtual bool IsConforming(List<Effect> effects)
        {
            List<Effect> templateEffects = effects.FindAll(
                effect => effect.Name.ToLower().Equals(ControlledEffectName.ToLower()));
            return false;
        }
    }
}

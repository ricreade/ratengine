using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.World;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Helper class that provides configuration rules and requirements to its
    /// associated flag name.  
    /// </summary>
    /// <remarks>This class is used in the <see cref="EffectTemplate"/> class to
    /// define the flags and flag organization required to support the effect, 
    /// such as multiplicity, whether the flag must be present in the 
    /// <see cref="Effect"/>, and other arbitrary settings as defined by the
    /// game.  Loose flags (such as those applied directly to most <see cref="GameElement"/> 
    /// derived classes like the <seealso cref="Room"/> class) are not controlled in 
    /// this way and therefore may be applied however the game sees fit.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class FlagContext
    {
        
        public FlagContext() { }

        public FlagContext(string flagName)
        {
            ControlledFlagName = flagName;
        }

        [DataMember]
        public virtual string ControlledFlagName { get; protected set; }

        [DataMember]
        public virtual Guid FlagContextID { get; protected set; }

        [DataMember]
        public virtual bool IsRequired { get; protected set; }

        [DataMember]
        public virtual int Multiplicity { get; protected set; }

        [DataMember]
        public virtual string Settings { get; protected set; }

        /// <summary>
        /// TODO: figure this out.
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public virtual bool IsConforming(List<Flag> flags)
        {
            List<Flag> templateFlags = flags.FindAll(
                flag => flag.Name.ToLower().Equals(ControlledFlagName.ToLower()));
            return false;
        }
    }
}

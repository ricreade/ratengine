using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Defines a specific collection of <see cref="Flag"/> values that
    /// comprise an effect.  
    /// </summary>
    /// <remarks>Effects are collections of flags that are intended to be 
    /// interpreted and assigned as a group to inform the application 
    /// of some quality of the parent element.  Effects may be associated with any
    /// <see cref="GameElement"/> derived class, except a <see cref="Flag"/> 
    /// or another <see cref="Effect"/>.  This means they may appear in
    /// nearly any context.  The effective meaning of an effect is defined by the 
    /// system instructions applied to a keyword, which means they are entirely
    /// dependent upon the unique rules of a given game design and have no
    /// intrinsic meaning.  An effect must have an associated 
    /// <see cref="EffectTemplate"/> to define how the effect should be 
    /// constructed.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Effect : GameElement
    {
        
        public Effect()
        {

        }

        [DataMember]
        public virtual EffectTemplate Template { get; set; }

        public override void AddEffect(Effect effect)
        {
            return;
        }

        public virtual bool IsConforming()
        {
            if (ReferenceEquals(Template, null))
                return false;
            return Template.IsConforming(this);
        }

        public override bool RemoveEffect(Effect effect)
        {
            return false;
        }
    }
}

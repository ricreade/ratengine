using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Defines a specific collection of <see cref="Effect"/> and <see cref="Flag"/>
    /// records that comprise a trait.  
    /// </summary>
    /// <remarks>Traits are collections of effects and flags that are intended to be 
    /// interpreted and assigned as a group to inform the application 
    /// of some quality of the parent element.  Traits may be associated with any
    /// <see cref="GameElement"/> derived class, except a <see cref="Flag"/> 
    /// or <see cref="Effect"/>.  This means they may appear in
    /// nearly any context.  The effective meaning of a trait is defined by the 
    /// system instructions applied to a keyword, which means they are entirely
    /// dependent upon the unique rules of a given game design and have no
    /// intrinsic meaning.  A trait must have an associated 
    /// <see cref="TraitTemplate"/> to define how the trait should be 
    /// constructed.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Trait : GameElement
    {
        public Trait() 
        {
            
        }
        
    }
}

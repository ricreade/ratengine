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
    /// Defines a specific tag associated with a <see cref="GameElement"/>.
    /// </summary>
    /// <remarks>Flags are used to provide game information about the instance.
    /// Flags are generally interpreted collectively to inform the application 
    /// of the game context of the element.  Flags may be associated with any
    /// <see cref="GameElement"/> derived class, which means they may appear in
    /// nearly any context.  The effective meaning of a flag is defined by the 
    /// system instructions applied to a keyword, which means they are entirely
    /// dependent upon the unique rules of a given game design and have no
    /// intrinsic meaning.  A flag must have an associated <see cref="FlagTemplate"/> 
    /// to define how the flag's value should be interpreted.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Flag : GameElement
    {
        
        protected Flag()
        {
            
        }

        public Flag(Guid id) : base(id)
        {

        }

        public Flag(string name, string data, FlagTemplate template)
        {
            Name = name;
            Data = data;
            Template = template;
        }

        [DataMember]
        public virtual string Data { get; protected set; }

        [DataMember]
        public virtual FlagTemplate Template { get; protected set; }

        public override void AddEffect(Effect effect)
        {
            return;
        }

        public override void AddFlag(Flag flag)
        {
            return;
        }

        public virtual Flag Clone()
        {
            return new Flag(Name, Data, Template);
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

        public override bool RemoveFlag(Flag flag)
        {
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Helper class that identifies the allowed flags and flag structure for 
    /// an <see cref="Effect"/> instance.
    /// </summary>
    /// <remarks>Every effect must have an associated template to tell the 
    /// application how the effect should be structured as the <see cref="Effect"/> 
    /// instance does not otherwise provide a way to determine and enforce
    /// these rules.  Like a <see cref="Flag"/> instance, the data associated 
    /// with an <see cref="Effect"/> is entirely arbitrary for the game engine.
    /// This class is intended to enforce the defined game rules
    /// so that the application knows what to expect when investigating an
    /// effect and how its data should be read.</remarks>
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class EffectTemplate
    {
        
        public EffectTemplate() { }

        [DataMember]
        public virtual Guid EffectTemplateID { get; protected set; }

        [DataMember]
        public virtual List<FlagContext> FlagDefinitions { get; protected set; }

        public virtual void AddFlagDefinition(FlagContext context)
        {
            if (context != null)
            {
                if (FlagDefinitions == null)
                    FlagDefinitions = new List<FlagContext>();
                if (!HasFlagDefinition(context))
                    FlagDefinitions.Add(context);
            }
        }

        public virtual bool HasFlagDefinition(FlagContext context)
        {
            if (FlagDefinitions == null)
                return false;

            return FlagDefinitions.Find(
                flagDef => 
                    flagDef.ControlledFlagName.ToLower().Equals(
                        context.ControlledFlagName.ToLower())) != null;
        }

        public bool IsConforming(Effect Element)
        {
            bool conforms = true;
            foreach(FlagContext context in FlagDefinitions)
            {
                conforms &= context.IsConforming(Element.Flags);
            }
            return conforms;
        }

        public virtual bool RemoveFlagDefinition(FlagContext context)
        {
            if (FlagDefinitions == null)
                return false;
            return FlagDefinitions.Remove(context);
        }
    }
}

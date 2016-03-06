using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Effects
{
    /// <summary>
    /// Flag
    /// This class defines a specific tag associated with some other game object
    /// that defines how that game object interacts with other game objects.
    /// </summary>
    public class Flag : GameElement
    {
        /// <summary>
        /// The class of flag this object represents.
        /// </summary>
        public enum FlagClassification
        {
            /// <summary>
            /// This flag declares a quality about its parent effect but has no game mechanics impact.
            /// The Modifier String, Comparator Value, and Mitigation Type properties will be ignored.
            /// </summary>
            Declarative,

            /// <summary>
            /// This flag is used to enhance an effect applied to a target by the parent object.  The
            /// flag must match the name of the targetted effect.  The Modifier String must be specified.
            /// The Mitigation Type will be ignored.
            /// </summary>
            Enhancement,

            /// <summary>
            /// This flag is used to mitigate an effect applied to the parent object.  The flag name 
            /// must match the name of the targetted effect.  The Modifier String and Mitigation Type 
            /// must be specified.  
            /// </summary>
            Mitigation
        }

        /// <summary>
        /// The mitigation strategy used by this mitigation flag.
        /// </summary>
        public enum MitigationType
        {
            /// <summary>
            /// This flag has no mitigation effect.  This value is invalid for a Mitigation flag.
            /// </summary>
            None,

            /// <summary>
            /// This flag is intended to reduce the impact of an effect.  The Component and Modifier 
            /// String must both be specified.
            /// </summary>
            Reduce,

            /// <summary>
            /// This flag attempts to prevent the effect from applying to the target.  The Component
            /// and Modifier String must both be specified.  The Modifier String must be expressed as 
            /// a percentage.
            /// </summary>
            Prevent
        }

        private string _effectname;
        private FlagClassification _class;
        private string _modstr;
        private int _compval;
        private MitigationType _mitigatetype;
        private Effect.Component _component;

        /// <summary>
        /// The name of the effect this flag is intended to modify if this  is an enhancement or 
        /// mitigation flag.
        /// </summary>
        public string EffectName
        {
            get { return _effectname; }
        }

        /// <summary>
        /// The classification of this flag, indicating its game purpose.
        /// </summary>
        public FlagClassification Classification
        {
            get { return _class; }
        }

        /// <summary>
        /// The modifier value if this flag modifies an effect.
        /// </summary>
        public string ModifierString
        {
            get { return _modstr; }
        }

        /// <summary>
        /// The comparator value if this flag will be compared to another flag.
        /// </summary>
        public int ComparatorValue
        {
            get { return _compval; }
        }

        /// <summary>
        /// The mitigation setting for this flag.
        /// </summary>
        public MitigationType MitigationSetting
        {
            get { return _mitigatetype; }
        }

        /// <summary>
        /// The component targetted by this flag if this flag modifies an effect.
        /// </summary>
        public Effect.Component TargetComponent
        {
            get { return _component; }
        }

        /// <summary>
        /// Instantiates a declarative flag.
        /// </summary>
        /// <param name="Name">The name of the flag.</param>
        public Flag(string Name)
        {
            _name = Name;
            ApplySettings(null, FlagClassification.Declarative, null, 0, Effect.Component.None, MitigationType.None);
        }

        /// <summary>
        /// Instantiates an enhancement flag with the specified settings.
        /// </summary>
        /// <param name="EffectName">The name of the effect this flag targets.</param>
        /// <param name="ModifierString">The modifier this flag applies to the target effect, if any.</param>
        /// <param name="ComparatorValue">The comparator if this flag if it will be compared to another flag.</param>
        /// <param name="TargetComponent">The effect component this flag modifies.</param>
        public Flag(string EffectName, string ModifierString, int ComparatorValue, Effect.Component TargetComponent)
        {
            ApplySettings(EffectName, FlagClassification.Enhancement, ModifierString, ComparatorValue, TargetComponent, MitigationType.None);
        }

        /// <summary>
        /// Instantiates a mitigation flag with the specified settings.
        /// </summary>
        /// <param name="EffectName">The name of the effect this flag targets.</param>
        /// <param name="ModifierString">The modifier this flag applies to the target effect, if any.</param>
        /// <param name="ComparatorValue">The comparator if this flag if it will be compared to another flag.</param>
        /// <param name="TargetComponent">The effect component this flag modifies.</param>
        /// <param name="MitigationSettings">The mitigation setting that specifies how this flag mitigates an effect.</param>
        public Flag(string EffectName, string ModifierString, int ComparatorValue, Effect.Component TargetComponent, MitigationType MitigationSettings)
        {
            ApplySettings(EffectName, FlagClassification.Mitigation, ModifierString, ComparatorValue, TargetComponent, MitigationSettings);
        }

        /// <summary>
        /// Validates and applies the specified flag settings.
        /// </summary>
        /// <param name="EffectName">The name of the effect this flag targets.</param>
        /// <param name="Classification">The classification of this flag, indicating its game purpose.</param>
        /// <param name="ModifierString">The modifier this flag applies to the target effect, if any.</param>
        /// <param name="ComparatorValue">The comparator if this flag if it will be compared to another flag.</param>
        /// <param name="TargetComponent">The effect component this flag modifies.</param>
        /// <param name="MitigationSettings">The mitigation setting that specifies how this flag mitigates an effect.</param>
        private void ApplySettings(string EffectName, FlagClassification Classification, string ModifierString, int ComparatorValue, Effect.Component TargetComponent, MitigationType MitigationSettings)
        {
            ValidateSettings(EffectName, Classification, ModifierString, ComparatorValue, TargetComponent, MitigationSettings);
            _effectname = EffectName;
            _class = Classification;
            _modstr = ModifierString;
            _compval = ComparatorValue;
            _component = TargetComponent;
            _mitigatetype = MitigationSettings;
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override void LoadDataRow(System.Data.DataRow Row)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks the specified settings to validate that they are correctly formed.
        /// </summary>
        /// <param name="EffectName">The name of the effect the flag targets.</param>
        /// <param name="Classification">The flag type.</param>
        /// <param name="ModifierString">The modifier string if this flag is a type that can modify effects.</param>
        /// <param name="ComparatorValue">The comparator value if this flag is a type that can be compared with other flags.</param>
        /// <param name="MitigationSettings">The mitigation type if this is a mitigation flag.</param>
        private static void ValidateSettings(string EffectName, FlagClassification Classification, string ModifierString, int ComparatorValue, Effect.Component TargetComponent, MitigationType MitigationSettings)
        {
            string errors = "";

            if (EffectName == null || EffectName.Trim().Length == 0 || EffectName.Trim().Length > 255)
            {
                errors += "The name must be a string from 1 to 255 characters. ";
            }

            if (!(false))
            {

            }

            if (errors.Length > 0)
                throw new ArgumentException(errors);
        }
    }
}

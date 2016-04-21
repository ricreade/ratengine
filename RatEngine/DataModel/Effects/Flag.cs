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
            Mitigation,

            /// <summary>
            /// The effect associated with this flag excludes the specified target(s) from the effect.
            /// </summary>
            Exclusion,

            /// <summary>
            /// The effect associated with this flag explicitly includes the specified target(s) in the
            /// effect; all other targets are unaffected.
            /// </summary>
            Inclusion,

            /// <summary>
            /// The effect associated with this flag can apply to multiple targets.  The effect name determines
            /// the scope of the effect (either room, region, or world).
            /// </summary>
            Ambient,

            /// <summary>
            /// The effect associated with this flag is reapplied at the specified interval unti the effect
            /// expires.
            /// </summary>
            Recurring
        }

        /// <summary>
        /// The results of a flag comparison.
        /// </summary>
        public enum FlagComparison
        {
            /// <summary>
            /// The compared flags are the same (they have the same values).
            /// </summary>
            IdenticalEqual,

            /// <summary>
            /// The compared flags serve the same purpose, and the other flag is less powerful than this one.
            /// </summary>
            IdenticalLesser,

            /// <summary>
            /// The compared flags serve the same purpose, and the other flag is more powerful than this one.
            /// </summary>
            IdenticalGreater,

            /// <summary>
            /// The compared flags have no relationship.
            /// </summary>
            NotEqual,

            /// <summary>
            /// The compared flags oppose each other and the flags have equal power.
            /// </summary>
            OpposedEqual,

            /// <summary>
            /// The compared flags oppose each other and the other flag is less powerful than this one.
            /// </summary>
            OpposedLesser,

            /// <summary>
            /// The compared flags oppose each other and the other flag is more powerful than this one.
            /// </summary>
            OpposedGreater
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
        private string _args;
        private FlagClassification _class;
        private string _modstr;
        private int _compval;
        private MitigationType _mitigatetype;
        private Effect.Component _component;

        /// <summary>
        /// The name of the effect this flag is intended to modify if this is an enhancement or 
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
        /// The arguments associated with this flag to determine its specific behavior.
        /// </summary>
        public string Arguments
        {
            get { return _args; }
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
        /// <param name="GameID">The game id of this flag object, or null if this is a new record.</param>
        /// <param name="Name">The name of the flag.</param>
        public Flag(string GameID, string Name) : base(GameID)
        {
            _name = Name;
            ApplySettings(null, FlagClassification.Declarative, null, 0, Effect.Component.None, MitigationType.None);
        }

        /// <summary>
        /// Instantiates an enhancement flag with the specified settings.
        /// </summary>
        /// <param name="GameID">The game id of this flag object, or null if this is a new record.</param>
        /// <param name="EffectName">The name of the effect this flag targets.</param>
        /// <param name="ModifierString">The modifier this flag applies to the target effect, if any.</param>
        /// <param name="ComparatorValue">The comparator if this flag if it will be compared to another flag.</param>
        /// <param name="TargetComponent">The effect component this flag modifies.</param>
        public Flag(string GameID, string EffectName, string ModifierString, int ComparatorValue, Effect.Component TargetComponent) : base(GameID)
        {
            ApplySettings(EffectName, FlagClassification.Enhancement, ModifierString, ComparatorValue, TargetComponent, MitigationType.None);
        }

        /// <summary>
        /// Instantiates a mitigation flag with the specified settings.
        /// </summary>
        /// <param name="GameID">The game id of this flag object, or null if this is a new record.</param>
        /// <param name="EffectName">The name of the effect this flag targets.</param>
        /// <param name="ModifierString">The modifier this flag applies to the target effect, if any.</param>
        /// <param name="ComparatorValue">The comparator if this flag if it will be compared to another flag.</param>
        /// <param name="TargetComponent">The effect component this flag modifies.</param>
        /// <param name="MitigationSettings">The mitigation setting that specifies how this flag mitigates an effect.</param>
        public Flag(string GameID, string EffectName, string ModifierString, int ComparatorValue, Effect.Component TargetComponent, MitigationType MitigationSettings) : base(GameID)
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

        /// <summary>
        /// Constucts a Flag object from the specified string data.  If the string data is empty or null, or
        /// is badly formed for a Flag representation, an error occurs.
        /// </summary>
        /// <param name="FlagString">The string representing the Flag to instantiate.</param>
        /// <returns></returns>
        public static Flag BuildFromString(string FlagString)
        {
            return null;
        }

        /// <summary>
        /// Compares two flags to determine their relationship.
        /// </summary>
        /// <param name="BaseFlag"></param>
        /// <param name="CompareFlag"></param>
        /// <returns></returns>
        public static FlagComparison CompareFlags(Flag BaseFlag, Flag CompareFlag)
        {
            return FlagComparison.NotEqual;
        }

        public static FlagComparison CompareFlags(string BaseFlag, string CompareFlag)
        {
            return FlagComparison.NotEqual;
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
        /// Returns a string equivalent of this flag for application to a Flaggable
        /// object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Effects;

namespace RatEngine.DataModel.Effects
{
    /// <summary>
    /// Effect
    /// This class defines a specific game effect that is composed of a
    /// discrete set of flags.  This this effect is applied to a target, that
    /// target gains those flags.
    /// </summary>
    public class Effect : Flaggable
    {
        public enum Component
        {
            /// <summary>
            /// The effect damage.
            /// </summary>
            Damage,

            /// <summary>
            /// The effect duration.
            /// </summary>
            Duration,

            /// <summary>
            /// The effect's applied level.
            /// </summary>
            Level,

            /// <summary>
            /// No component is specified.
            /// </summary>
            None
        }

        /// <summary>
        /// Constructs a simple Effect object.  If the unique Game ID property is specified, the effect
        /// object is populated from the data source.  If this is a new Effect record, specify null for
        /// this value.
        /// </summary>
        /// <param name="GameID">The game id of this effect object, or null if this is a new record.</param>
        public Effect(string GameID) : base(GameID) { }

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
    }
}

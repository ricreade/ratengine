using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Mob.Advancement
{
    /// <summary>
    /// AbilityEffect
    /// This class defines an ability that has been applied to a target.  This
    /// means that the ability has been used by some game entity and this class
    /// represents the effect of that ability on the target.  This is a way to
    /// associated the ability and all of its effects (and flags) with the target
    /// such that durations and level of the effect can be applied.
    /// </summary>
    public class AbilityEffect : GameElement
    {
        // This is the Ability that was used.  This Ability contains references to
        // its associated flags and base power already.  The effect of this Ability
        // on the target can be derived from this information.
        private Ability _ability;

        // This is the duration of the effect (total).  This value is used by the game
        // engine to determine when the effect will expire and to remove this reference
        // from the target when it does.
        private int _duration;

        // This is the level at which the effect was applied.  The power property of
        // the Ability gives the ability's effectiveness all else being equal, but
        // entities of different levels will use the same ability to different effect.
        // This property represents that.
        private int _effectlevel;

        public Ability Ability
        {
            get { return _ability; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public int EffectLevel
        {
            get { return _effectlevel; }
            set { _effectlevel = value; }
        }

        /// <summary>
        /// AbilityEffect(Ability ability, DataRow dataRow)
        /// Constructor will fill the construct Fields and 
        /// </summary>
        /// <returns></returns>

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

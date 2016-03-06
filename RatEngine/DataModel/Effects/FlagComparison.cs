﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Effects
{
    /// <summary>
    /// This class defines a comparison between two flags in the game.  The values in this
    /// class are intended to be instantiated at server startup and will remain in memory
    /// for the lifetime of the application instance.
    /// </summary>
    class FlagComparison : GameElement
    {
        // The flag representing the attacker.  This is the flag that should be
        // the response to _flagto.
        private Flag _flagfrom;

        // The flag representing the defender.  This flag contains the value to
        // which _flagfrom responds.
        private Flag _flagto;

        // The type of comparison being made between these two flags.
        private FlagComparer.FlagComparisonType _comp;

        public Flag FlagFrom
        {
            get { return _flagfrom; }
        }

        public Flag FlagTo
        {
            get { return _flagto; }
        }

        public FlagComparer.FlagComparisonType Comparison
        {
            get { return _comp; }
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
    }
}

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
    /// This class defines a comparison between two flags in the game.  The values in this
    /// class are intended to be instantiated at server startup and will remain in memory
    /// for the lifetime of the application instance.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class FlagComparison : GameElement
    {
        // The flag representing the attacker.  This is the flag that should be
        // the response to _flagto.
        private Flag _flagfrom;

        // The flag representing the defender.  This flag contains the value to
        // which _flagfrom responds.
        private Flag _flagto;

        // The type of comparison being made between these two flags.
        private FlagComparer.FlagComparisonType _comp;

        public FlagComparison() 
        {

        }

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

        //public override RatDataModelAdapter DataAdapter
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public override bool Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Delete(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void LoadDataRow(System.Data.DataRow Row)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Save()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Save(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

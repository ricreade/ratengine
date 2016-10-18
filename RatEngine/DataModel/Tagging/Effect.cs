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
    /// Effect
    /// This class defines a specific game effect that is composed of a
    /// discrete set of flags.  This this effect is applied to a target, that
    /// target gains those flags.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Effect : IDataObject
    {
        private EffectTemplate _template;
        private RatDataModelAdapter _adapter;

        public EffectTemplate Template
        {
            get { return _template; }
        }

        public RatDataModelAdapter DataAdapter
        {
            get { return _adapter; }

            set { _adapter = value; }
        }

        /// <summary>
        /// Constructs a simple Effect object.  If the unique Game ID property is specified, the effect
        /// object is populated from the data source.  If this is a new Effect record, specify null for
        /// this value.
        /// </summary>
        /// <param name="GameID">The game id of this effect object, or null if this is a new record.</param>
        public Effect(RatDataModelAdapter Adapter)
        {
            _adapter = Adapter;
        }


        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Delete(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        //public override void LoadDataRow(System.Data.DataRow Row)
        //{
        //    throw new NotImplementedException();
        //}

        public void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Save(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }
    }
}

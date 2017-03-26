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
    /// Flag
    /// This class defines a specific tag associated with some other game object
    /// that defines how that game object interacts with other game objects.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Flag : GameElement // : IDataObject
    {
        private Tuple<string, byte[]> _data;
        private FlagTemplate _template;
        //private RatDataModelAdapter _adapter;
        
        /// <summary>
        /// Instantiates a declarative flag.
        /// </summary>
        /// <param name="GameID">The game id of this flag object, or null if this is a new record.</param>
        /// <param name="Name">The name of the flag.</param>
        public Flag()
        {
            
        }

        [DataMember]
        public FlagTemplate Template
        {
            get { return _template; }
        }

        //public RatDataModelAdapter DataAdapter
        //{
        //    get { return _adapter; }

        //    set { _adapter = value; }
        //}

        //public bool Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Delete(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public void LoadFromAdapter(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Save()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Save(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// This class represents a collection of regions and organizes them together in a structured way.
    /// The Realm represents the overall game map and as such, any given game should have only one
    /// Realm.  This provides a way to introduce versions of the same game at different difficulties
    /// or completely different games using the same engine.
    /// </summary>
    [DataContract]
    public class Realm : GameElement
    {
        // Database field names.
        public struct Fields
        {
            public const string ID = "ID";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "";
        }

        public struct SPArguments
        {
            public const string ID = "Id";
        }

        // A collection of all Regions in the Realm.
        private ConcurrentDictionary<string, Region> _regions;

        // Public getters and setters.

        /// <summary>
        /// Realm(int realmID, string desc)
        /// Constructor will handle hydrating GameElement attributes from data row and instantiate _regions variable. Will hydrate using
        /// a provided DataRow
        /// </summary>
        public Realm(string GameID, RatDataModelAdapter Adapter) : base(GameID, Adapter)
        {
            //instantiate Dictionary of Regions
            _regions = new ConcurrentDictionary<string, Region>();

            //if statements to make sure DataRow contains correct information
            //if (Row != null)
            //    LoadDataRow(Row);
            //else
            //    throw new NullReferenceException("The DataRow record for a Realm was null.  " +
            //        "Cannot initialize the Realm.");
        }

        [DataMember]
        public IEnumerable<Region> Regions
        {
            get { return _regions.Select(item => item.Value); }
        }

        public override bool Delete()
        {
            if (_adapter != null)
            {

            }
            return false;
        }

        public override void LoadDataRow(DataRow Row)
        {
            //parse through DataRow making sure data types are correct in each field
            try
            {
                PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
                PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
                PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);
            }
            catch (Exception ex)
            {
                throw;
            }

            //load _regions using method
            LoadRegions();
        }

        /// <summary>
        /// LoadRegions
        /// This method loads all Regions in this Realm.  This
        /// method should only be called at service start up.
        /// </summary>
        public void LoadRegions()
        {
            //Create instance of RecordManager for retrieving Data
            RecordManager recordManager = new RecordManager();

            //Create empty DataTable for results
            DataTable dtResults = new DataTable();
            //attempt to retrieve table
            try
            {
                //create IList<SqlParam> and sp name for SendReadRequest 
                IList<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter realmID = new SqlParameter("@RealmID", this._id);//TODO: verify names and parameters of sp when final ERD is up
                string spName = "mspGetRegions";

                //add SqlParam to Ilist
                sqlParams.Add(realmID);

                //fill table by calling SendReadRequest
                dtResults = recordManager.SendReadRequest(spName, sqlParams);

                //foreach loop to create and add each Region to _regions list with the <Tkey> as name of Region
                foreach (DataRow row in dtResults.Rows)
                {
                    Region newRegion;
                    //create Region
                    try
                    {
                        newRegion = new Region(null, row, this);

                        //add newRegion to _regions list
                        if (!_regions.TryAdd(newRegion.Name, newRegion))
                            throw new OperationFailedException("Region " + newRegion.Name +
                                " could not be added to Realm " + this.Name + ".");
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.ToString());
                        throw;
                    }

                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());//TODO: write Exception code for each Exception message found while testing.
                throw;
            }
        }

        public void ResolveTransitionReferences()
        {
            foreach (Region reg in Regions)
            {
                reg.ResolveTransitionReferences();
            }
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}

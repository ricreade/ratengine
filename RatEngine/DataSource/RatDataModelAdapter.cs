using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataModel.World;

namespace RatEngine.DataSource
{
    public enum RatDataModelType
    {
        Keyword,
        KeywordSyntax,
        Realm,
        Region,
        Room,
        SystemInstruction,
        Transition
    }

    /// <summary>
    /// Utility class to support data handling between the data source and the
    /// data models.
    /// </summary>
    public class RatDataModelAdapter
    {

        private IDataResultSet _queryResult;
        private RatDataModelType _lastRetrievedModel;

        // Database field names.
        public struct RealmFields
        {
            public const string ID = "RealmID";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
        }

        public struct RegionFields
        {
            public const string ID = "Id";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
            public const string REALM = "fkRealm";
        }

        public struct RoomFields
        {
            public const string ID = "Id";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
            public const string REGION = "fkRegion";
        }

        public struct TransitionFields
        {
            public const string ID = "ID";
            public const string NAME = "Name";
            public const string DESC = "Description";
            public const string DESCFROM = "descriptionFrom";
            public const string DESCTO = "descriptionTo";
            public const string KEYWORD = "KeyWord";
            public const string ROOM_FROM = "fkRoomFrom";
            public const string ROOM_TO = "fkRoomTo";
        }

        public IDataResultSet ResultSet
        {
            get { return _queryResult; }
        }

        public RatDataModelType LastRetrievedModel
        {
            get { return _lastRetrievedModel; }
        }

        private List<SqlParameter> ConvertParameterList(List<DataParameter> Parameters)
        {
            List<SqlParameter> plist = new List<SqlParameter>();
            if (Parameters != null)
            {
                foreach (DataParameter p in Parameters)
                {
                    plist.Add(new SqlParameter(p.FieldName, p.Value));
                }
            }
            
            return plist;
        }

        public void Delete(RatDataModelType Model, List<DataParameter> Parameters)
        {
            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            _queryResult = conn.SendWriteRequest("", ConvertParameterList(Parameters));
        }

        public void Retrieve(RatDataModelType Model, List<DataParameter> Parameters)
        {
            
            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            _queryResult = conn.SendReadRequest("", ConvertParameterList(Parameters));
            _lastRetrievedModel = Model;

        }

        public void Save(RatDataModelType Model, List<DataParameter> Parameters)
        {
            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            _queryResult = conn.SendWriteRequest("", ConvertParameterList(Parameters));
        }

        
    }
}

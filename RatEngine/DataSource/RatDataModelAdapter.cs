using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataModel.World;

using log4net;

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
        private static readonly ILog log = LogManager.GetLogger(typeof(RatDataModelAdapter));

        private IDataResultSet _queryResult;
        private Guid _newgameid;
        private int _newregid;
        private int _newrecid;
        private int _returnvalue;
        private RatDataModelType _lastRetrievedModel;

        // Database field names.
        public struct GameRegistryFields
        {
            public const string ID = "GameRegistryID";
            public const string GAME_ID = "GameId";
            public const string PREFIX_ID = "GameIdPrefixID";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
        }

        public struct GameRegistryParameters
        {
            public const string GAME_ID = "@gameid";
            public const string REGISTRY_ID = "@regid";
            public const string RECORD_ID = "@id";
            public const string RETURN_VALUE = "@returnval";
        }

        public struct RealmFields
        {
            public const string ID = "RealmID";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
        }

        public struct RealmParameters
        {
            public const string ID = "@realmid";
            public const string NAME = "@name";
            public const string DESCRIPTION = "@descript";
        }

        public struct RealmProcedures
        {
            public const string DELETE = "DeleteRealm";
            public const string INSERT = "InsertRealm";
            public const string RETRIEVE = "RetrieveRealm";
            public const string UPDATE = "UpdateRealm";
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

        public Guid NewGameID
        {
            get { return _newgameid; }
        }

        public int NewRecordID
        {
            get { return _newrecid; }
        }

        public int NewRegistryID
        {
            get { return _newregid; }
        }

        public IDataResultSet ResultSet
        {
            get { return _queryResult; }
        }

        public RatDataModelType LastRetrievedModel
        {
            get { return _lastRetrievedModel; }
        }

        public int ReturnValue
        {
            get { return _returnvalue; }
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
            string proc = null;
            SqlParameter p = new SqlParameter(GameRegistryParameters.RETURN_VALUE, SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            switch (Model)
            {
                case RatDataModelType.Realm:
                    proc = RealmProcedures.DELETE;
                    break;
                default:
                    log.Error(string.Format("Cannot process delete request for model type enumeration '{0}'.", Model.ToString()));
                    break;
            }

            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            List<SqlParameter> paramlist = ConvertParameterList(Parameters);
            paramlist.Add(p);
            conn.SendWriteRequest(proc, paramlist);
            SetResultIDValues(conn);
        }

        public void Retrieve(RatDataModelType Model, List<DataParameter> Parameters)
        {
            string proc = null;

            switch (Model)
            {
                case RatDataModelType.Realm:
                    proc = RealmProcedures.RETRIEVE;
                    break;
                default:
                    // Log this error.
                    break;
            }
            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            _queryResult = conn.SendReadRequest(proc, ConvertParameterList(Parameters));
            _lastRetrievedModel = Model;

        }

        public void Save(RatDataModelType Model, List<DataParameter> Parameters)
        {
            string proc = null;
            List<SqlParameter> outParams = new List<SqlParameter>();
            SqlParameter p = null;

            switch (Model)
            {
                case RatDataModelType.Realm:
                    if (Parameters.Find(item => item.FieldName == RealmFields.ID) == null)
                    {
                        proc = RealmProcedures.INSERT;

                        p = new SqlParameter(GameRegistryParameters.GAME_ID, SqlDbType.UniqueIdentifier);
                        p.Direction = ParameterDirection.Output;
                        outParams.Add(p);

                        p = new SqlParameter(GameRegistryParameters.RECORD_ID, SqlDbType.Int);
                        p.Direction = ParameterDirection.Output;
                        outParams.Add(p);
                    }
                    else
                    {
                        proc = RealmProcedures.UPDATE;

                        p = new SqlParameter(GameRegistryParameters.RETURN_VALUE, SqlDbType.Int);
                        p.Direction = ParameterDirection.Output;
                        outParams.Add(p);
                    }
                    break;
            }
            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            List<SqlParameter> paramlist = ConvertParameterList(Parameters);
            paramlist.AddRange(outParams);

            conn.SendWriteRequest(proc, paramlist);
            //_queryResult = conn.SendReadRequest(proc, ConvertParameterList(Parameters));
            //_lastRetrievedModel = Model;
            SetResultIDValues(conn);
        }

        private void SetResultIDValues(SqlDataConnection Connection)
        {
            Guid guidVal;
            int intVal;
            List<SqlParameter> plist = Connection.GetConnectionParameters();
            SqlParameter p = null;

            p = plist.Find(item => item.ParameterName == GameRegistryParameters.GAME_ID);
            if (p != null && Guid.TryParse(p.Value.ToString(), out guidVal))
            {
                _newgameid = guidVal;
            }

            p = plist.Find(item => item.ParameterName == GameRegistryParameters.REGISTRY_ID);
            if (p != null && int.TryParse(p.Value.ToString(), out intVal))
            {
                _newregid = intVal;
            }

            p = plist.Find(item => item.ParameterName == GameRegistryParameters.RECORD_ID);
            if (p != null && int.TryParse(p.Value.ToString(), out intVal))
            {
                _newrecid = intVal;
            }

            p = plist.Find(item => item.ParameterName == GameRegistryParameters.RETURN_VALUE);
            if (p != null && int.TryParse(p.Value.ToString(), out intVal))
            {
                _returnvalue = intVal;
            }
        }
    }
}

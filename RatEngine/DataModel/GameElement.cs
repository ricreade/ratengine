﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel
{
    /// <summary>
    /// GameElement
    /// This class is the base class for all game objects populated from the database.
    /// All items included in the database must have ID, Name, and Description properties.
    /// This provides a common set of properties for all game elements to more easily
    /// support commands like "Look".
    /// </summary>
    public abstract class GameElement
    {
        protected int _id;
        protected string _gameid;
        protected string _name;
        protected string _descr;
        protected RatDataModelAdapter _adapter;

        /// <summary>
        /// The base constructor for a GameElement.  All instances deriving from GameElement
        /// must specify a unique GameID value.  If this is a new record to be saved to the
        /// database, specify null for this value.
        /// </summary>
        /// <param name="GameID"></param>
        public GameElement(string GameID, RatDataModelAdapter Adapter)
        {
            _gameid = GameID;
            _adapter = Adapter;
        }

        /// <summary>
        /// The database primary key.
        /// </summary>
        public int ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the data adapter for communication with the data source.
        /// </summary>
        public RatDataModelAdapter DataAdapter
        {
            get { return _adapter; }
            set { _adapter = value; }
        }

        /// <summary>
        /// The unique game identifier for this game object.
        /// </summary>
        public string GameID
        {
            get { return _gameid; }
        }

        /// <summary>
        /// The name of this game object.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The description of this game object.
        /// </summary>
        public string Description
        {
            get { return _descr; }
            set { _descr = value; }
        }

        /// <summary>
        /// Delete
        /// Abstract method defining a delete method to permanently delete the
        /// record defined by the derived object.  This method must be implemented
        /// by all non-abstract derived classes.
        /// </summary>
        /// <returns>[bool] True if the delete operation was successful, otherwise false.</returns>
        public abstract bool Delete();

        /// <summary>
        /// LoadDataRow
        /// Abstract method defining a method to hydrate the derived object from
        /// a database record.  THis method must be implemented by all non-abstract
        /// derived classes.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing the data
        /// with which the derived object will hydrate itself.</param>
        public abstract void LoadDataRow(DataRow Row);

        /// <summary>
        /// Save
        /// Abstract method defining a method to save the record defined by the
        /// derived object with the derived object's current instance properties.
        /// If this is a new record (the id property is 0), then this is an 
        /// INSERT operation.  If this is an existing object (the id property is
        /// greater than 0), then this is an UPDATE operation.  If the id property
        /// contains a negative value, this method should throw an exception.
        /// This method must be implemented by all non-abstract derived classes.
        /// </summary>
        /// <returns>[bool] True if the save operation was successful, otherwise false.</returns>
        public abstract bool Save();

        /// <summary>
        /// TryPopulatePropertyFromDataRow
        /// Attempts to populate the specified property from the specified data row field.  If the FieldName
        /// value does not exist in the table, throws a new OperationFailedException.  If the property type
        /// does not match the type contained in the data row, throws an InvalidCastException.  If the data
        /// row field value is null, sets the property to the default value for its type.  Otherwise, this
        /// method attempts to set the property value to the value stored in the data row field.
        /// </summary>
        /// <typeparam name="T">The type of the property to be hydrated.</typeparam>
        /// <param name="Row">The data row containing the database value to be stored.</param>
        /// <param name="FieldName">The field name in the data row that contains the desired value.</param>
        /// <param name="Property">The property to be hydrated.</param>
        public void PopulatePropertyFromDataRow<T>(DataRow Row, string FieldName, out T Property)
        {
            if (!Row.Table.Columns.Contains(FieldName))
            {
                throw new OperationFailedException("The given FieldName " + FieldName +
                    " is invalid for table " + Row.Table.TableName + ".");
            }

            if (Row.IsNull(FieldName))
            {
                Property = default(T);
                return;
            }

            if (Row[FieldName].GetType() != typeof(T))
            {
                throw new InvalidCastException("The type given for the property <" + typeof(T) +
                    "> does not match the data row type <" + Row[FieldName].GetType() + ">.");
            }

            try
            {
                Property = (T)Row[FieldName];
            }
            catch (Exception ex)
            {
                // This will occur if something unexpected happened.  Rethrow the exception.
                throw;
            }
        }
    }
}

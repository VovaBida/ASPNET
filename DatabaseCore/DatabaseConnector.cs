﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
//ToDo: Extract, fill gridview, save changes from gridview to DB.
namespace firstgit.DatabaseCore
{
    public class DatabaseConnector
    {
        /// <summary>
        /// SQL Server connection.
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// Data table containing extracted data.
        /// </summary>
        private DataTable _dataTable;

        /// <summary>
        /// Target GridView
        /// </summary>
        private GridView _grid;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="connection">SQL server connection the data will be extracted from.</param>
        /// <param name="grid">Target GridView.</param>
        /// <param name="tableName">Table name on SQL server.</param>
        public DatabaseConnector(SqlConnection connection, string tableName, GridView grid)
        {
            this._connection = connection;
            this._grid = grid;
            this.ExtractData(tableName);
        }

        /// <summary>
        /// Extracts data from specified table and stores it in DataTable object.
        /// </summary>
        /// <param name="tableName">SQL Server table name.</param>
        private void ExtractData(string tableName)
        {
            _connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM " + tableName, _connection);
            this._dataTable = new DataTable();
            dataAdapter.Fill(this._dataTable);
            _connection.Close();
        }

        /// <summary>
        /// Replaces data on SQL Server with data in provided DataTable object.
        /// </summary>
        /// <param name="table">Table with values to be saved on the server.</param>
        private void SaveData(DataTable table)
        {
            _connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM " + table.TableName, _connection);
            dataAdapter.Update(table);
            _connection.Close();
        }

        /// <summary>
        /// ctor.
        /// </summary>
        ///<param name="connectionString">SQL server connection string with valid syntax.</param>
        ///<param name="grid">Target GridView.</param>
        ///<param name="tableName">Table name on SQL server.</param>
        public DatabaseConnector(string connectionString, string tableName, GridView grid)
            : this(new SqlConnection(connectionString), tableName, grid)
        {

        }

        /// <summary>
        /// Fills specified gridview with data from SQL Server.
        /// </summary>
        /// <param name="target">Target grid that is to be filled with data.</param>
        public void FillGridView()
        {
            this._grid.DataSource = this._dataTable;
        }

        /// <summary>
        /// Saves data from GridView to SQL Server.
        /// </summary>
        public void SaveChanges()
        {
            DataTable table = new DataTable(this._dataTable.TableName);
            foreach (DataColumn column in this._dataTable.Columns)
                table.Columns.Add(column);
            foreach (GridViewRow row in this._grid.Rows)
            {
                List<TableCell> cells = new List<TableCell>();
                foreach (TableCell cell in row.Cells)
                {
                    cells.Add(cell);
                }
                table.Rows.Add(cells.ToArray());
            }
            this.SaveData(table);
        }
    }
}
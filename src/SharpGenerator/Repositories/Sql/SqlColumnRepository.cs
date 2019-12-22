﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SharpGenerator.Models;

namespace SharpGenerator.Repositories.Sql
{
    public class SqlColumnRepository : BaseSqlRepository, IColumnRepository
    {
        public SqlColumnRepository(SqlConnection connection) : base(connection)
        {
        }
        
        public List<Column> FindByTable(string tableName, string databaseName)
        {
            var columns = new List<Column>();
            string query = @"
select column_name, data_type
from information_schema.columns
where table_name = @table_name
and table_catalog = @table_catalog";
            using (var rs = ExecuteReader(query, new SqlParameter("@table_name", tableName), new SqlParameter("@table_catalog", databaseName))) {
                while (rs.Read()) {
                    columns.Add(new Column(GetString(rs, 0), GetString(rs, 1)));
                }
            }
            return columns;
        }
    }
}

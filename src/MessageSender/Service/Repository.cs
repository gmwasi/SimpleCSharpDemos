using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Main.Models;
using Microsoft.Extensions.Configuration;

namespace Main.Service
{
    public class Repository : IRepository
    {
        private IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Call a stored procedure an give back the results
        public IEnumerable<Table> Get()
        {
            var result = new List<Table>();
            string connectionString = _configuration.GetValue<string>("ConnectionStrings:mainConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "procSelectAllTables";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;


            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //read the data
                while (reader.Read())
                {
                    var resultItem = Map(reader);
                    result.Add(resultItem);
                }
            }
            connection.Close();
            return result;
        }

        private Table Map(IDataRecord record)
        {
            var table = new Table()
            {
                Name = record["Name"] as string,
                CreateDate = (DateTime) record["CreateDate"]
            };
            return table;
        }
    }
}
//SAMPLE STORED PROCEDURE
/*
 * /****** Object:  StoredProcedure [dbo].[procSelectAllFromAllTables]    Script Date: 18/07/2018 15:12:03 ****** /
   SET ANSI_NULLS ON
   GO
   SET QUOTED_IDENTIFIER ON
   GO
   ALTER Procedure [dbo].[procSelectAllTables]
   AS
   
   SELECT
   name AS [Name],
   crdate AS [CreateDate]
   FROM
   SYSOBJECTS
   WHERE
   xtype = 'U';
   GO
 */
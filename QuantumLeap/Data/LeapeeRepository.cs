using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace QuantumLeap.Data
{
    public class LeapeeRepository
    {
        const string ConnectionString = "Server = localhost; Database = QuantumLeap; Trusted_Connection = True;";

        public IEnumerable<Leapee> GetLeapees()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "SELECT id, name FROM leapees";

                var leapees = db.Query<Leapee>(sqlQuery).ToList();

                return leapees;
            }
        }


        public Leapee AddLeapee(string name)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var leapeeRepo = new LeapeeRepository();

                var insertQuery = @"
                    INSERT INTO [dbo].[Leapees]
                               ([Name])
                    OUTPUT inserted.*
                         VALUES
                               (@name)";

                var parameters = new
                {
                    Name = name,
                };

                var newLeapee = db.QueryFirstOrDefault<Leapee>(insertQuery, parameters);

                if (newLeapee != null)
                {
                    return newLeapee;
                }
            }
            throw new Exception("Leapee was not created");
        }
    }
}
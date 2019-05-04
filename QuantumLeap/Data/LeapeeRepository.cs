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
    }
}
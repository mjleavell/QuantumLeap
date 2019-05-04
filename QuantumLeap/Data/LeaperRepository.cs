using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Data
{
    public class LeaperRepository
    {
        const string ConnectionString = "Server = localhost; Database = QuantumLeap; Trusted_Connection = True;";

        public IEnumerable<Leaper> GetLeapers()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "SELECT * FROM Leapers";

                var leapers = db.Query<Leaper>(sqlQuery);

                return leapers;
            }
        }
    }
}

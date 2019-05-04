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
                var sqlQuery = "SELECT id, name, budget FROM Leapers";

                var leapers = db.Query<Leaper>(sqlQuery).ToList();

                return leapers;
            }
        }

        public Leaper AddLeaper(string name, decimal budget)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var leaperRepo = new LeaperRepository();

                var insertQuery = @"
                    INSERT INTO [dbo].[Leapers]
                               ([Name]
                               ,[Budget])
                    OUTPUT inserted.*
                         VALUES
                               (@name
                               ,@budget)";

                var parameters = new
                {
                    Name = name,
                    Budget = budget,
                };

                var newLeaper = db.QueryFirstOrDefault<Leaper>(insertQuery, parameters);

                if (newLeaper != null)
                {
                    return newLeaper;
                }
            }
            throw new Exception("Leaper was not created");
        }
    }
}

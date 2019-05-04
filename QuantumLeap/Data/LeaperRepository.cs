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

        public Leaper UpdateLeaper(Leaper leaperToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updateQuery = @"
                            UPDATE Leapers
                            SET Name = @name,
                                Budget = @budget
                            WHERE id = @id";

                var rowsAffected = db.Execute(updateQuery, leaperToUpdate);

                if (rowsAffected == 1)
                    return leaperToUpdate;
            }
            throw new Exception("Could not update leaper");
        }

        public void DeleteLeaper(int leaperId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameter = new { Id = leaperId };

                var deleteQuery = "DELETE From Leapers WHERE Id = @id";

                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("Leaper was not deleted");
                }
            }
        }
    }
}

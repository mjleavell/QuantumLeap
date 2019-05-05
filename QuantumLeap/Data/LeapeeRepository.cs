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

        public Leapee UpdateLeapee(Leapee leapeeToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updateQuery = @"
                            UPDATE Leapees
                            SET Name = @name
                            WHERE id = @id";

                var rowsAffected = db.Execute(updateQuery, leapeeToUpdate);

                if (rowsAffected == 1)
                    return leapeeToUpdate;
            }
            throw new Exception("Could not update leapee");
        }

        public void DeleteLeapee(int leapeeId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var deleteQuery = "DELETE From Leapees WHERE Id = @id";

                var parameter = new { Id = leapeeId };

                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("Leapee was not deleted");
                }
            }
        }

    }
}
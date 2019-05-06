using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Data
{
    public class LeapeeEventsRepository
    {
        const string ConnectionString = "Server = localhost; Database = QuantumLeap; Trusted_Connection = True;";

        public IEnumerable<LeapeeEvent> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var getQuery = "SELECT id, leapeeid, eventid FROM leapeeEvents";

                var leapeeEvents = db.Query<LeapeeEvent>(getQuery).ToList();

                return leapeeEvents;
            }
        }
    }
}

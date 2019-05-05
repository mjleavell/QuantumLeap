using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace QuantumLeap.Data
{
    public class EventRepository
    {
        const string ConnectionString = "Server = localhost; Database = QuantumLeap; Trusted_Connection = True;";

        public IEnumerable<Event> GetEvents()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var getQuery = "SELECT id, name, eventDate, isCorrected FROM events";

                var events = db.Query<Event>(getQuery).ToList();

                return events;
            }
        }

    }
}
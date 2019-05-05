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

        public Event AddEvent(string name, DateTime eventDate, bool isCorrected)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var eventRepo = new EventRepository();

                var insertQuery = @"
                    INSERT INTO [dbo].[Events]
                                ([Name]
                                ,[EventDate]
                                ,[IsCorrected])
                    OUTPUT inserted.*
                            VALUES
                                (@name
                                ,@eventDate
                                ,@isCorrected)";

                var parameters = new
                {
                    Name = name,
                    EventDate = eventDate,
                    IsCorrected = isCorrected
                };

                var newEvent = db.QueryFirstOrDefault<Event>(insertQuery, parameters);

                if (newEvent != null)
                {
                    return newEvent;
                }
            }
            throw new Exception("Event was not created");
        }

        public Event UpdateEvent(Event eventToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updateQuery = @"
                            UPDATE Events
                            SET Name = @name,
                                EventDate = @eventDate,
                                IsCorrected = @isCorrected
                            WHERE id = @id";

                var rowsAffected = db.Execute(updateQuery, eventToUpdate);

                if (rowsAffected == 1)
                    return eventToUpdate;
            }
            throw new Exception("Could not update event");
        }

        public void DeleteEvent(int eventId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var deleteQuery = "DELETE From Events WHERE Id = @id";

                var parameter = new { Id = eventId };

                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("Leapee was not deleted");
                }
            }
        }

    }
}
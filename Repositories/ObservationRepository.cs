using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateObservationApp.Model;
using Npgsql;

namespace ClimateObservationApp.Repositories
{
    public class ObservationRepository
    {

        private static readonly string connectionString = "Server=localhost;Port=5432;Database=Klimatdatabas;User ID=demouser; Password=BlizzardRustler10";


        private bool success;
        /// <summary>
        /// Adds an observer
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public Observer AddObserver(Observer observer)
        {
            try
            {
                string stmt = "insert into observer(firstname, lastname) values(@firstname, @lastname) Returning id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var command = new NpgsqlCommand(stmt, conn);

                //command.Parameters.AddWithValue("id", observer.Id);
                command.Parameters.AddWithValue("firstname", observer.FirstName);
                command.Parameters.AddWithValue("lastname", observer.LastName);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    observer.Id = (int)reader["id"];

                }

                return observer;

            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Updates measurments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateMeasurment(int id, double value)
        {
            try
            {
                bool update = false;
                string stmt = "update measurement set value = @value where id = @id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var command = new NpgsqlCommand(stmt, conn);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("value", value);

                command.ExecuteNonQuery();

                return update;

            }
            catch (PostgresException ex)
            {
                string errorCode = ex.SqlState;
                throw new Exception("fail...", ex);
            }
        }


        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategory(int id)
        {
            try
            {
                string stmt = "select * from category where id = @id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                command.Parameters.AddWithValue("id", id);
                using var reader = command.ExecuteReader();
                Category category = new Category();
                while (reader.Read())
                {
                    category.Id = (int)reader["id"];
                    category.Name = Convert.IsDBNull(reader["name"]) ? null : (string)reader["name"];
                    category.Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"];
                    category.Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"];
                }
                return category;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Gets categories
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories() {
            try
            {
                string stmt = "select * from category where id = 2 or id = 3 or id = 21";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                using var reader = command.ExecuteReader();
                Category category = null;
                var categories = new List<Category>();
                while (reader.Read())
                {
                    category = new Category();
                    {
                        category.Id = (int)reader["id"];
                        category.Name = Convert.IsDBNull(reader["name"]) ? null : (string)reader["name"];
                        category.Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"];
                        category.Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"];
                    };

                    categories.Add(category);

                }

                return categories;
            }
            catch (PostgresException ex)
            {
                string errorCode = ex.SqlState;
                throw new Exception("fail...", ex);
            }


        }
        
        /// <summary>
        /// Gets a unit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Unit GetUnit(int? id)
        {
            try
            {
                string stmt = "select * from unit where id = @id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                command.Parameters.AddWithValue("id", id);
                using var reader = command.ExecuteReader();
                Unit unit = null;
                while (reader.Read())

                    unit = new Unit

                    {

                        Abbrevation = (string)reader["abbrevation"],
                        Type = (string)reader["type"],
                        Id = (int?)reader["id"]


                    };

                return unit;
            }
            catch (PostgresException ex)
            {

                string errorCode = ex.SqlState;
                throw new Exception("fail...", ex);
            }



        }



        /// <summary>
        /// Gets all subcategories based on  category_id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Category> GetSubCategories(int id)

        {
            try
            {
                string stmt = "select * from category where basecategory_id = @id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var command = new NpgsqlCommand(stmt, conn);

                command.Parameters.AddWithValue("id", id);

                using var reader = command.ExecuteReader();
                Category category = null;
                var categories = new List<Category>();
                while (reader.Read())

                {

                    category = new Category();

                    {

                        category.Id = (int)reader["id"];
                        category.Name = Convert.IsDBNull(reader["name"]) ? null : (string)reader["name"];
                        category.Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"];
                        category.Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"];



                    };

                    categories.Add(category);

                }

                return categories;
            }
            catch (PostgresException ex)
            {
                string errorCode = ex.SqlState;
                throw new Exception("fail...", ex);
            }


        }

        /// <summary>
        /// Gets all observers
        /// </summary>
        /// <returns></returns>
        public List<Observer> Getobservers() {
            try
            {
                string stmt = "select * from observer order by lastname";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                using var reader = command.ExecuteReader();
                Observer observer = null;
                var observers = new List<Observer>();
                while (reader.Read())

                {
                    observer = new Observer

                    {

                        Id = (int)reader["id"],
                        FirstName = (string)reader["firstname"],
                        LastName = (string)reader["lastname"]


                    };

                    observers.Add(observer);

                }

                return observers;
            }
            catch (PostgresException ex)
            {
                string errorCode = ex.SqlState;
                throw new Exception("fail...", ex);

            }


        }


        /// <summary>
        /// Deletes picked observer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteObserver(int id)
        {


            try
            {
                string stmt = "delete from observer where id = @id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                command.Parameters.AddWithValue("id", id);
                using var reader = command.ExecuteReader();

                success = true;
                return success;

            }
            catch (PostgresException ex)
            {
                throw new Exception("Du kan inte ta bort en användare som genomfört en observation");

            }


        }

        /// <summary>
        /// Gets all observations from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Observation> GetObservations(int id)

        {
            try
            {
                string stmt = "select * from observation where observer_id = @id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                command.Parameters.AddWithValue("id", id);
                using var reader = command.ExecuteReader();
                Observation observation = null;
                var observations = new List<Observation>();
                while (reader.Read())

                {
                    observation = new Observation

                    {
                        Id = (int)reader["id"],
                        Date = (DateTime)reader["Date"],
                        Observer_Id = (int)reader["observer_id"],
                        Geolocation_Id = (int)reader["geolocation_id"]
                    };

                    observations.Add(observation);

                }

                return observations;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// Gets all measurments realated to an observation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Measurement> GetMeasurements(int id)

        {
            try
            {
                string stmt = "select * from measurement where observation_id = @id order by id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var command = new NpgsqlCommand(stmt, conn);
                command.Parameters.AddWithValue("id", id);
                using var reader = command.ExecuteReader();
                Measurement measurement;
                var measurements = new List<Measurement>();

                while (reader.Read())

                {
                    {
                        measurement = new Measurement

                        {
                            Id = (int)reader["id"],
                            Value = (double)reader["value"],
                            Observation_id = (int)reader["observation_id"],
                            Category_id = (int)reader["category_id"]

                        };

                        measurements.Add(measurement);

                    }

                }
                return measurements;
            }
            catch (Exception)
            {

                throw;
            }

        }
       
        /// <summary>
        /// Gets all area where observations is possible
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAreas() {
            try
            {
                string stmt = "select * from area";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var command = new NpgsqlCommand(stmt, conn);


                using var reader = command.ExecuteReader();
                Area area = null;
                var areas = new List<Area>();
                while (reader.Read())

                {
                    area = new Area

                    {
                        Id = (int)reader["id"],
                        Country_Id = (int)reader["country_id"],
                        Name = (string)reader["name"]
                    };

                    areas.Add(area);

                }

                return areas;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Gets id from the selected area
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Geolocation GetGeolocationID(int id)
        {
            try
            {
                string stmt = "select * from geolocation where area_id = @area_id";
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var command = new NpgsqlCommand(stmt, conn);
                command.Parameters.AddWithValue("area_id", id);
                using var reader = command.ExecuteReader();

                Geolocation geolocation = null;
                while (reader.Read())
                {
                    geolocation = new Geolocation
                    {
                        Id = (int)reader["id"],
                        Latitude = (double)reader["latitude"],
                        Longitude = (double)reader["longitude"],
                        Area_Id = (int)reader["area_id"]
                    };
                }
                return geolocation;
            }
            catch (Exception)
            {

                throw;
            }

        }



        #region transaction

        /// <summary>
        /// Adds observation with measurments with transaction
        /// </summary>
        /// <param name="observation"></param>
        /// <param name="measurements"></param>
        /// <param name="geoId"></param>
        /// <returns></returns>
        /// 
        public Observation AddObservation(Observation observation, List<Measurement> measurements, int geoId)
        {
            string stmt = "insert into observation (date, observer_id, geolocation_id) values (@date, @observerId, @geoId) Returning id";
            string stmt2 = "insert into measurement (value, observation_id, category_id) values (@value, @observationId, @categoryId) Returning id";
            int id = 0;
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var transaction = conn.BeginTransaction();
            try
            {

                using var command = new NpgsqlCommand(stmt, conn);
                using var command2 = new NpgsqlCommand(stmt2, conn);

                command.Parameters.AddWithValue("date", observation.Date);
                command.Parameters.AddWithValue("observerId", observation.Observer_Id);
                command.Parameters.AddWithValue("geoId", geoId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    observation.Id = (int)reader["id"];
                    id = observation.Id;
                }
                reader.Close();
                foreach (var m in measurements)
                {
                    command2.Parameters.AddWithValue("value", m.Value);
                    command2.Parameters.AddWithValue("observationId", id);
                    command2.Parameters.AddWithValue("categoryId", m.Category_id);
                    var reader2 = command2.ExecuteReader();
                    command.Parameters.Clear();

                    while (reader2.Read())
                    {
                        m.Id = (int)reader["id"];
                    }
                    reader2.Close();
                }



                transaction.Commit();
                return observation;
            }
            catch (PostgresException ex)
            {
                transaction.Rollback();
                string errorCode = ex.SqlState;
                throw new Exception("fail...", ex);
            }
        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Project.Data;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.Repository
{
    class SensorDataRepository
    {
        private readonly string connectionString;

        public SensorDataRepository()
        {
            this.connectionString = DatabaseHelper.GetConnection().ConnectionString;
        }

        // DB Connection 연결
        private MySqlConnection GetOpenConnection()
        {
            var connection = new MySqlConnection(this.connectionString);
            connection.Open();
            return connection;
        }

        // metaInfoId로 SensorData 조회
        public SensorData FindSensorDataById(int metaInfoId)
        {
            SensorData sensorData = null;

            // DB Connection 연결
            using (var connection = GetOpenConnection())
            {
                // SQL Query 작성
                string query = @"
            SELECT meta_info_id, ntc, pm10, pm2_5, pm1_0, ct1, ct2, ct3, ct4
            FROM sensor_data
            WHERE meta_info_id = @metaInfoId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@metaInfoId", metaInfoId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sensorData = MapReaderTosensorData(reader);
                        }
                    }
                }
            }
            return sensorData;
        }

        // metaInfoId로 Ntc 조회
        public SensorData FindNtcById(int metaInfoId)
        {
            SensorData sensorData = null;

            // DB Connection 연결
            using (var connection = GetOpenConnection())
            {
                // SQL Query 작성
                string query = @"
            SELECT meta_info_id, ntc
            FROM sensor_data
            WHERE meta_info_id = @metaInfoId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@metaInfoId", metaInfoId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sensorData = MapReaderTosensorData(reader);
                        }
                    }
                }
            }
            return sensorData;
        }

        // metaInfoId로 Pm 조회
        public SensorData FindPmById(int metaInfoId)
        {
            SensorData sensorData = null;

            // DB Connection 연결
            using (var connection = GetOpenConnection())
            {
                // SQL Query 작성
                string query = @"
            SELECT meta_info_id, pm10, pm2_5, pm1_0
            FROM sensor_data
            WHERE meta_info_id = @metaInfoId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@metaInfoId", metaInfoId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sensorData = MapReaderTosensorData(reader);
                        }
                    }
                }
            }
            return sensorData;
        }

        // metaInfoId로 Ct 조회
        public SensorData FindCtById(int metaInfoId)
        {
            SensorData sensorData = null;

            // DB Connection 연결
            using (var connection = GetOpenConnection())
            {
                // SQL Query 작성
                string query = @"
            SELECT meta_info_id, ct1, ct2, ct3, ct4
            FROM sensor_data
            WHERE meta_info_id = @metaInfoId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@metaInfoId", metaInfoId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sensorData = MapReaderTosensorData(reader);
                        }
                    }
                }
            }
            return sensorData;
        }

        private SensorData MapReaderTosensorData(MySqlDataReader reader)
        {
            return new SensorData
            {
                MetaInfoId = reader.GetInt32("meta_info_id"),
                Ntc = reader.GetDouble("ntc"),
                Pm10 = reader.GetDouble("pm10"),
                Pm2_5 = reader.GetDouble("pm2_5"),
                Pm1_0 = reader.GetDouble("pm1_0"),
                Ct1 = reader.GetDouble("ct1"),
                Ct2 = reader.GetDouble("ct2"),
                Ct3 = reader.GetDouble("ct3"),
                Ct4 = reader.GetDouble("ct4"),
            };
        }
    }
}

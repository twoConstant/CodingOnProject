using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Project.Data;
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

        // DB Connection
        private MySqlConnection GetOpenConnection()
        {
            var connection = new MySqlConnection(this.connectionString);
            connection.Open();
            return connection;
        }

        // 중복 제거된 SensorData 조회 (machineId 기반)
        public List<SensorData> FindSensorDataByMachineId(int machineId)
        {
            List<SensorData> sensorDataList = new List<SensorData>();

            using (var connection = GetOpenConnection())
            {
                string query = @"
                    SELECT 
                        sd.meta_info_id,
                        mi.collection_date_time,
                        sd.ntc, sd.pm10, sd.pm2_5, sd.pm1_0, 
                        sd.ct1, sd.ct2, sd.ct3, sd.ct4
                    FROM 
                        sensor_data sd
                    JOIN 
                        meta_info mi ON sd.meta_info_id = mi.meta_info_id
                    WHERE 
                        mi.machine_id = @machineId
                        AND mi.meta_info_id = (
                            SELECT MAX(meta_info_id)
                            FROM meta_info
                            WHERE collection_date_time = mi.collection_date_time
                              AND machine_id = @machineId
                        )
                    ORDER BY 
                        mi.collection_date_time;";

                using (var command = new MySqlCommand(query, connection))
                {
                    // 파라미터 추가
                    command.Parameters.AddWithValue("@machineId", machineId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sensorDataList.Add(MapReaderTosensorData(reader));
                        }
                    }
                }
            }

            return sensorDataList;
        }

        private SensorData MapReaderTosensorData(MySqlDataReader reader)
        {
            return new SensorData
            {
                MetaInfoId = reader.GetInt32("meta_info_id"),
                CollectionDateTime = reader.GetDateTime("collection_date_time"),
                Ntc = reader.GetDouble("ntc"),
                Pm10 = reader.GetDouble("pm10"),
                Pm2_5 = reader.GetDouble("pm2_5"),
                Pm1_0 = reader.GetDouble("pm1_0"),
                Ct1 = reader.GetDouble("ct1"),
                Ct2 = reader.GetDouble("ct2"),
                Ct3 = reader.GetDouble("ct3"),
                Ct4 = reader.GetDouble("ct4")
            };
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Project.Data;
using Project.Models;

namespace Project.Repository
{
    class Sensor_dataRepository : ISensorDataRepository
    {
        // ConnectionString 가져오기
        private readonly string connectionString;

        // 생성자
        public Sensor_dataRepository()
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


        // meta_info_id로 sensor_data 찾기
        public Sensor_data FindSensordataByMachineID(int machine_id, string sensorValue, string startDate, string endDate)
        {
            Sensor_data sensor_data = null;

            using (var connection = GetOpenConnection())
            {
                string query = @"
                SELECT 
                    m.meta_info_id, 
                    m.machine_id, 
                    m.error_log_id, 
                    m.collection_date_time AS collection_data_time, -- 별칭 설정
                    m.state,
                    s.ntc, 
                    s.pm10, 
                    s.pm2_5, 
                    s.pm1_0, 
                    s.ct1, 
                    s.ct2, 
                    s.ct3, 
                    s.ct4
                FROM 
                    meta_info m
                INNER JOIN 
                    sensor_data s
                ON 
                    m.meta_info_id = s.meta_info_id
                WHERE 
                    m.machine_id = @machine_id AND
                    m.collection_date_time BETWEEN @startTime AND @endTime;
                ";

                Console.WriteLine("[INFO] SQL Query:");
                Console.WriteLine(query);

                startDate = startDate.Substring(0, 10) + " 00:00:00";
                endDate = endDate.Substring(0, 10) + " 23:59:59";


                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machine_id", machine_id);
                    command.Parameters.AddWithValue("@startTime", startDate);
                    command.Parameters.AddWithValue("@endTime", endDate);

                    Console.WriteLine("[INFO] Query Parameters:");
                    Console.WriteLine($"machine_id: {machine_id}");
                    Console.WriteLine($"startTime: {startDate}");
                    Console.WriteLine($"endTime: {endDate}");

                    Console.WriteLine("[INFO] Executing SQL query.");

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("[INFO] Data found, mapping to Sensor_data.");
                            sensor_data = MapReaderToSeonsor_data(reader);
                        
                            Console.WriteLine("[INFO] Mapped Sensor Data:");
                            Console.WriteLine($"Meta Info ID: {sensor_data.meta_info_id}");
                            //Console.WriteLine($"Machine ID: {sensor_data.machine_id}");
                            //Console.WriteLine($"Error Log ID: {sensor_data.ErrorLogId}");
                            Console.WriteLine($"Collection Data Time: {sensor_data.collection_data_time}");
                            //Console.WriteLine($"State: {sensor_data.state}");
                            Console.WriteLine($"NTC: {sensor_data.ntc}");
                            Console.WriteLine($"PM10: {sensor_data.pm10}");
                            Console.WriteLine($"PM2.5: {sensor_data.pm2_5}");
                            Console.WriteLine($"CT1: {sensor_data.ct1}");
                            Console.WriteLine($"CT2: {sensor_data.ct2}");
                            Console.WriteLine($"CT3: {sensor_data.ct3}");
                            Console.WriteLine($"CT4: {sensor_data.ct4}");
                        }
                        else
                        {
                            Console.WriteLine("[WARN] No data found for the given query.");
                        }
                    }
                }
            }
            return sensor_data;
        }


        public List<Sensor_data> FindSensordataListByMachineID(int machine_id, string sensorValue, string startDate, string endDate)
        {
            List<Sensor_data> metasensorData = new List<Sensor_data>();

            using (var connection = GetOpenConnection())
            {
                string query = @"
                SELECT 
                    m.meta_info_id, 
                    m.machine_id, 
                    m.error_log_id, 
                    m.collection_date_time AS collection_data_time, -- 별칭 설정
                    m.state,
                    s.ntc, 
                    s.pm10, 
                    s.pm2_5, 
                    s.pm1_0, 
                    s.ct1, 
                    s.ct2, 
                    s.ct3, 
                    s.ct4
                FROM 
                    meta_info m
                INNER JOIN 
                    sensor_data s
                ON 
                    m.meta_info_id = s.meta_info_id
                WHERE 
                    m.machine_id = @machine_id AND
                    m.collection_date_time BETWEEN @startTime AND @endTime;
                ";

                Console.WriteLine("[INFO] SQL Query:");
                Console.WriteLine(query);

                startDate = startDate.Substring(0, 10) + " 00:00:00";
                endDate = endDate.Substring(0, 10) + " 23:59:59";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machine_id", machine_id);
                    command.Parameters.AddWithValue("@startTime", startDate);
                    command.Parameters.AddWithValue("@endTime", endDate);

                    Console.WriteLine("[INFO] Query Parameters:");
                    Console.WriteLine($"machine_id: {machine_id}");
                    Console.WriteLine($"startTime: {startDate}");
                    Console.WriteLine($"endTime: {endDate}");

                    Console.WriteLine("[INFO] Executing SQL query.");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            metasensorData.Add(MapReaderToSeonsor_data(reader));
                        }
                    }
                }
            }
            //foreach (var item in metasensorData)
            //{
            //    Console.WriteLine(item.meta_info_id);
            //}
            Console.WriteLine("FindSensordataListByMachineID 실행완료");
            //Console.WriteLine("MetaSens   orData : " + metasensorData[0].ntc);
            return metasensorData; 
        }


        private Sensor_data MapReaderToSeonsor_data(MySqlDataReader reader)
        {
            return new Sensor_data
            {
                //meta_info_id = reader.GetInt32("meta_info_id"),

                //ntc = reader.IsDBNull(reader.GetOrdinal("ntc")) ? (double?)null : reader.GetDouble("ntc"),

                //pm10 = reader.IsDBNull(reader.GetOrdinal("pm10")) ? (double?)null : reader.GetDouble("pm10"),
                //pm2_5 = reader.IsDBNull(reader.GetOrdinal("pm2_5")) ? (double?)null : reader.GetDouble("pm2_5"),
                //pm1_0 = reader.IsDBNull(reader.GetOrdinal("pm1_0")) ? (double?)null : reader.GetDouble("pm1_0"),

                //ct1 = reader.IsDBNull(reader.GetOrdinal("ct1")) ? (double?)null : reader.GetDouble("ct1"),
                //ct2 = reader.IsDBNull(reader.GetOrdinal("ct2")) ? (double?)null : lq
                //reader.GetDouble("ct2"),
                //ct3 = reader.IsDBNull(reader.GetOrdinal("ct3")) ? (double?)null : reader.GetDouble("ct3"),
                //ct4 = reader.IsDBNull(reader.GetOrdinal("ct4")) ? (double?)null : reader.GetDouble("ct4")

                meta_info_id = reader.GetInt32("meta_info_id"),

                ntc = reader.GetDouble("ntc"),

                pm10 = reader.GetDouble("pm10"),
                pm2_5 = reader.GetDouble("pm2_5"),
                pm1_0 = reader.GetDouble("pm1_0"),

                ct1 = reader.GetDouble("ct1"),
                ct2 = reader.GetDouble("ct2"),
                ct3 = reader.GetDouble("ct3"),
                ct4 = reader.GetDouble("ct4"),

                collection_data_time = reader.IsDBNull(reader.GetOrdinal("collection_data_time"))
            ? (DateTime?)null // Handle NULL values
            : reader.GetDateTime("collection_data_time"),
            }; 
        }
    }
}

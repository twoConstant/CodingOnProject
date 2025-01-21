using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Project.Data;
using Project.Models;

namespace Project.Repository
{
    public class MachineCurrStateRepository : IMachineCurrStateRepository
    {
        private readonly string connectionString;
        // 가동률 랜덤값을 생성하는 객체
        private readonly Random random = new Random();

        // 생성자
        public MachineCurrStateRepository()
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


        // 필터링에 따른 동적 쿼리 실행
        public List<MachineCurrState> FindMachineCurrStateByFilters(string manufacturer, string deviceType, string state)
        {
            List<MachineCurrState> machineStates = new List<MachineCurrState>();

            using (var connection = GetOpenConnection())
            {
                // 기본 쿼리
                StringBuilder query = new StringBuilder(@"
                    WITH meta_info_latest AS (
                        SELECT *
                        FROM meta_info
                        WHERE (machine_id, collection_date_time, meta_info_id) IN (
                            SELECT machine_id, MAX(collection_date_time), MAX(meta_info_id)
                            FROM meta_info
                            GROUP BY machine_id
                        )
                    )
                    SELECT 
                        Machine.machine_id,
                        Machine.device_id,
                        Machine.type,
                        MetaLatest.state,
                        Sensor.ntc,
                        Sensor.pm10,
                        Sensor.ct1
                    FROM machine AS Machine
                    LEFT JOIN meta_info_latest AS MetaLatest ON Machine.machine_id = MetaLatest.machine_id
                    LEFT JOIN sensor_data AS Sensor ON MetaLatest.meta_info_id = Sensor.meta_info_id
                    WHERE 1=1
                ");

                // 동적 필터 조건 추가
                if (!string.IsNullOrEmpty(manufacturer))
                {
                    query.Append(" AND Machine.device_manufacturer = @manufacturer");
                }

                if (!string.IsNullOrEmpty(deviceType))
                {
                    query.Append(" AND Machine.type = @deviceType");
                }

                if (!string.IsNullOrEmpty(state))
                {
                    query.Append(" AND MetaLatest.state = @state");
                }

                // 쿼리 디버깅 출력
                Console.WriteLine("===== SQL Query =====");
                Console.WriteLine(query.ToString());

                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    // 파라미터 값 추가 및 디버깅
                    if (!string.IsNullOrEmpty(manufacturer))
                    {
                        command.Parameters.AddWithValue("@manufacturer", manufacturer);
                        Console.WriteLine($"@manufacturer: {manufacturer}");
                    }

                    if (!string.IsNullOrEmpty(deviceType))
                    {
                        command.Parameters.AddWithValue("@deviceType", deviceType);
                        Console.WriteLine($"@deviceType: {deviceType}");
                    }

                    if (!string.IsNullOrEmpty(state))
                    {
                        command.Parameters.AddWithValue("@state", state);
                        Console.WriteLine($"@state: {state}");
                    }

                    Console.WriteLine("=====================");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            machineStates.Add(MapReaderToMachineCurrState(reader));
                        }
                    }
                }
            }

            return machineStates;
        }

        // Reader를 MachineCurrState로 매핑
        private MachineCurrState MapReaderToMachineCurrState(MySqlDataReader reader)
        {
            return new MachineCurrState
            {
                MachineId = reader.GetInt32("machine_id"),
                DeviceId = reader["device_id"] as string,
                Type = reader.GetInt32("type"),
                State = reader.IsDBNull(reader.GetOrdinal("state")) ? (int?)null : reader.GetInt32("state"),
                OperatingRate = random.Next(70, 100),
                Ntc = reader.IsDBNull(reader.GetOrdinal("ntc")) ? (double?)null : reader.GetDouble("ntc"),
                Pm10 = reader.IsDBNull(reader.GetOrdinal("pm10")) ? (double?)null : reader.GetDouble("pm10"),
                Ct1 = reader.IsDBNull(reader.GetOrdinal("ct1")) ? (double?)null : reader.GetDouble("ct1")
            };
        }

    }
}

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
    class MachineRepository : IMachineRepository
    {
        // ConnectionString 가져오기
        private readonly string connectionString;

        // 생성자
        public MachineRepository()
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


        // Machine ID로 Machine 찾기
        public Machine FindMachineById(int machineId)
        {
            Machine machine = null;

            // DB Connection 연결
            using (var connection = GetOpenConnection())
            {
                // SQL Query 작성
                string query = @"
            SELECT machine_id, device_id, device_manufacturer, device_name, type
            FROM machine
            WHERE machine_id = @machineId";

                // 로깅: 매개변수와 쿼리 출력
                Console.WriteLine("===== SQL Query Execution =====");
                Console.WriteLine($"Query: {query}");
                Console.WriteLine($"Parameter: @machineId = {machineId}");

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machineId", machineId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            machine = MapReaderToMachine(reader);
                        }
                    }
                }
            }

            // 로깅: 반환된 결과
            Console.WriteLine("===== Query Result =====");
            if (machine != null)
            {
                Console.WriteLine($"Machine Found: ID={machine.MachineId}, DeviceID={machine.DeviceId}, Manufacturer={machine.DeviceManufacturer}");
            }
            else
            {
                Console.WriteLine("No Machine Found for the given ID.");
            }

            return machine;
        }

        private Machine MapReaderToMachine(MySqlDataReader reader)
        {
            return new Machine
            {
                MachineId = reader.GetInt32("machine_id"),
                DeviceId = reader["device_id"] as string,
                DeviceManufacturer = reader["device_manufacturer"] as string,
                DeviceName = reader["device_name"] as string,
                Type = reader.GetInt32("type")
            };
        }
    }
}

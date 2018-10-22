using MQTTDataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTDataProvider.Classes
{
    class SetLHDescriptions
    {
        public static void SetDescriptions()
        {
            var names = new List<string>
            {
                "ESP_TimeStap",
                "IMU1_AccX",
                "IMU1_AccY",
                "IMU1_AccZ",
                "IMU1_GyroX",
                "IMU1_GyroY",
                "IMU1_GyroZ",
                "IMU1_MagX",
                "IMU1_MagY",
                "IMU1_MagZ",
                "IMU1_Q0",
                "IMU1_Q1",
                "IMU1_Q2",
                "IMU1_Q3",
                "IMU2_AccX",
                "IMU2_AccY",
                "IMU2_AccZ",
                "IMU2_GyroX",
                "IMU2_GyroY",
                "IMU2_GyroZ",
                "IMU2_MagX",
                "IMU2_MagY",
                "IMU2_MagZ",
                "IMU2_Q0",
                "IMU2_Q1",
                "IMU2_Q2",
                "IMU2_Q3",
                "Temp_Ext",
                "Humidity_Ext",
                "Temp_Int",
                "Humidity_Int",
                "Pulse_TempLobe",
                "GSR"
            };
            HubConnector.SetValuesName(names);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MQTTDataProvider.MQTTManager
{
    class MQTTManager
    {
        MainWindowView mWindow;
        private bool _isRecording = false;

        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                _isRecording = value;
            }
        }

        public void MQTT_DataAcquired()
        {

            if (_isRecording == true)
            {
                SendData();
            }
        }
        #region Variables

        public string IMU1_AccX;
        public string IMU1_AccY;
        public string IMU1_AccZ;
        public string IMU1_GyroX;
        public string IMU1_GyroY;
        public string IMU1_GyroZ;
        public string IMU1_MagX;
        public string IMU1_MagY;
        public string IMU1_MagZ;
        public string IMU1_Q0;
        public string IMU1_Q1;
        public string IMU1_Q2;
        public string IMU1_Q3;
        public string IMU2_AccX;
        public string IMU2_AccY;
        public string IMU2_AccZ;
        public string IMU2_GyroX;
        public string IMU2_GyroY;
        public string IMU2_GyroZ;
        public string IMU2_MagX;
        public string IMU2_MagY;
        public string IMU2_MagZ;
        public string IMU2_Q0;
        public string IMU2_Q1;
        public string IMU2_Q2;
        public string IMU2_Q3;
        public string Temp_External;
        public string Humidity_External;
        public string Temp_Internal;
        public string Humidity_Internal;
        public string Pulse_TempLobe;
        public string GSR;
        #endregion

        public static ConnectorHub.ConnectorHub myConnector;

        public void SetValueNames()
        {
            List<string> names = new List<string>();
            names.Add("IMU1_AccX");
            names.Add("IMU1_AccY");
            names.Add("IMU1_AccZ");
            names.Add("IMU1_GyroX");
            names.Add("IMU1_GyroY");
            names.Add("IMU1_GyroZ");
            names.Add("IMU1_MagX");
            names.Add("IMU1_MagY");
            names.Add("IMU1_MagZ");
            names.Add("IMU1_Q0");
            names.Add("IMU1_Q1");
            names.Add("IMU1_Q2");
            names.Add("IMU1_Q3");
            names.Add("IMU2_AccX");
            names.Add("IMU2_AccY");
            names.Add("IMU2_AccZ");
            names.Add("IMU2_GyroX");
            names.Add("IMU2_GyroY");
            names.Add("IMU2_GyroZ");
            names.Add("IMU2_MagX");
            names.Add("IMU2_MagY");
            names.Add("IMU2_MagZ");
            names.Add("IMU2_Q0");
            names.Add("IMU2_Q1");
            names.Add("IMU2_Q2");
            names.Add("IMU2_Q3");
            names.Add("Temp_Ext");
            names.Add("Humidity_Ext");
            names.Add("Temp_Int");
            names.Add("Humidity_Int");
            names.Add("Pulse_TempLobe");
            names.Add("GSR");
            myConnector.setValuesName(names);

        }

        public void SendData()
        {
            try
            {
                List<string> values = new List<string>();
                values.Add(IMU1_AccX);
                values.Add(IMU1_AccY);
                values.Add(IMU1_AccZ);
                values.Add(IMU1_GyroX);
                values.Add(IMU1_GyroY);
                values.Add(IMU1_GyroZ);
                values.Add(IMU1_MagX);
                values.Add(IMU1_MagY);
                values.Add(IMU1_MagZ);
                values.Add(IMU1_Q0);
                values.Add(IMU1_Q1);
                values.Add(IMU1_Q2);
                values.Add(IMU1_Q3);
                values.Add(IMU2_AccX);
                values.Add(IMU2_AccY);
                values.Add(IMU2_AccZ);
                values.Add(IMU2_GyroX);
                values.Add(IMU2_GyroY);
                values.Add(IMU2_GyroZ);
                values.Add(IMU2_MagX);
                values.Add(IMU2_MagY);
                values.Add(IMU2_MagZ);
                values.Add(IMU2_Q0);
                values.Add(IMU2_Q1);
                values.Add(IMU2_Q2);
                values.Add(IMU2_Q3);
                values.Add(Temp_External);
                values.Add(Humidity_External);
                values.Add(Temp_Internal);
                values.Add(Humidity_Internal);
                values.Add(Pulse_TempLobe);
                values.Add(GSR);
                myConnector.storeFrame(values);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public MQTTManager()
        {
            myConnector = new ConnectorHub.ConnectorHub();
            myConnector.init();
            myConnector.sendReady();
        }
    }
}


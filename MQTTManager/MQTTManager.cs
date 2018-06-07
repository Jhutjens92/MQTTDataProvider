using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace MQTTDataProvider.MQTTManager
{
    class MQTTManager
    {
        //IMU 1
        private float IMU1_AccX = 0;
        private float IMU1_AccY = 0;
        private float IMU1_AccZ = 0;
        private float IMU1_GyroX = 0;
        private float IMU1_GyroY = 0;
        private float IMU1_GyroZ = 0;
        private float IMU1_MagX = 0;
        private float IMU1_MagY = 0;
        private float IMU1_MagZ = 0;
        //IMU2
        private float IMU2_AccX = 0;
        private float IMU2_AccY = 0;
        private float IMU2_AccZ = 0;
        private float IMU2_GyroX = 0;
        private float IMU2_GyroY = 0;
        private float IMU2_GyroZ = 0;
        private float IMU2_MagX = 0;
        private float IMU2_MagY = 0;
        private float IMU2_MagZ = 0;
        //SHT1x1
        private float temp1 = 0;
        private float humidity1 = 0;
        //SHT1x2
        private float temp2 = 0;
        private float humidity2 = 0;
        //Pulse Sensor 1
        private float pulse_Pulse = 0;
        //Pulse Sensor 2
        private float pulse_TempLobe = 0;
        //GSR Sensor
        private float gsr = 0;

        public static ConnectorHub.ConnectorHub myConnector;
        public ConnectorHub.FeedbackHub myFeedback;
        private bool _isRecording = false;
        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                _isRecording = value;
            }
        }


        public MQTTManager()
        {
            myConnector = new ConnectorHub.ConnectorHub();
            myConnector.init();
            myFeedback = new ConnectorHub.FeedbackHub();
            myFeedback.init();
            myConnector.sendReady();
        }

        #region Send data
        public void SetValueNames()
        {
            List<string> names = new List<string>();
            //IMU1
            names.Add("IMU1_AccX");
            names.Add("IMU1_AccY");
            names.Add("IMU1_AccZ");
            names.Add("IMU1_GyroX");
            names.Add("IMU1_GyroY");
            names.Add("IMU1_GyroZ");
            names.Add("IMU1_MagX");
            names.Add("IMU1_MagY");
            names.Add("IMU1_MagZ");
            //IMU2
            names.Add("IMU2_AccX");
            names.Add("IMU2_AccY");
            names.Add("IMU2_AccZ");
            names.Add("IMU2_GyroX");
            names.Add("IMU2_GyroY");
            names.Add("IMU2_GyroZ");
            names.Add("IMU2_MagX");
            names.Add("IMU2_MagY");
            names.Add("IMU2_MagZ");
            //SHT1x1
            names.Add("temp1");
            names.Add("humidity1");
            //SHT1x2
            names.Add("temp2");
            names.Add("humidity2");
            //Pulse Sensor 1
            names.Add("pulse_Pulse");
            //Pulse Sensor 2
            names.Add("pulse_TempLobe");
            //GSR Sensor
            names.Add("gsr");

            myConnector.setValuesName(names);

        }
        #endregion

        #region MQTT Events
        private void MQTT_DataAcquired(object sender, object e)
        {

            if (_isRecording == true)
            {
                SendData();
            }
        }

      
        #endregion

        /// <summary>
        /// Method to broadcast packets of data
        /// </summary>
        /// <param name="pressure"></param>
        public void SendData()
        {
            try
            {
                List<string> values = new List<string>();
                values.Add(IMU1_AccX.ToString());
                values.Add(IMU1_AccY.ToString());
                values.Add(IMU1_AccZ.ToString());
                values.Add(IMU1_GyroX.ToString());
                values.Add(IMU1_GyroY.ToString());
                values.Add(IMU1_GyroZ.ToString());
                values.Add(IMU1_MagX.ToString());
                values.Add(IMU1_MagY.ToString());
                values.Add(IMU1_MagZ.ToString());
                values.Add(IMU2_AccX.ToString());
                values.Add(IMU2_AccY.ToString());
                values.Add(IMU2_AccZ.ToString());
                values.Add(IMU2_GyroX.ToString());
                values.Add(IMU2_GyroY.ToString());
                values.Add(IMU2_GyroZ.ToString());
                values.Add(IMU2_MagX.ToString());
                values.Add(IMU2_MagY.ToString());
                values.Add(IMU2_MagZ.ToString());
                values.Add(temp1.ToString());
                values.Add(humidity1.ToString());
                values.Add(temp2.ToString());
                values.Add(humidity2.ToString());
                values.Add(pulse_Pulse.ToString());
                values.Add(pulse_TempLobe.ToString());
                values.Add(gsr.ToString());
                Debug.WriteLine("MQTTManager.values" + values.Count);
                Debug.WriteLine("MQTTManager/ The size of value: " + values.Count);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

        }
    }
}


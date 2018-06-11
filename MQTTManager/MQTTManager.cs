namespace MQTTDataProvider.MQTTManager
{
    class MQTTManager
    {
        private bool _isRecording = false;

        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                _isRecording = value;
            }
        }

        private void MQTT_DataAcquired(object sender, object e)
        {

            if (_isRecording == true)
            {
                LHConnector SendLearningHubData = new LHConnector();
                SendLearningHubData.SendLearningHubData();
            }
        }   
    }
}


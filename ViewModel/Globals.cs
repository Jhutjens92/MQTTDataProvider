﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace MQTTDataProvider.ViewModel
{
    public static class Globals
    {
        
        public static bool IsRecordingMqtt
        {
            get { return isRecordingMqtt; }
            set
            {
                isRecordingMqtt = value;
            }
        }
        private static bool isRecordingMqtt = false;

        public static bool JsonErrorMessage
        {
            get { return jsonErrorMessage; }
            set
            {
                jsonErrorMessage = value;
            }
        }
        private static bool jsonErrorMessage = false;
    }
}
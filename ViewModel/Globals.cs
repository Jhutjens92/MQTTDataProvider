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
        private static bool _isRecordingMqtt = false;
        public static bool IsRecordingMqtt
        {
            get { return _isRecordingMqtt; }
            set
            {
                _isRecordingMqtt = value;
            }
        }
    }
}
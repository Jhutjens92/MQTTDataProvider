﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static bool _isRecordingDone = false;
        public static bool IsRecordingDone
        {
            get { return _isRecordingDone; }
            set
            {
                _isRecordingDone = value;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace MQTTDataProvider.ViewModel
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Class containing global variables. </summary>
    ///
    /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class Globals
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Method for setting IsRecordingMqtt. </summary>
        ///
        /// <value> True if this object is recording mqtt, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool IsRecordingMqtt
        {
            get { return isRecordingMqtt; }
            set
            {
                isRecordingMqtt = value;
            }
        }
        private static bool isRecordingMqtt = false;
    }
}
﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shiny.Sensors;

namespace SensorData.Models
{
    public class SensorDataDisplayModel
    {
        public string SensorName { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string AdditionalField { get; set; }
    }

    public class MasterDataModel
    {
        public Dictionary<long, CompassReading> CompassData { get; set; }
        public Dictionary<long, MotionReading> AccelerometerData { get; set; }
        public Dictionary<long, MotionReading> GyroscopeData { get; set; }
        public Dictionary<long, bool> ProximityData { get; set; }
        public Dictionary<long, ushort> HeartRateData { get; set; }
    }

    public class SensorDataResponse : BaseResponse<SensorDataResponse>
    {
        //[JsonProperty("userId")]
        //public int UID { get; set; }
    }
}

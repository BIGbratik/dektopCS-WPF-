using System;

namespace dektopCS.classes
{
    public class CSTask
    {
        public int ID { get; set; }
        public int EmergTypeID { get; set; }
        public string TaskName { get; set; }
        public double EmergLat { get; set; }
        public double EmergLng { get; set; }
        public DateTime dateTime { get; set; }
        public string EmergParams { get; set; }
        public string EmergMeasures { get; set; }
    }
}

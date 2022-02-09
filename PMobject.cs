using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dektopCS
{
    //Класс, описывающий объект Сил и Средств
    public class PMobject
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Structer { get; set; }
        public string Subord { get; set; }
        public string isReady { get; set; }
        public string Count { get; set; }
        public string Place { get; set; }
        public string Phone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesNotes
{
    interface Period
    {
        public double GetAverageMeasurement();


        public double GetMaxGlucoze();


        public double GetMinGlucoze();


        public int GetTotalMesurements();
    }
}

/**
* @author Georgoulas Vasileios
*
* @date - 29/2/2020
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiabetesNotes.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; }

        public DateTime Time { get; }

        public double Glucoze { get; set; }

        public int DayNum { get; set; }

        [JsonIgnore]
        public virtual Day _DayNum { get; set; }

        public Measurement()
        {
            Time = DateTime.Now;
        }

        public Measurement(double _Glucoze)
        {
            Time = DateTime.Now;
            this.Glucoze = _Glucoze;
        }

        public Measurement(int _DayNum, double _Glucoze)
        {
            this.Time = DateTime.Now;
            this.DayNum = _DayNum;
            this.Glucoze = _Glucoze;
        }


    }
}

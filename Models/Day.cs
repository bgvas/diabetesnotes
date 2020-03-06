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
using Microsoft.EntityFrameworkCore;

namespace DiabetesNotes.Models
{
    public class Day : Period
    {

        [Key]
        [Column(Order = 1)]
        public int Id { get; }

        public int DayNum { get; }

        [JsonIgnore]
        public virtual List<Measurement> Measurements { get; set; } = new List<Measurement>(); 

        public int MonthId { get; set; }

        [JsonIgnore]
        public virtual Month _MonthId { get; set; }

        public double GetAverageMeasurement()
        {
            return Measurements.Select(e => e.Glucoze).Average();
        }

        public double GetMaxGlucoze()
        {
            return Measurements.Select(e => e.Glucoze).Max();
        }


        public double GetMinGlucoze()
        {
            return Measurements.Select(e => e.Glucoze).Min();
        }


        public int GetTotalMesurements()
        {
            return Measurements.Count;
        }


        public void AddMeasurement(double _Glucoze)

        {
           Measurements.Add(new Measurement(_Glucoze));
        }

        public List<Measurement> GetAllMeasurementsOfADay(int Id)
        {
            return Measurements.ToList<Measurement>();
        }

        
        public Day()
        {
            this.DayNum = DateTime.Now.Day;
        }

        public Day(int _Month)
        {
            this.DayNum = DateTime.Now.Day;
            this.MonthId = _Month;
            
        }

        public Day(int Day, int Month)
        {
            this.DayNum = Day;
            this.MonthId = Month;
        }
        
        

       
    }
}

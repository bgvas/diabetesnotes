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
    public class Month 
    {
        
        [Key]
        [Column(Order = 1)]
        public int MonthNum { get; }

        [JsonIgnore]
        public virtual List<Day> Days { get; set; } = new List<Day>();

        public int GetTotalMesurements()
        {  
            return Days.Count();
        }

        public double GetAverageMeasurement() 
        {
            return  Days.Select(e => e.GetAverageMeasurement()).Average(); 
        }

        public double GetMaxGlucoze()
        {
            return Days.Select(e => e.GetMaxGlucoze()).Max();
        }

        public double GetMinGlucoze()
        {
            return Days.Select(e => e.GetMinGlucoze()).Min();
        }

       
        public void AddDay(Day NewDay)
        {
            Days.Add(NewDay);
        }


        public Month()
        {
            this.MonthNum = DateTime.Now.Month;
        }

        public Month(int _Month)
        {
            this.MonthNum = _Month;
        }
      
    }
}

/**
* @author Georgoulas Vasileios
*
* @date - 29/2/2020
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetesNotes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DiabetesNotes.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase 
    {

        public readonly DataBaseContext _context;
       
       /// <summary>
       /// Initializes a new instance of the <see cref="MeasurementController"/> class.
       /// </summary>
       /// <param name="context">The context.</param>
       public MeasurementController(DataBaseContext context)
       {
          _context = context;
       }

       

        // Get api/measurement
        /// <summary>
        /// Get All Measurements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<Measurement>> GetAllMeasurements()
        {
            return await _context.Measurement.ToListAsync();
        }

        // POST: api/Measurement
        /// <summary>
        /// Create a new measurement
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddANewMeasurement(Measurement A_Measurement)
        {
            Measurement NewMeasurement = new Measurement(A_Measurement.Glucoze);
            Day NewDay = new Day();
            NewDay.AddMeasurement(A_Measurement.Glucoze);
            Month NewMonth = new Month();
            NewMonth.AddDay(NewDay);

            if (!MonthExistInDB())
            {
               _context.Month.Add(NewMonth);
            }
            else if (!DayExistInDB())
            {
                NewDay.MonthId = DateTime.Now.Month;
                _context.Day.Add(NewDay);
            }
            else
            {
                NewMeasurement.DayNum =GetDayId();
                _context.Measurement.Add(NewMeasurement);
            }
            await _context.SaveChangesAsync();
        }
        
        public bool  MonthExistInDB() // check if exist, in DB, a specific month
        {
            return _context.Month.Where(month => month.MonthNum == DateTime.Now.Month).Any();
        }

        public bool DayExistInDB()  // check if exist, in DB, a specific day of a month
        {
            return _context.Day.Where(day => day.DayNum == DateTime.Now.Day)
                .Select(day => day.MonthId == DateTime.Now.Month).Any();
        }

        public int GetDayId()  // Return DayId, of a specific month's day from DB
        {
            return  _context.Day.Where(day => day.DayNum == DateTime.Now.Day && day.MonthId == DateTime.Now.Month)
                .Select(e => e.Id).FirstOrDefault();
        }
        
    }
}
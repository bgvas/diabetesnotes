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
    public class DayController : ControllerBase
    {
        public readonly DataBaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DayController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DayController(DataBaseContext context)
        {
            _context = context;

        }

        // GET: api/Day/5/5
        /// <summary>
        /// Gets all measurements of a Day.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{day}/{month}")]
        public async Task<ICollection<Measurement>> GetAllMeasurementsOfaDay(int day, int month)
        {
            int Id = await FindTheIdOfADay(day, month);

            return await _context.Measurement.Where(e => e.DayNum.Equals(Id)).ToListAsync();
        }

        // GET: api/Day/max/5/5
        /// <summary>
        /// Gets the max measurement of a Day.
        /// </summary>
        /// <returns></returns>
        [HttpGet("max/{day}/{month}")]
        public async Task<List<Measurement>> GetMaxMeasurementOfADay(int day, int month)
        {
            try
            {
                int Id = await FindTheIdOfADay(day, month);
                double MaxMeasurement = MeasurementListOfADay(Id).Select(e => e.Glucoze).Max();
                return MeasurementListOfADay(Id).Where(e => e.Glucoze.Equals(MaxMeasurement)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Day/min/5/5
        /// <summary>
        /// Gets the min measurement of a Day.
        /// </summary>
        /// <returns></returns>
        [HttpGet("min/{day}/{month}")]
        public async Task<List<Measurement>> GetMinMeasurementOfADay(int day, int month)
        {
            try
            {
                int Id = await FindTheIdOfADay(day, month);
                double MinMeasurement = MeasurementListOfADay(Id).Select(e => e.Glucoze).Max();
                return MeasurementListOfADay(Id).Where(e => e.Glucoze.Equals(MinMeasurement)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Day/average/5/5
        /// <summary>
        /// Gets the measurement average of a Day.
        /// </summary>
        /// <returns></returns>
        [HttpGet("average/{day}/{month}")]
        public async Task<double> GetMeasurementAverageOfADay(int day, int month)
        {
            try
            {
                int Id = await FindTheIdOfADay(day, month);
                return MeasurementListOfADay(Id).Select(e => e.Glucoze).Average();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // GET: api/Day/check/5/5
        /// <summary>
        /// Check if measurements of a day are over normal levels
        /// </summary>
        /// <returns></returns>
        [HttpGet("check/{day}/{month}")]
        public async Task<List<Measurement>> CheckMeasurementsOfADay(int day, int month)
        {
            try
            {
                int Id = await FindTheIdOfADay(day, month);
                return await _context.Measurement.Where(e => e.DayNum.Equals(Id) && e.Glucoze > Helper.NormalGlucozeLevel()).ToListAsync();
               
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> FindTheIdOfADay(int day, int month)
        {
            return await _context.Day.Where(e => e.DayNum.Equals(day) && e.MonthId.Equals(month))
                    .Select(e => e.Id).SingleOrDefaultAsync();
        }
               
        public List<Measurement> MeasurementListOfADay(int id)
        {
            return   _context.Measurement.Where(e => e.DayNum.Equals(id)).ToList();
        }

    }

    
}
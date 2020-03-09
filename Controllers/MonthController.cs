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
    public class MonthController : ControllerBase
    {
        public readonly DataBaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MonthController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/month/max/5
        /// <summary>
        /// Max measurement of month
        /// </summary>
        /// <returns></returns>
        [HttpGet("max/{id}")]
        public async Task<ICollection<Measurement>> GetMaxMeasurementOfMonth(int id)
        {
            double MaxGlucozeOfMonth = MeasurementsOfMonth(id).SelectMany(e => e.Value).ToList().Max(e => e.Glucoze);

            return await _context.Measurement.Where(e => e.Glucoze.Equals(MaxGlucozeOfMonth)).ToListAsync();
        }

        // GET: api/month/min/5
        /// <summary>
        /// Min measurement of month
        /// </summary>
        /// <returns></returns>
        [HttpGet("min/{id}")]
        public async Task<ICollection<Measurement>> GetMinMeasurementOfMonth(int id)
        {
            double MinGlucozeOfMonth =  MeasurementsOfMonth(id).SelectMany(e => e.Value).ToList().Min(e => e.Glucoze);

            return await _context.Measurement.Where(e => e.Glucoze.Equals(MinGlucozeOfMonth)).ToListAsync();
        }

        // GET: api/month/average/5
        /// <summary>
        /// Average measurement of month
        /// </summary>
        /// <returns></returns>
        [HttpGet("average/{id}")]
        public Tuple<double, string> GetAverageMeasurementOfMonth(int id)
        {
            double  AverageMeasurement;
            try
            {
                AverageMeasurement = MeasurementsOfMonth(id).SelectMany(e => e.Value).ToList().Average(e => e.Glucoze);
            }
            catch (Exception)
            {
                return null;
            }
            string AverageComment;
            if (Helper.DifferenceFromNormalGlucoze(AverageMeasurement) > 0)     // checking if the average glucoze of month
            {                                                                   // is over, under or normal.
                AverageComment = "over";
            }
            else if (Helper.DifferenceFromNormalGlucoze(AverageMeasurement) < 0)
            {
                AverageComment = "under";
            }
            else AverageComment = "normal";
            Tuple<double, string> Results = new Tuple<double, string>(AverageMeasurement, AverageComment);
            return Results;
        }

        public Dictionary<int, List<Measurement>> MeasurementsOfMonth(int Month)
        {
            List<int> DayId = _context.Day.Where(e => e.MonthId.Equals(Month)).Select(e => e.Id).ToList();
            Dictionary<int, List<Measurement>> Measurements = new Dictionary<int, List<Measurement>>();
            foreach (int i in DayId)
            {
                Measurements.Add(i, new List<Measurement>(_context.Measurement.Where(e => e.DayNum.Equals(i)).ToList()));
            }
            return Measurements;
        }

    }
}
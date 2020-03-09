/**
* @author Georgoulas Vasileios
*
* @date - 29/2/2020
*/
using DiabetesNotes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesNotes
{
    public static class Helper 

    {
        private const double NORMAL_UPPER_GLUCOZE_LEVEL = 0.99;
        private const double NORMAL_LOWER_GLUCOZE_LEVEL = 0.72;

        public static double NormalGlucozeLevel()
        {
            return NORMAL_UPPER_GLUCOZE_LEVEL;
        } 

        public static bool CheckIfIsNormal(double Glucoze)
        {
            if (Glucoze >= NORMAL_LOWER_GLUCOZE_LEVEL && Glucoze <= NORMAL_UPPER_GLUCOZE_LEVEL)
            {
                return true;
            }
            else return false;
        }

        public static double DifferenceFromNormalGlucoze(double Glucoze)
        {
            if (Glucoze < NORMAL_LOWER_GLUCOZE_LEVEL || Glucoze > NORMAL_UPPER_GLUCOZE_LEVEL)
            {
                return Glucoze - NORMAL_LOWER_GLUCOZE_LEVEL;
            }
            else return 0;
        }
    }
}

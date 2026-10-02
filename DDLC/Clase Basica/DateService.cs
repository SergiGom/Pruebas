using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DDL.Clase_Basica
{
    public class DateService : IDateService
    {
        private readonly IServiceProvider _services;

        public DateService(IServiceProvider services)
        {
            _services = services;
        }

        public bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public bool IsBusinessDay(DateTime date)
        {
            return !IsWeekend(date);
        }

        public int DaysBetween(DateTime start, DateTime end)
        {
            return Math.Abs((end - start).Days);
        }

        public bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        public DateTime AddBusinessDays(DateTime start, int days)
        {
            var current = start;
            int added = 0;
            while (added < days)
            {
                current = current.AddDays(1);
                if (IsBusinessDay(current))
                    added++;
            }
            return current;
        }

        public string GetAge(DateTime birthDate, DateTime? referenceDate = null)
        {
            var refDate = referenceDate ?? DateTime.Now;
            int age = refDate.Year - birthDate.Year;
            if (refDate < birthDate.AddYears(age))
                age--;
            return age.ToString();
        }

        public bool IsDateInRange(DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }

        public int GetWeekOfYear(DateTime date)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Interfaces_para_Moq
{
    public interface IDateService
    {
        bool IsWeekend(DateTime date);
        bool IsBusinessDay(DateTime date);
        int DaysBetween(DateTime start, DateTime end);
        bool IsLeapYear(int year);
        DateTime AddBusinessDays(DateTime start, int days);
        string GetAge(DateTime birthDate, DateTime? referenceDate = null);
        bool IsDateInRange(DateTime date, DateTime start, DateTime end);
        int GetWeekOfYear(DateTime date);
    }

}

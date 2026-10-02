using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Clase_Basica
{
    public class TimeService : ITimeService
    {
        public bool IsWithinWorkingHours(DateTime time, TimeSpan? start = null, TimeSpan? end = null)
        {
            var workStart = start ?? new TimeSpan(9, 0, 0);
            var workEnd = end ?? new TimeSpan(17, 0, 0);
            var currentTime = new TimeSpan(time.Hour, time.Minute, time.Second);
            return currentTime >= workStart && currentTime <= workEnd;
        }

        public TimeSpan Difference(TimeSpan t1, TimeSpan t2)
        {
            return (t1 - t2).Duration();
        }

        public bool IsTimeInRange(TimeSpan time, TimeSpan start, TimeSpan end)
        {
            if (start <= end)
                return time >= start && time <= end;
            // Rango que cruza medianoche
            return time >= start || time <= end;
        }

        public string FormatDuration(TimeSpan duration)
        {
            var totalMinutes = (int)duration.TotalMinutes;
            var hours = totalMinutes / 60;
            var minutes = totalMinutes % 60;
            return $"{hours}h {minutes}m";
        }

        public bool IsOvertime(TimeSpan workDuration)
        {
            return workDuration > TimeSpan.FromHours(8);
        }
    }

}

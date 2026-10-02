using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Interfaces_para_Moq
{
    public interface ITimeService
    {
        bool IsWithinWorkingHours(DateTime time, TimeSpan? start = null, TimeSpan? end = null);
        TimeSpan Difference(TimeSpan t1, TimeSpan t2);
        bool IsTimeInRange(TimeSpan time, TimeSpan start, TimeSpan end);
        string FormatDuration(TimeSpan duration);
        bool IsOvertime(TimeSpan workDuration);
    }

}

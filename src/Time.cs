// <copyright file="Time.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create TimeParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum TimeType
{
    Current = 0,
    Hold = 1
}

public readonly record struct TimeParameters(
    int Year,
    int Month,
    int Day,
    int Hour,
    int Minute,
    int Second)
{
    public string ParameterString =>
        $",{Year},{Month},{Day},{Hour},{Minute},{Second}";
}

public static class Time
{
    public static TimeParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');
        
        return new TimeParameters(
            Year: int.Parse(split[2]),
            Month: int.Parse(split[3]),
            Day: int.Parse(split[4]),
            Hour: int.Parse(split[5]),
            Minute: int.Parse(split[6]),
            Second: int.Parse(split[7]));
    }
    
    public static TimeParameters CreateParameters(
        int year,
        int month,
        int day,
        int hour,
        int minute,
        int second)
    {
        int _year;
        int _month;
        int _day;
        int _hour;
        int _minute;
        int _second;

        if (year is >= 0 and <= 99)
        {
            _year = year;
        }
        else
        {
            throw new ArgumentException($"Year Invalid (0 to 99): {year}");
        }

        if (month is >= 1 and <= 12)
        {
            _month = month;
        }
        else
        {
            throw new ArgumentException($"Month Invalid (1 to 12): {month}");
        }

        if (day is >= 1 and <= 31)
        {
            _day = day;
        }
        else
        {
            throw new ArgumentException($"Day Invalid (1 to 31): {day}");
        }

        if (hour is >= 0 and <= 23)
        {
            _hour = hour;
        }
        else
        {
            throw new ArgumentException($"Hour Invalid (0 to 23): {hour}");
        }

        if (minute is >= 0 and <= 59)
        {
            _minute = minute;
        }
        else
        {
            throw new ArgumentException($"Minute Invalid (0 to 59): {minute}");
        }

        if (second is >= 0 and <= 59)
        {
            _second = second;
        }
        else
        {
            throw new ArgumentException($"Second Invalid (0 to 59): {second}");
        }

        return new TimeParameters(
            Year: _year,
            Month: _month,
            Day: _day,
            Hour: _hour,
            Minute: _minute,
            Second: _second);
    }
}

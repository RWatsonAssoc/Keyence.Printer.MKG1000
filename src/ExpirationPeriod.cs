// <copyright file="ExpirationPeriod.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create ExpirationPeriodParameters</summary>

namespace Keyence.Printer.MKG1000;

public readonly record struct ExpirationPeriodParameters(
    int ProgramNumber,
    int ExpirationPeriodNumber,
    int Year,
    int Month,
    int Day,
    int Hour,
    int Minute)
{
    public string ParameterString =>
        $",{ProgramNumber},{ExpirationPeriodNumber},{Year},{Month},{Day},{Hour},{Minute}";
}

public static class ExpirationPeriod
{
    public static ExpirationPeriodParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new ExpirationPeriodParameters(
            ProgramNumber: int.Parse(split[1]),
            ExpirationPeriodNumber: int.Parse(split[2]),
            Year: int.Parse(split[3]),
            Month: int.Parse(split[4]),
            Day: int.Parse(split[5]),
            Hour: int.Parse(split[6]),
            Minute: int.Parse(split[7]));
    }

    public static ExpirationPeriodParameters CreateParameters(
        int programNumber,
        int expirationPeriodNumber,
        int year,
        int month,
        int day,
        int hour,
        int minute)
    {
        int _programNumber;
        int _expirationPeriodNumber;
        int _year;
        int _month;
        int _day;
        int _hour;
        int _minute;

        if (programNumber is >= 0 and <= 500)
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0 to 500): {programNumber}");
        }

        if (expirationPeriodNumber is >= 0 and <= 50)
        {
            _expirationPeriodNumber = expirationPeriodNumber;
        }
        else
        {
            throw new ArgumentException($"Expiration Period Number is Invalid (0 to 50): {expirationPeriodNumber}");
        }

        if (year is >= -99 and <= 99)
        {
            _year = year;
        }
        else
        {
            throw new ArgumentException($"Year is Invalid (-99 to +99): {year}");
        }

        if (month is >= -99 and <= 99)
        {
            _month = month;
        }
        else
        {
            throw new ArgumentException($"Month is Invalid (-99 to +99): {month}");
        }

        if (day is >= -1999 and <= 1999)
        {
            _day = day;
        }
        else
        {
            throw new ArgumentException($"Day is Invalid (-1999 to +1999): {day}");
        }

        if (hour is >= -99 and <= 99)
        {
            _hour = hour;
        }
        else
        {
            throw new ArgumentException($"Hour is Invalid (-99 to +99): {hour}");
        }

        if (minute is >= -99 and <= 99)
        {
            _minute = minute;
        }
        else
        {
            throw new ArgumentException($"Minute is Invalid (-99 to +99): {minute}");
        }

        return new ExpirationPeriodParameters(
            ProgramNumber: _programNumber,
            ExpirationPeriodNumber: _expirationPeriodNumber,
            Year: _year,
            Month: _month,
            Day: _day,
            Hour: _hour,
            Minute: _minute);
    }
}

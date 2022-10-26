// <copyright file="ShiftTime.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create ShiftTimeParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum ShiftCondition
{
    Disabled = 0,
    EnabledOnlyOnce = 1,
    AlwaysEnabled = 2
}

public readonly record struct ShiftTimeParameters(
    ShiftCondition ShiftCondition,
    int ShiftHour,
    int ShiftMinute)
{
    public string ParameterString =>
        $",{(int)ShiftCondition},{ShiftHour},{ShiftMinute}";
}

public static class ShiftTime
{
    public static ShiftTimeParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new ShiftTimeParameters(
            ShiftCondition: (ShiftCondition)Enum.Parse(typeof(ShiftCondition), split[1]),
            ShiftHour: int.Parse(split[2]),
            ShiftMinute: int.Parse(split[3]));
    }

    public static ShiftTimeParameters CreateParameters(
        ShiftCondition shiftCondition,
        int shiftHour,
        int shiftMinute)
    {
        int _shiftHour;
        int _shiftMinute;

        if (shiftHour is >= 18 and <= 32)
        {
            _shiftHour = shiftHour;
        }
        else
        {
            throw new ArgumentException($"Shift Hour Invalid (18 to 32): {shiftHour}");
        }

        if (shiftMinute is >= 0 and <= 59)
        {
            _shiftMinute = shiftMinute;
        }
        else
        {
            throw new ArgumentException(
                $"Shift Minute Invalid (0 to 59): {shiftMinute}");
        }

        return new ShiftTimeParameters(
            ShiftCondition: shiftCondition,
            ShiftHour: _shiftHour,
            ShiftMinute: _shiftMinute);
    }
}

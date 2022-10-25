// <copyright file="CharacterCode.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum for character code values</summary>

namespace Keyence.Printer.MKG1000;

public enum CharacterCode
{
    Ascii = 0,
    Shiftjis = 1,
    Latin9 = 2,
    Gb2312 = 3,
    Utf8 = 5,
    RequestAtTimeOfSetting = 9
}

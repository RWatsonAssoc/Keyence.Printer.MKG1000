﻿// <copyright file="ErrorResponse.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Record struct definition for ErrorResponse</summary>

namespace Keyence.Printer.MKG1000;

public readonly record struct ErrorResponse(
    int ErrorCode,
    string ErrorName,
    string ErrorDescription,
    string Countermeasures)
{
    public string ErrorString =>
        $"{ErrorCode}, {ErrorName}, {ErrorDescription}, {Countermeasures}";
}

// <copyright file="Rs232CConnection.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Values used when using an RS-232C connection to the printer</summary>

namespace Keyence.Printer.MKG1000;

public class Rs232CConnection : Connection
{
    public readonly Rs232CBaudRate BaudRate;
    public const int BitLength = 8; // Fixed
    public readonly Rs232CParityCheck ParityCheck;
    public readonly Rs232CStopBit StopBit;
    
    public Rs232CConnection(
        Rs232CBaudRate baudRate = Rs232CBaudRate.Bps38400,
        Rs232CParityCheck parityCheck = Rs232CParityCheck.None,
        Rs232CStopBit stopBit = Rs232CStopBit.OneBit,
        Delimiter delimiter = Delimiter.CR,
        Checksum checksum = Checksum.NotPresent,
        CommunicationBuffer buffer = CommunicationBuffer.Off)
        : base(delimiter, checksum, buffer)
    {
        BaudRate = baudRate;
        ParityCheck = parityCheck;
        StopBit = stopBit;
    }
}

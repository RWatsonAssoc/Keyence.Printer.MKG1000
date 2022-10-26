// <copyright file="Connection.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Values used when using a connection to the printer</summary>

namespace Keyence.Printer.MKG1000;

public class Connection
{
    public readonly Delimiter Delimiter;
    public readonly Checksum Checksum;
    public readonly CommunicationBuffer Buffer;

    public Connection(
        Delimiter delimiter = Delimiter.CR,
        Checksum checksum = Checksum.NotPresent,
        CommunicationBuffer buffer = CommunicationBuffer.Off)
    {
        Delimiter = delimiter;
        Checksum = checksum;
        Buffer = buffer;
    }
}

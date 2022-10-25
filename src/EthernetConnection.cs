// <copyright file="EthernetConnection.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Values used when using an ethernet connection to the printer</summary>

namespace Keyence.Printer.MKG1000;

public class EthernetConnection : Connection
{
    public readonly System.Net.IPAddress IpAddress;
    public readonly int Port;

    public EthernetConnection(
        string ipString,
        int port = 9004,
        Delimiter delimiter = Delimiter.CR,
        Checksum checksum = Checksum.NotPresent,
        CommunicationBuffer buffer = CommunicationBuffer.Off)
        : base(delimiter, checksum, buffer)
    {
        IpAddress = System.Net.IPAddress.Parse(ipString);
        Port = port;
    }

    public EthernetConnection(
        System.Net.IPAddress ipAddress,
        int port = 9004,
        Delimiter delimiter = Delimiter.CR,
        Checksum checksum = Checksum.NotPresent,
        CommunicationBuffer buffer = CommunicationBuffer.Off)
        : base(delimiter, checksum, buffer)
    {
        IpAddress = ipAddress;
        Port = port;
    }
}

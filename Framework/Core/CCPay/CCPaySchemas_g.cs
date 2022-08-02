/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFS4IoT.CCPay
{

    [DataContract]
    public sealed class StatusClass
    {
        public StatusClass(TerminalEnum? Terminal = null, string TerminalErrorDescription = "")
        {
            this.Terminal = Terminal;
            this.TerminalErrorDescription = TerminalErrorDescription;
        }

        public enum TerminalEnum
        {
            Busy,
            Idle,
            Inoperative,
            Unknown
        }

        [DataMember(Name = "terminal")]
        public TerminalEnum? Terminal { get; init; }

        [DataMember(Name = "terminalErrorDescription")]
        public string TerminalErrorDescription { get; init; }
    }


    [DataContract]
    public sealed class CapabilitiesClass
    {
        public CapabilitiesClass(bool? NFCSupported = null, bool? ChipSupported = null, bool? MagSupported = null)
        {
            this.NFCSupported = NFCSupported;
            this.ChipSupported = ChipSupported;
            this.MagSupported = MagSupported;
        }

        [DataMember(Name = "nfcSupported")]
        public bool? NFCSupported { get; init; }

        [DataMember(Name = "chipSupported")]
        public bool? ChipSupported { get; init; }

        [DataMember(Name = "magSupported")]
        public bool? MagSupported { get; init; }
    }
}
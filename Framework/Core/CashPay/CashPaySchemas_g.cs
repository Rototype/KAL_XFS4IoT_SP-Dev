/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFS4IoT.CashPay
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
        public CapabilitiesClass(bool? NoteAcceptingSupported = null, bool? NoteDispensingSupported = null, bool? CoinDispensingSupported = null)
        {
            this.NoteAcceptingSupported = NoteAcceptingSupported;
            this.NoteDispensingSupported = NoteDispensingSupported;
            this.CoinDispensingSupported = CoinDispensingSupported;
        }

        [DataMember(Name = "noteAcceptingSupported")]
        public bool? NoteAcceptingSupported { get; init; }

        [DataMember(Name = "noteDispensingSupported")]
        public bool? NoteDispensingSupported { get; init; }

        [DataMember(Name = "coinDispensingSupported")]
        public bool? CoinDispensingSupported { get; init; }
    }
}
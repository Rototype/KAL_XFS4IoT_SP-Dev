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
        public StatusClass(Availability AcceptCash = Availability.Unknown,
            Availability Change = Availability.Unknown,
            FillLevel NoteRecycler = FillLevel.Unknown,
            FillLevel NoteBin = FillLevel.Unknown,
            FillLevel CoinHopper1 = FillLevel.Unknown,
            FillLevel CoinHopper2 = FillLevel.Unknown,
            FillLevel CoinHopper3 = FillLevel.Unknown,
            FillLevel CoinHopper4 = FillLevel.Unknown,
            CounterStatusClass[] NoteBinCounters = null,
            CounterStatusClass[] NoteRecyclerCounters = null,
            CounterStatusClass[] CoinHopperCounters = null
            )
        {
            this.AcceptCash = AcceptCash;
            this.Change = Change;
            this.NoteRecycler = NoteRecycler;
            this.NoteBin = NoteBin;
            this.CoinHopper1 = CoinHopper1;
            this.CoinHopper2 = CoinHopper2;
            this.CoinHopper3 = CoinHopper3;
            this.CoinHopper4 = CoinHopper4;
            this.NoteBinCounters = NoteBinCounters;
            this.NoteRecyclerCounters = NoteRecyclerCounters;
            this.CoinHopperCounters = CoinHopperCounters;
        }

        public class CounterStatusClass
        {
            public string Currency;
            public decimal Amount;
            public int Count;
        }

        public enum Availability
        {
            Unknown,
            Available,
            Unavailable,
        }

        public enum FillLevel
        {
            Unknown,
            Normal,
            Empty,
            Full,
            Low
        }

        [DataMember(Name = "acceptCash")]
        public Availability AcceptCash { get; init; }
        [DataMember(Name = "Change")]
        public Availability Change { get; init; }

        [DataMember(Name = "noteRecycler")]
        public FillLevel NoteRecycler { get; init; }
        [DataMember(Name = "noteBin")]
        public FillLevel NoteBin { get; init; }
        [DataMember(Name = "coinHopper1")]
        public FillLevel CoinHopper1 { get; init; }
        [DataMember(Name = "coinHopper2")]
        public FillLevel CoinHopper2 { get; init; }
        [DataMember(Name = "coinHopper3")]
        public FillLevel CoinHopper3 { get; init; }
        [DataMember(Name = "coinHopper4")]
        public FillLevel CoinHopper4 { get; init; }


        [DataMember(Name = "notBinCounters")]
        public CounterStatusClass[] NoteBinCounters { get; init; }
        [DataMember(Name = "noteRecyclerCounters")]
        public CounterStatusClass[] NoteRecyclerCounters { get; init; }
        [DataMember(Name = "coinHopperCounters")]
        public CounterStatusClass[] CoinHopperCounters { get; init; }
        
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
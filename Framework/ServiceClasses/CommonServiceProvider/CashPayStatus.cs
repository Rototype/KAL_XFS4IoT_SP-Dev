/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Common
{
    public sealed class CashPayStatusClass
    {
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

        public CashPayStatusClass(Availability AcceptCash = Availability.Unknown,
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

        public Availability AcceptCash { get; set; }
        public Availability Change { get; set; }

        public FillLevel NoteRecycler { get; set; }
        public FillLevel NoteBin { get; set; }
        public FillLevel CoinHopper1 { get; set; }
        public FillLevel CoinHopper2 { get; set; }
        public FillLevel CoinHopper3 { get; set; }
        public FillLevel CoinHopper4 { get; set; }


        public CounterStatusClass[] NoteBinCounters { get; set; }
        public CounterStatusClass[] NoteRecyclerCounters { get; set; }
        public CounterStatusClass[] CoinHopperCounters { get; set; }
    }
}

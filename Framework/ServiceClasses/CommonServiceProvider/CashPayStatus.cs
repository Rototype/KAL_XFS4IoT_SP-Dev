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
            public string Currency { get; set; }
            public decimal Amount { get; set; }
            public int Count { get; set; }

            public CounterStatusClass(string currency, decimal amount, int count)
            {
                Currency = currency;
                Amount = amount;
                Count = count;
            }
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
            List<CounterStatusClass> NoteBinCounters = null,
            List<CounterStatusClass> NoteRecyclerCounters = null,
            List<CounterStatusClass> CoinHopperCounters = null
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
            if (NoteBinCounters != null) this.NoteBinCounters = NoteBinCounters;
            else this.NoteBinCounters = new List<CounterStatusClass>();
            if (NoteRecyclerCounters != null) this.NoteRecyclerCounters = NoteRecyclerCounters;
            else this.NoteRecyclerCounters = new List<CounterStatusClass>();
            if (CoinHopperCounters != null) this.CoinHopperCounters = CoinHopperCounters;
            else this.CoinHopperCounters= new List<CounterStatusClass>();   
        }

        public Availability AcceptCash { get; set; }
        public Availability Change { get; set; }

        public FillLevel NoteRecycler { get; set; }
        public FillLevel NoteBin { get; set; }
        public FillLevel CoinHopper1 { get; set; }
        public FillLevel CoinHopper2 { get; set; }
        public FillLevel CoinHopper3 { get; set; }
        public FillLevel CoinHopper4 { get; set; }


        public List<CounterStatusClass> NoteBinCounters { get; set; }
        public List<CounterStatusClass> NoteRecyclerCounters { get; set; }
        public List<CounterStatusClass> CoinHopperCounters { get; set; }
    }
}

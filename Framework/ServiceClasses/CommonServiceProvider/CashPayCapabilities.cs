/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Common
{
    public sealed class CashPayCapabilitiesClass
    {

        public CashPayCapabilitiesClass(bool NFCSupported, bool ChipSupported, bool MagSupported)
        {
            this.NFCSupported = NFCSupported;
            this.ChipSupported = ChipSupported;
            this.MagSupported = MagSupported;
        }

        public bool NFCSupported { get; init; }
        public bool ChipSupported { get; init; }
        public bool MagSupported { get; init; }
    }
}



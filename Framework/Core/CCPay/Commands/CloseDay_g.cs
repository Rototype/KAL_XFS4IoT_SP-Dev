/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CCPay.Commands
{
    [DataContract]
    [Command(Name = "CCPay.CloseDay")]
    public sealed class CloseDayCommand : Command<CloseDayCommand.PayloadData>
    {
        public CloseDayCommand(int RequestId, CloseDayCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout)
                : base(Timeout)
            {
            }
        }
    }
}

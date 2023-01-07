/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.CashPay
{
    internal class LoadCashEvents : CashPayEvents, ILoadCashEvents
    {

        public LoadCashEvents(IConnection connection, int requestId)
            : base(connection, requestId)
        { }
 
        public async Task UserInteractionUpdateEvent(XFS4IoT.CashPay.Events.UserInteractionUpdateEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.CashPay.Events.UserInteractionUpdateEvent(requestId, Payload));

    }
}

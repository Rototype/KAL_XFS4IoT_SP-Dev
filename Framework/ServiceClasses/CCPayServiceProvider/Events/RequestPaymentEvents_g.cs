/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.

\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;
using XFS4IoT.CCPay.Events;

namespace XFS4IoTFramework.CCPay
{
    internal class RequestPaymentEvents : CCPayEvents, IRequestPaymentEvents
    {

        public RequestPaymentEvents(IConnection connection, int requestId)
            : base(connection, requestId)
        { }
        public async Task UserInteractionUpdateEvent(XFS4IoT.CCPay.Events.UserInteractionUpdateEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.CCPay.Events.UserInteractionUpdateEvent(requestId, Payload));

    }
}

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
    public class UserInteractionUpdateCommandEvent
    {
        public UserInteractionUpdateCommandEvent(IRequestPaymentEvents MediaEvent)
        {
            RequestPayment = MediaEvent;
        }
        public UserInteractionUpdateCommandEvent(IReverseLastPaymentEvents MediaEvent)
        {
            ReverseLastPayment = MediaEvent;
        }
        public UserInteractionUpdateCommandEvent(ICloseDayEvents MediaEvent)
        {
            CloseDay = MediaEvent;
        }

        public async Task UserInteractionUpdateEvent(UserInteractionUpdateEvent.PayloadData Payload)
        {
            Contracts.Assert(RequestPayment is not null || ReverseLastPayment is not null || CloseDay is not null, $"Not control media event interface set.");

            if (RequestPayment is not null)
                await RequestPayment.UserInteractionUpdateEvent(Payload);
            if (ReverseLastPayment is not null)
                await ReverseLastPayment.UserInteractionUpdateEvent(Payload);
            if (CloseDay is not null)
                await CloseDay.UserInteractionUpdateEvent(Payload);
        }

        private IRequestPaymentEvents RequestPayment { get; init; } = null;
        private IReverseLastPaymentEvents ReverseLastPayment { get; init; } = null;
        private ICloseDayEvents CloseDay { get; init; } = null;
    }
}


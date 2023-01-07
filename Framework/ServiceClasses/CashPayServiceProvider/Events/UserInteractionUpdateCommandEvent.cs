/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;
using XFS4IoT.CashPay.Events;

namespace XFS4IoTFramework.CashPay
{
    public class UserInteractionUpdateCommandEvent
    {
        public UserInteractionUpdateCommandEvent(IRequestPaymentEvents MediaEvent)
        {
            RequestPayment = MediaEvent;
        }
        public UserInteractionUpdateCommandEvent(IEmptyCashEvents MediaEvent)
        {
            EmptyCash = MediaEvent;
        }
        public UserInteractionUpdateCommandEvent(ILoadCashEvents MediaEvent)
        {
            LoadCash = MediaEvent;
        }

        public async Task UserInteractionUpdateEvent(UserInteractionUpdateEvent.PayloadData Payload)
        {
            Contracts.Assert(RequestPayment is not null || LoadCash is not null || EmptyCash is not null, $"Not control media event interface set.");

            if (RequestPayment is not null)
                await RequestPayment.UserInteractionUpdateEvent(Payload);
            if (LoadCash is not null)
                await LoadCash.UserInteractionUpdateEvent(Payload);
            if (EmptyCash is not null)
                await EmptyCash.UserInteractionUpdateEvent(Payload);
        }

        private IRequestPaymentEvents RequestPayment { get; init; } = null;
        private ILoadCashEvents LoadCash { get; init; } = null;
        private IEmptyCashEvents EmptyCash { get; init; } = null;
    }
}


/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System.Threading;
using System.Threading.Tasks;
using XFS4IoTFramework.Common;
using XFS4IoTServer;

namespace XFS4IoTFramework.CCPay
{
    public interface ICCPayDevice : IDevice
    {
    
        Task<CloseDayResult> CloseDay(UserInteractionUpdateCommandEvent ce, CloseDayRequest request,
                              CancellationToken cancellation);
                              
        Task<RequestPaymentResult> RequestPayment(UserInteractionUpdateCommandEvent ce, RequestPaymentRequest request,
                              CancellationToken cancellation);

        Task<ReverseLastPaymentResult> ReverseLastPayment(UserInteractionUpdateCommandEvent ce, ReverseLastPaymentRequest request,
                              CancellationToken cancellation);

        Task<ResetResult> ResetDevice(CancellationToken cancellation);

        CCPayStatusClass CCPayStatus { get; set; }

        CCPayCapabilitiesClass CCPayCapabilities { get; set; }

    }
}

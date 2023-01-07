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

namespace XFS4IoTFramework.CashPay
{
    public interface ICashPayDevice : IDevice
    {
    
        Task<EmptyCashResult> EmptyCash(UserInteractionUpdateCommandEvent ce, EmptyCashRequest request,
                              CancellationToken cancellation);
                              
        Task<RequestPaymentResult> RequestPayment(UserInteractionUpdateCommandEvent ce, RequestPaymentRequest request,
                              CancellationToken cancellation);

        Task<LoadCashResult> LoadCash(UserInteractionUpdateCommandEvent ce, LoadCashRequest request,
                              CancellationToken cancellation);

        Task<ResetResult> ResetDevice(CancellationToken cancellation);

        CashPayStatusClass CashPayStatus { get; set; }

        CashPayCapabilitiesClass CashPayCapabilities { get; set; }

    }
}

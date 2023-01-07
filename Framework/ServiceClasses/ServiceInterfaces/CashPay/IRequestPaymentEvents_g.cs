/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/


using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.CashPay
{
    public interface IRequestPaymentEvents
    {
        Task UserInteractionUpdateEvent(XFS4IoT.CashPay.Events.UserInteractionUpdateEvent.PayloadData Payload);
    }
}

/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 \***********************************************************************************************/


using XFS4IoTServer;
using System.Threading.Tasks;
using XFS4IoT.CCPay.Events;

namespace XFS4IoTFramework.CCPay
{
    public interface IReverseLastPaymentEvents
    {
        Task UserInteractionUpdateEvent(XFS4IoT.CCPay.Events.UserInteractionUpdateEvent.PayloadData Payload);
    }
}

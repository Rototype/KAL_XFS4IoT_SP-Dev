/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 * 
\***********************************************************************************************/

using System.Threading.Tasks;

using XFS4IoTFramework.CashPay;

namespace XFS4IoTServer
{
    public interface ICashPayService
    {
    }

    public interface ICashPayServiceClass : ICashPayUnsolicitedEvents
    {
    }
}

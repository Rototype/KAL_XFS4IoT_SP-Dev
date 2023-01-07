/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;

namespace XFS4IoTFramework.CashPay
{
    internal abstract class CashPayEvents
    {
        protected readonly IConnection connection;
        protected readonly int requestId;

        public CashPayEvents(IConnection connection, int requestId)
        {
            this.connection = connection;
            this.requestId = requestId;
        }

    }
}

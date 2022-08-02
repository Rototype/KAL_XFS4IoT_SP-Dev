/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.CCPay
{
    internal class ResetEvents : CCPayEvents, IResetEvents
    {

        public ResetEvents(IConnection connection, int requestId)
            : base(connection, requestId)
        { }

    }
}

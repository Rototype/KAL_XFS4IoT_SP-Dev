/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.

\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;

using XFS4IoT;
using XFS4IoTFramework.CCPay;

namespace XFS4IoTServer
{
    public partial class CCPayServiceClass : ICCPayServiceClass
    {
        private IServiceProvider ServiceProvider { get; init; }
        private ILogger Logger { get; init; }
        private ICCPayDevice Device { get => ServiceProvider.Device.IsA<ICCPayDevice>(); }
    }
}

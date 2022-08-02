/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFS4IoT;
using XFS4IoTFramework.CCPay;
using XFS4IoTFramework.Common;

namespace XFS4IoTServer
{

    public partial class CCPayServiceClass
    {

        public CCPayServiceClass(IServiceProvider ServiceProvider, ILogger logger)
        {
            this.ServiceProvider = ServiceProvider.IsNotNull();
            Logger = logger;
            this.ServiceProvider.Device.IsNotNull($"Invalid parameter received in the {nameof(CCPayServiceClass)} constructor. {nameof(ServiceProvider.Device)}").IsA<ICCPayDevice>();

            CommonService = ServiceProvider.IsA<ICommonService>($"Invalid interface parameter specified for common service. {nameof(CCPayServiceClass)}");
            
            GetCapabilities();
            GetStatus();
        }

        /// <summary>
        /// Common service interface
        /// </summary>
        public ICommonService CommonService { get; init; }

        private void GetCapabilities()
        {
            Logger.Log(Constants.DeviceClass, "CCPayDev.CCPayCapabilities");
            CommonService.CCPayCapabilities = Device.CCPayCapabilities;
            Logger.Log(Constants.DeviceClass, "CCPayDev.CCPayCapabilities=");

            CommonService.CCPayCapabilities.IsNotNull($"The device class set CCPayCapabilities property to null. The device class must report device capabilities.");
        }

        private void GetStatus()
        {
            Logger.Log(Constants.DeviceClass, "CCPayDev.CCPayStatus");
            CommonService.CCPayStatus = Device.CCPayStatus;
            Logger.Log(Constants.DeviceClass, "CCPayDev.CCPayStatus=");

            CommonService.CCPayStatus.IsNotNull($"The device class set CCPayStatus property to null. The device class must report device status.");
        }
    }
}

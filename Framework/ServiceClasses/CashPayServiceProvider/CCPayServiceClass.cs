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
using XFS4IoTFramework.CashPay;
using XFS4IoTFramework.Common;

namespace XFS4IoTServer
{

    public partial class CashPayServiceClass
    {

        public CashPayServiceClass(IServiceProvider ServiceProvider, ILogger logger)
        {
            this.ServiceProvider = ServiceProvider.IsNotNull();
            Logger = logger;
            this.ServiceProvider.Device.IsNotNull($"Invalid parameter received in the {nameof(CashPayServiceClass)} constructor. {nameof(ServiceProvider.Device)}").IsA<ICashPayDevice>();

            CommonService = ServiceProvider.IsA<ICommonService>($"Invalid interface parameter specified for common service. {nameof(CashPayServiceClass)}");
            
            GetCapabilities();
            GetStatus();
        }

        /// <summary>
        /// Common service interface
        /// </summary>
        public ICommonService CommonService { get; init; }

        private void GetCapabilities()
        {
            Logger.Log(Constants.DeviceClass, "CashPayDev.CashPayCapabilities");
            CommonService.CashPayCapabilities = Device.CashPayCapabilities;
            Logger.Log(Constants.DeviceClass, "CashPayDev.CashPayCapabilities=");

            CommonService.CashPayCapabilities.IsNotNull($"The device class set CashPayCapabilities property to null. The device class must report device capabilities.");
        }

        private void GetStatus()
        {
            Logger.Log(Constants.DeviceClass, "CashPayDev.CashPayStatus");
            CommonService.CashPayStatus = Device.CashPayStatus;
            Logger.Log(Constants.DeviceClass, "CashPayDev.CashPayStatus=");

            CommonService.CashPayStatus.IsNotNull($"The device class set CashPayStatus property to null. The device class must report device status.");
        }
    }
}

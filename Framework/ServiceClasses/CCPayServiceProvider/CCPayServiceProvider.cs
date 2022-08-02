/***********************************************************************************************\
 * (C) Rototype, 2022
 * CCPay licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XFS4IoT;
using XFS4IoTFramework.Common;

namespace XFS4IoTServer
{
    /// <summary>
    /// Default implimentation of a Credit Card pay service provider. 
    /// </summary>

    public class CCPayServiceProvider : ServiceProvider, ICCPayService, ICommonService
    {
        public CCPayServiceProvider(EndpointDetails endpointDetails, string ServiceName, IDevice device, ILogger logger)
            :
            base(endpointDetails,
                 ServiceName,
                 new[] { XFSConstants.ServiceClass.Common, XFSConstants.ServiceClass.CCPay },
                 device,
                 logger)
        {
            CommonService = new CommonServiceClass(this, logger, ServiceName);
            CCPay = new CCPayServiceClass(this, logger);
        }

        private readonly CCPayServiceClass CCPay;
        private readonly CommonServiceClass CommonService;

        #region Common unsolicited events
        public Task StatusChangedEvent(CommonStatusClass.DeviceEnum? Device,
                                       CommonStatusClass.PositionStatusEnum? Position,
                                       int? PowerSaveRecoveryTime,
                                       CommonStatusClass.AntiFraudModuleEnum? AntiFraudModule,
                                       CommonStatusClass.ExchangeEnum? Exchange,
                                       CommonStatusClass.EndToEndSecurityEnum? EndToEndSecurity) => CommonService.StatusChangedEvent(Device,
                                                                                                                                     Position,
                                                                                                                                     PowerSaveRecoveryTime,
                                                                                                                                     AntiFraudModule,
                                                                                                                                     Exchange,
                                                                                                                                     EndToEndSecurity);


        public Task NonceClearedEvent(string ReasonDescription) => throw new NotImplementedException("NonceClearedEvent is not supported in the CCPay Service.");

        public Task ErrorEvent(CommonStatusClass.ErrorEventIdEnum EventId,
                               CommonStatusClass.ErrorActionEnum Action,
                               string VendorDescription) => CommonService.ErrorEvent(EventId, Action, VendorDescription);

        #endregion

        #region Common Service

        /// <summary>
        /// Stores Common interface capabilites internally
        /// </summary>
        public CommonCapabilitiesClass CommonCapabilities { get => CommonService.CommonCapabilities; set => CommonService.CommonCapabilities = value; }

        /// <summary>
        /// Common Status
        /// </summary>
        public CommonStatusClass CommonStatus { get => CommonService.CommonStatus; set => CommonService.CommonStatus = value; }

        /// <summary>
        /// Stores CCPay interface capabilites internally
        /// </summary>
        public CCPayCapabilitiesClass CCPayCapabilities { get => CommonService.CCPayCapabilities; set => CommonService.CCPayCapabilities = value; }

        /// <summary>
        /// CCPay Status
        /// </summary>
        public CCPayStatusClass CCPayStatus { get => CommonService.CCPayStatus; set => CommonService.CCPayStatus = value; }


        #endregion
    }
}

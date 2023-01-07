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

    public class CashPayServiceProvider : ServiceProvider, ICashPayService, ICommonService
    {
        public CashPayServiceProvider(EndpointDetails endpointDetails, string ServiceName, IDevice device, ILogger logger)
            :
            base(endpointDetails,
                 ServiceName,
                 new[] { XFSConstants.ServiceClass.Common, XFSConstants.ServiceClass.CashPay },
                 device,
                 logger)
        {
            CommonService = new CommonServiceClass(this, logger, ServiceName);
            CashPay = new CashPayServiceClass(this, logger);
        }

        private readonly CashPayServiceClass CashPay;
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
        /// Stores CashPay interface capabilites internally
        /// </summary>
        public CashPayCapabilitiesClass CashPayCapabilities { get => CommonService.CashPayCapabilities; set => CommonService.CashPayCapabilities = value; }

        /// <summary>
        /// CashPay Status
        /// </summary>
        public CashPayStatusClass CashPayStatus { get => CommonService.CashPayStatus; set => CommonService.CashPayStatus = value; }


        #endregion
    }
}

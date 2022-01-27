﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using XFS4IoTFramework.CashManagement;
using XFS4IoT.CashManagement.Events;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.Storage;

namespace XFS4IoTFramework.CashManagement
{
    public interface ICashManagementService
    {

        Task ShutterStatusChangedEvent(CashManagementCapabilitiesClass.PositionEnum Position, CashManagementStatusClass.ShutterEnum Status);

        Task ItemsTakenEvent(CashDispenserCapabilitiesClass.OutputPositionEnum Position, string AdditionalBunches);

        Task ItemsInsertedEvent(CashDispenserCapabilitiesClass.OutputPositionEnum Postion);

        Task ItemsPresentedEvent(CashDispenserCapabilitiesClass.OutputPositionEnum Position, string AdditionalBunches);
    }

    public interface ICashManagementServiceClass : ICashManagementService, ICashManagementUnsolicitedEvents
    { 
    }
}
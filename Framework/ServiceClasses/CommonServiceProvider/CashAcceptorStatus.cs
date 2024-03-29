﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Common
{
    public sealed class CashAcceptorStatusClass
    {
        public enum StackerItemsEnum
        {
            CustomerAccess,
            NoCustomerAccess,
            AccessUnknown,
            NoItems,
        }

        public enum BanknoteReaderEnum
        {
            Ok,
            Inoperable,
            Unknown,
            NotSupported
        }

        public enum IntermediateStackerEnum
        {
            Empty,
            NotEmpty,
            Full,
            Unknown,
            NotSupported,
        }

        public CashAcceptorStatusClass(IntermediateStackerEnum IntermediateStacker,
                                       StackerItemsEnum StackerItems,
                                       BanknoteReaderEnum BanknoteReader,
                                       bool DropBox,
                                       Dictionary<CashManagementCapabilitiesClass.PositionEnum, CashManagementStatusClass.PositionStatusClass> Positions)
        {
            this.IntermediateStacker = IntermediateStacker;
            this.StackerItems = StackerItems;
            this.BanknoteReader = BanknoteReader;
            this.DropBox = DropBox;
            this.Positions = Positions;
        }

        /// <summary>
        /// Supplies the state of the intermediate stacker. The following values are possible:
        /// 
        /// * ```Empty``` - The intermediate stacker is empty.
        /// * ```NotEmpty``` - The intermediate stacker is not empty.
        /// * ```Full``` - The intermediate stacker is full. This may also be reported during a cash-in transaction
        /// where a limit specified by CashAcceptor.CashInStart has been reached.
        /// * ```Unknown``` - Due to a hardware error or other condition, the state of the intermediate stacker
        /// cannot be determined.
        /// * ```NotSupported``` - The physical device has no intermediate stacker.
        /// </summary>
        public IntermediateStackerEnum IntermediateStacker { get; set; }

        /// <summary>
        /// This field informs the application whether items on the intermediate stacker have been in customer access. 
        /// The following values are possible:
        /// 
        /// * ```CustomerAccess``` - Items on the intermediate stacker have been in customer access. If the device is a 
        /// cash recycler then the items on the intermediate stacker may be there as a result of a previous 
        /// cash-out operation.
        /// * ```NoCustomerAccess``` - Items on the intermediate stacker have not been in customer access.
        /// * ```AccessUnknown``` - It is not known if the items on the intermediate stacker have been in customer access.
        /// * ```NoItems``` - There are no items on the intermediate stacker or the physical device has no intermediate 
        /// stacker.
        /// </summary>
        public StackerItemsEnum StackerItems { get; set; }

        /// <summary>
        /// Supplies the state of the banknote reader. The following values are possible:
        /// 
        /// * ```Ok``` - The banknote reader is in a good state.
        /// * ```Inoperable``` - The banknote reader is inoperable.
        /// * ```Unknown``` - Due to a hardware error or other condition, the state of the banknote reader cannot be
        /// determined.
        /// * ```NotSupported``` - The physical device has no banknote reader.
        /// </summary>
        public BanknoteReaderEnum BanknoteReader { get; set; }

        /// <summary>
        /// The drop box is an area within the Cash Acceptor where items which have caused a problem during an operation 
        /// are stored. This field specifies the status of the drop box. 
        /// If true, some items are stored in the drop box due to a cash-in transaction which caused a problem.
        /// If false, the drop box is empty or there is no drop box.
        /// </summary>
        public bool DropBox { get; set; }

        /// <summary>
        /// Array of structures for each position to which items can be dispensed or presented.
        /// </summary>
        public Dictionary<CashManagementCapabilitiesClass.PositionEnum, CashManagementStatusClass.PositionStatusClass> Positions { get; init; }
    }
}

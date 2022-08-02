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

namespace XFS4IoTFramework.Common
{
    public sealed class CCPayStatusClass
    {
        public enum TerminalEnum
        {
            Busy,
            Idle,
            Inoperative,
            Unknown
        }

        public CCPayStatusClass(TerminalEnum Terminal = TerminalEnum.Unknown, string TerminalErrorDescription = "")
        {
            this.Terminal = Terminal;
            this.TerminalErrorDescription = TerminalErrorDescription;
        }

        public TerminalEnum Terminal { get; set; }
        public string TerminalErrorDescription { get; set; }
    }
}

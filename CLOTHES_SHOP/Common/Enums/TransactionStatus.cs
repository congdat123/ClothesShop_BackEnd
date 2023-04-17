using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Common.Enums
{
    public enum TransactionStatus
    {
        /// <summary>
        /// PENDING.
        /// </summary>
        PENDING = 0,

        /// <summary>
        /// FAILED.
        /// </summary>
        FAILED = 1,

        /// <summary>
        /// SUCCESSFUL.
        /// </summary>
        SUCCESSFUL = 2,

        /// <summary>
        /// ABANDONED.
        /// </summary>
        ABANDONED = 3,
    }
}

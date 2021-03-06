﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    public enum TransformEnum
    {
        Default,
        IntegerDefaultToZero,
        IntegerRemoveDots,
        IntegerRemoveQuotions,
        ByteRemoveQuotions,
        IpAddress,
        DefaultZeroDouble,
        DefaultOpenDate,
        AntiNullAddress,
        NullabelDateTime
    }
}

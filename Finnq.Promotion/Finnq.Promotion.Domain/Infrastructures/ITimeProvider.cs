﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Infrastructures {
    public interface ITimeProvider {
        DateTime Now { get; }
    }
}

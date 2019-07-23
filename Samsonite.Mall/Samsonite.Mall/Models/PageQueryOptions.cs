﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samsonite.Mall.Models {
    public class PageQueryOptions {
        public PageQueryOptions() {
            Page = 1;
            PageSize = 20;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
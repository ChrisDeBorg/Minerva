﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.DataModels.Relations
{
    public class RelationMeta
    {
        public bool? Private { get; set; }
        public int? Importance { get; set; }
        public string? Note { get; set; }
        public Dictionary<string, object>? Tags { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Models.Configuration
{
    public class BackupArchivingConfiguration
    {
        public string ArchiveFolder { get; set; }
        public int KeepNumberOfArchives { get; set; }
    }
}

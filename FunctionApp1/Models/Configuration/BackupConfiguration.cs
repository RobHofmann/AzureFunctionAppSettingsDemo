using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Models.Configuration
{
    public class BackupConfiguration
    {
        public string BackupTargetFolder { get; set; }
        public bool EmptyBackupTargetFolderBeforeBackingUp { get; set; }
        public bool CompressBackup { get; set; }
        public bool DeleteLooseFilesAfterCompressing { get; set; }
        public string CompressTimestampFormat { get; set; }
        public string[] ExcludeList { get; set; }
    }
}

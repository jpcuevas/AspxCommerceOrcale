using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
namespace SageFrame.Common.RegisterModule
{
    public class Common
    {
        public static string TemporaryFolder = "Install\\Temp";
        public static string ModuleFolder = "Modules";
        public static string Password = "";
        public static bool RemoveZipFile = true;

        public static string LargeImagePath = "PageImages";
        public static string MediumImagePath = "PageImages\\mediumthumbs";
        public static string SmallImagePath = "PageImages\\smallthumbs";

        public static string DLLTargetPath = "bin";

        public static string TemporaryTemplateFolder = "TemplateDownloads";
    }
}
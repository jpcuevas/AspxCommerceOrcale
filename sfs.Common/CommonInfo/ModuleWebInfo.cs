using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class ModuleWebInfo
{

    public int ModuleID { get; set; }
    public string ModuleName { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public string DownloadUrl { get; set; }

}
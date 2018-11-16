using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ManualsFiltering
{
    public class Module
    {

        public string Author { get; set; }
        public string Compatibility { get; set; }
        public string DefuserDifficulty { get; set; }
        public string Description { get; set; }
        public string ExpertDifficulty { get; set; }
        public string ModuleID { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Published { get; set; }
        public int TwitchPlaysScore { get; set; }
        public string TwitchPlaysSupport { get; set; }
        public string Type { get; set; }
        public string RuleSeedSupport { get; set; }
        public string TwitchPlaysSpecial { get; set; }
        public bool Favorited { get; set; }
        public int indexPosition { get; set; }
    }
}

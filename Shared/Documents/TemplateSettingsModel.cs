using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Documents
{
    public class TemplateSettingsModel
    {
        [JsonPropertyName("hasClient")]
        public bool HasClient { get; set; } = false;

        [JsonPropertyName("hasCompanies")]
        public bool HasCompanies { get; set; } = false;

        [JsonPropertyName("hasMeteo")]
        public bool HasMeteo { get; set; } = false;

        [JsonPropertyName("hasSiteData")]
        public bool HasSiteData { get; set; } = false;
    }
}

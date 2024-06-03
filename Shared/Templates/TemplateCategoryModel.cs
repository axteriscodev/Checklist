using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Shared.Defaults;

namespace Shared.Templates
{
    public class TemplateCategoryModel : CategoryModel
    {
        [JsonPropertyName("questions")]
        public List<TemplateQuestionModel> Questions { get; set; } = [];
    }
}

using ConstructionSiteLibrary.Model.DocumentWizard;
using Radzen.Blazor;
using Shared.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSiteLibrary.Components.Documents.Wizard
{
    public partial class WizardDocumentCreation
    {

        private TemplateModel? _selectedTemplate;

        private RadzenSteps? _stepsComponent;

        private void Back()
        {
            _stepsComponent?.PrevStep();
        }

        private void Forward(DocumentStepArgs args)
        {
            _stepsComponent?.NextStep();
        }


    }
}

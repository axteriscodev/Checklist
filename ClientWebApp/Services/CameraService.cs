using ConstructionSiteLibrary.Components.Choices;
using ConstructionSiteLibrary.Components.Utilities;
using ConstructionSiteLibrary.Interfaces;
using ClientWebApp.Components;
using Radzen;


namespace ClientWebApp.Services
{
    public class CameraService(DialogService dialogService) : ICameraService
    {

        private DialogService _dialogService = dialogService;

        public async Task<string> OpenCamera()
        {
           
            var img = await _dialogService.OpenAsync<CameraComponent>("");
            return img;
        }
    }
}

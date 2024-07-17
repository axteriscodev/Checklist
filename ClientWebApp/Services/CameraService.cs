using ConstructionSiteLibrary.Components.Choices;
using ConstructionSiteLibrary.Components.Utilities;
using ConstructionSiteLibrary.Interfaces;
using ClientWebApp.Components;
using Radzen;
using ConstructionSiteLibrary.Services;
using ConstructionSiteLibrary.Managers;
using Microsoft.JSInterop;


namespace ClientWebApp.Services
{
    public class CameraService(DialogService dialogService, IJSRuntime IJSRuntime) : ICameraService
    {

        private DialogService _dialogService = dialogService;

        private IJSRuntime _iJSRuntime = IJSRuntime;

        public async Task<string> OpenCamera()
        {
            var screenManager = new ScreenManager();
            await screenManager.Init(_iJSRuntime);
            var screenSize = screenManager.GetScreenSize();

            var scritta =  $"width : {screenSize!.Width}px; height: {screenSize!.Height}px;";

            var options = new DialogOptions
            {
                Style = $"width : {screenSize!.Width}px; height: {screenSize!.Height}px;",
            };
            var img = await _dialogService.OpenAsync<CameraComponent>("", options: options);
            return img;
        }
    }
}

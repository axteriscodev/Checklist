using ConstructionSiteLibrary.Model;

namespace ConstructionSiteLibrary.Pages
{
    public partial class Home
    {

        ScreenDimension? dim;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private void ScreenDimensionChanged(ScreenDimension? dimension)
        {
            dim = dimension;
            Console.WriteLine(dimension.ToString());
        }

        private void ScreenSizeChanged(ScreenSize? size)
        {
            Console.WriteLine("x: " + size.Width + " -  y: " + size.Height );
        }
    }
}

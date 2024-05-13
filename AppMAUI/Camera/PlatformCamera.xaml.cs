namespace AppMAUI.Camera;

public partial class PlatformCamera : ContentPage
{
	public PlatformCamera()
	{
		InitializeComponent();
	}

	private void CameraView_CamerasLoaded(object sender, EventArgs e)
	{
		cameraView.Camera = cameraView.Cameras.First();

		MainThread.BeginInvokeOnMainThread(async () => 
		{
			await cameraView.StartCameraAsync();
		});
	}
}
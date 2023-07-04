namespace Assets._Project._Scripts.CoreFeatures.CameraSystem
{
    public interface ICameraController
    {
        public void ResetController(CameraHandler cameraHandler);
        public void Execute();
    }
}
namespace Assets._Project._Scripts.Features.CameraSystem
{
    public interface ICameraController
    {
        public void ResetController(CameraHandler cameraHandler);
        public void Execute();
    }
}
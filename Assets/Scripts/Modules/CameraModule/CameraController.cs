using Modules.ActorModule.Concrete;
using Modules.CameraModule.Components;
using Modules.TickModule;

namespace Modules.CameraModule
{
    public class CameraController
    {
        public readonly CameraActor GameCamera;
        public readonly CameraActor UiCamera;
        
        private readonly ITickManager _tickManager;
        
        public CameraController(ITickManager tickManager, CameraActor gameCamera, CameraActor uiCamera)
        {
            _tickManager = tickManager;
            GameCamera = gameCamera;
            UiCamera = uiCamera;
        }

        public void StartBattle()
        {
            GameCamera.GetActorComponent<SmoothFollowerComponent>().Initialize(_tickManager);
        }
    }
}
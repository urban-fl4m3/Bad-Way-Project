using System.Collections.Generic;
using Modules.BattleModule;
using Modules.CameraModule;

namespace UI.Models
{
    public class BattleActorParameterModel: IModel
    {
        public readonly List<BattleActor> BattleActors;
        public readonly CameraController CameraController;

        public BattleActorParameterModel(List<BattleActor> battleActors, CameraController cameraController)
        {
            BattleActors = battleActors;
            CameraController = cameraController;
        }
    }
}
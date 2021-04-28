using System;
using System.Collections.Generic;
using Modules.BattleModule;
using Modules.CameraModule;
using UI.Models;

namespace UI.Interface
{
    public class BattleActorParameterModel: IModel
    {
        public List<BattleActor> BattleActors;
        public CameraController CameraController;

        public BattleActorParameterModel(List<BattleActor> battleActors, CameraController cameraController)
        {
            BattleActors = battleActors;
            CameraController = cameraController;
        }
    }
}
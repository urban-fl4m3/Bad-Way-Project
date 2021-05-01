using System.Collections.Generic;
using Modules.ActorModule.Components;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace UI.Views
{
    public class BattleActorParametersView : MonoBehaviour, IViewModel, ICanvasView, IActorParameter
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private ActorUIParameters _actorUIParametersPrefab;
        [SerializeField] private SortingInCollection _sortingInCollection;

        private readonly List<ActorUIParameters> _actorUIParameters = new List<ActorUIParameters>();

        private BattleActorParameterModel _model;

        public GameObject GameObject => gameObject;
        public Canvas Canvas { get; set; }


        public void ResolveModel(IModel model)
        {
            _model = (BattleActorParameterModel) model;
            _model.CameraController.OnCameraPositionChange += OnCameraMove;
            CreateActorParametersWindow();
        }

        public void Clear()
        {

        }

        public void CreateActorParametersWindow()
        {
            foreach (var actor in _model.BattleActors)
            {
                var stat = actor.Stats;
                var target = actor.Actor.TargetForUI;
                var actorName = actor.Actor.name;
                var health = stat.Health;
                var maxHealth = stat.MaxHealth;
                var isEnemy = false;
                if (actor.Actor.GetActorComponent<EnemyAI>())
                    isEnemy = true;
                
                var actorUIParameter = Instantiate(_actorUIParametersPrefab, _parent);
                actorUIParameter.Initialize(target, actorName, 1, health, maxHealth , Canvas, isEnemy);

                _actorUIParameters.Add(actorUIParameter);
            }

            _sortingInCollection.FetchCollection();
            _model.CameraController.OnCameraPositionChange += OnCameraMove;
        }

        private void OnCameraMove(Vector3 position)
        {
            foreach (var uiParameter in _actorUIParameters)
            {
                uiParameter.UpdatePosition();
            }

            _sortingInCollection.UpdateSorting();
        }
    }
}
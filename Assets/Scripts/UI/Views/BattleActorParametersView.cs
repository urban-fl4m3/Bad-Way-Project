using System.Collections.Generic;
using Modules.CameraModule.Components;
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
            _model.CameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>().PositionChanged 
                += HandleCameraPositionChanging;
            
            CreateActorParametersWindow();
        }

        public void Clear()
        {

        }

        public void CreateActorParametersWindow()
        {
            foreach (var actor in _model.BattleActors)
            {
                var actorName = actor.Actor.name;
                var health = actor.Health;
                var maxHealth = actor.MaxHealth;
                var isEnemy = actor.IsEnemy;

                var actorUIParameter = Instantiate(_actorUIParametersPrefab, _parent);
                actorUIParameter.Initialize(actor, actorName, 1, health, maxHealth , Canvas, isEnemy, _model);

                _actorUIParameters.Add(actorUIParameter);
                _sortingInCollection.FetchCollection(actorUIParameter.RectTransform);
            }
        }

        private void HandleCameraPositionChanging(object sender, Vector3 position)
        {
            foreach (var uiParameter in _actorUIParameters)
            {
                uiParameter.UpdatePosition();
            }

            _sortingInCollection.UpdateSorting();
        }
    }
}
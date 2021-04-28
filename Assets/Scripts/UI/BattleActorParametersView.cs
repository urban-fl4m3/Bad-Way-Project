using System;
using System.Collections.Generic;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace UI
{
    public class BattleActorParametersView : MonoBehaviour, IViewModel, ICanvasView, IActorParameter
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private ActorUIParameters _actorUIParametersPrefab;
        [SerializeField] private SortingInCollection _sortingInCollection;
        
        private readonly List<ActorUIParameters> _actorUIParameters = new List<ActorUIParameters>();
        
        private bool _isEnable = true;
        
        private BattleActorParameterModel _model;
        
        public GameObject GameObject { get; }
        public Canvas Canvas { get; set; }
        public Action<bool> OnEnableUI = delegate {  };

        public void EnableUI()
        {
            _isEnable = !_isEnable;
            OnEnableUI(_isEnable);
        }


       public void ResolveModel(IModel model)
       {
           _model = (BattleActorParameterModel)  model;
           _model.CameraController.OnCameraPositionChange += OnCameraMove;
           CreateActorParametersWindow();
       }

       public void Clear()
       {
           
       }
       
       public void CreateActorParametersWindow()
       {
           for (int a = 0; a < _model.BattleActors.Count; a++)
           {
               var index = a;
               var stat = _model.BattleActors[index].Stats;
               var target = _model.BattleActors[index].Actor.TargetForUI;
               var name = _model.BattleActors[index].Actor.name;
               var health = stat.Health;
               var maxHealth = stat.MaxHealth;
               
               var actorUIParameter = Instantiate(_actorUIParametersPrefab, _parent);
               actorUIParameter.Initialize(target, name, 1, health, maxHealth);

               _actorUIParameters.Add(actorUIParameter);
           }
           _sortingInCollection.GetCollection();
           _model.CameraController.OnCameraPositionChange += OnCameraMove;
       }

       private void OnCameraMove(Vector3 newposition)
       {
           foreach (var uiParameter in _actorUIParameters)
           {
               uiParameter.UpdatePosition();
           }
           _sortingInCollection.UpdateSorting();
       }

    }
}
using System;
using System.Collections.Generic;
using Modules.BattleModule;
using Modules.CameraModule;
using UI.Components;
using UnityEngine;

namespace UI
{
    public class BattleActorParametersView : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private ActorUIParameters _actorUIParametersPrefab;
        [SerializeField] private SortingInCollection _sortingInCollection;
        
        private readonly List<ActorUIParameters> _actorUIParameters = new List<ActorUIParameters>();
        private bool _isEnable = true;
        
        public Action<bool> OnEnableUI = delegate {  };

        public void EnableUI()
        {
            _isEnable = !_isEnable;
            OnEnableUI(_isEnable);
        }
        
        public void CreateActorParametersWindow(BattleActor battleActor)
        {
            var actorUIParameter = Instantiate(_actorUIParametersPrefab, _parent);
            actorUIParameter.Initialize( battleActor, this);
            _actorUIParameters.Add(actorUIParameter);

            Camera.main.GetComponent<CameraController>().OnCameraPositionChange += OnCameraMove;
        }
        
        private void OnCameraMove(Vector3 newposition)
        {
            foreach (var uiParameter in _actorUIParameters)
            {
                uiParameter.UpdatePosition();
            }
            _sortingInCollection.UpdateSorting();
        }
        
        public void AddListToSorting()
        {
            var actorRect = new List<RectTransform>();
            var a = 0;
            
            foreach (var param in _actorUIParameters)
            {
                actorRect.Add(param.RectTransform);
                param.transform.name = a.ToString();
                a++;
            }
            
            _sortingInCollection.AddCollection(actorRect);
        }
        
    }
}
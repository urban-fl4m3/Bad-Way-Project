using System;
using System.Collections.Generic;
using Modules.ActorModule.Components;
using Modules.BattleModule;
using Modules.CameraModule;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BattleActorParameters : MonoBehaviour
    {

        [SerializeField] private Transform _parent;
        [SerializeField] private ActorUIParameters _actorUIParametersPrefab;
        [SerializeField] private SortingInCollection _sortingInCollection;
        
        private List<ActorUIParameters> _actorUIParameters = new List<ActorUIParameters>();
        private bool _isEnable=true;
        
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

            Camera.main.GetComponent<CameraController>().OnCameraPositonChange += OnCameraMove;
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
            List<RectTransform> actorRect = new List<RectTransform>();
            int a = 0;
            foreach (var variabActorUIParameter in _actorUIParameters)
            {
                actorRect.Add(variabActorUIParameter.RectTransform);
                variabActorUIParameter.transform.name = a.ToString();
                a++;
            }
            _sortingInCollection.AddCollection(actorRect);
        }
        
    }
}
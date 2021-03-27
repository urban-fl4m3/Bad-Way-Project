using System.Collections.Generic;
using UI.Containers;
using UI.Helpers;
using UI.Views;
using UnityEngine;

namespace UI.Factories
{
    public class ViewFactory
    {
        private readonly Camera _uiCamera;
        private readonly CanvasContainer _canvasContainer;
        private readonly ViewsContainer _viewsContainer;
        
        private readonly Dictionary<string, Canvas> _canvasInstances = new Dictionary<string, Canvas>();
        
        public ViewFactory(CanvasContainer canvasContainer, ViewsContainer viewsContainer,
            Camera uiCamera)
        {
            _canvasContainer = canvasContainer;
            _viewsContainer = viewsContainer;
            _uiCamera = uiCamera;
        }

        public BaseView CreateWindow(Window viewType)
        {
            GetMainCanvas();

            var view = _viewsContainer.GetView(viewType);

            if (view == null)
            {
                Debug.LogWarning($"{nameof(_viewsContainer)} doesn't have {viewType} window.");
                return null;
            }

            return Object.Instantiate(view, GetMainCanvas().transform);
        }
        
        private Canvas GetMainCanvas()
        {
            var mainCanvasId = _canvasContainer.MainCanvas.GetInstanceID().ToString();
            if (!_canvasInstances.ContainsKey(mainCanvasId))
            {
                var canvas = Object.Instantiate(_canvasContainer.MainCanvas);
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = _uiCamera;
                canvas.planeDistance = 1.0f;
                _canvasInstances.Add(mainCanvasId, canvas);
            }

            return _canvasInstances[mainCanvasId];
        }
    }
}
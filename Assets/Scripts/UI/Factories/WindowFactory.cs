using System.Collections.Generic;
using UI.Configs;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace UI.Factories
{
    public class WindowFactory
    {
        private readonly Camera _camera;
        private WindowViewsConfig _config;

        private Dictionary<string, IViewModel> _models = new Dictionary<string, IViewModel>();
        private Dictionary<string, IButtonSubscriber> _buttonSubscribers = new Dictionary<string, IButtonSubscriber>();
        private Dictionary<string, ICanvasView> _canvasViews = new Dictionary<string, ICanvasView>();

        public WindowFactory(Camera camera, WindowViewsConfig config)
        {
            _camera = camera;
            _config = config;

        }

        public void AddWindow(string windowKey, IModel model)
        {
            var configGameObject = Object.Instantiate(_config.LoadCanvas());
            var canvas = configGameObject.GetComponent<Canvas>();

            var window = Object.Instantiate(_config.LoadView(windowKey));
            
            
            window.transform.SetParent(configGameObject.transform, false);
            
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = _camera;
            canvas.planeDistance = 1;

            var buttonSubscriber = window.GetComponent<IButtonSubscriber>();
            var modelView = window.GetComponent<IViewModel>();
            var canvasView = window.GetComponent<ICanvasView>();
            
            canvasView.Canvas = canvas;

            
            _canvasViews.Add(windowKey, canvasView);
            modelView.ResolveModel(model);

            if (buttonSubscriber != null)
            {
                _buttonSubscribers.Add(windowKey, buttonSubscriber);
                buttonSubscriber.SubscribeButtons();
            }

            _models.Add(windowKey, modelView);
        }

        public void HideWindow(string windowKey, bool a)
        {
            _canvasViews[windowKey].Canvas.enabled = a;
            _models[windowKey].GameObject.SetActive(a);
        }
        public void RemoveWindow(string windowKey)
        {
            _buttonSubscribers[windowKey].UnsubscribeButtons();
            _canvasViews[windowKey].Canvas.enabled = false;
            _models[windowKey].GameObject.SetActive(false);
        }
    }
}
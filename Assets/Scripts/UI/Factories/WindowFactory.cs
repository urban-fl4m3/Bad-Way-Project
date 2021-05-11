using System.Collections.Generic;
using Modules.CameraModule;
using UI.Configs;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace UI.Factories
{
    public class WindowFactory
    {
        private readonly CameraController _cameraController;
        private readonly WindowViewsConfig _config;

        private readonly Dictionary<string, IViewModel> _models = new Dictionary<string, IViewModel>();
        private readonly Dictionary<string, IButtonSubscriber> _buttonSubscribers = new Dictionary<string, IButtonSubscriber>();
        private readonly Dictionary<string, ICanvasView> _canvasViews = new Dictionary<string, ICanvasView>();

        public WindowFactory(CameraController cameraController, WindowViewsConfig config)
        {
            _cameraController = cameraController;
            _config = config;
        }

        public void AddWindow(string windowKey, IModel model)
        {
            var configGameObject = Object.Instantiate(_config.LoadCanvas());
            var canvas = configGameObject.GetComponent<Canvas>();

            var window = Object.Instantiate(
                _config.LoadView(windowKey), configGameObject.transform, false);
            
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = _cameraController.UiCamera.Component;
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

        public void Reset()
        {
            foreach (var viewsValue in _canvasViews.Values)
            {
                viewsValue.ResetCanvas();
            }
        }
    }
}
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

        private Dictionary<string, IViewModel> _models;
        private Dictionary<string, IButtonSubscriber> _buttonSubscribers;
        private Dictionary<string, ICanvasView> _canvasViews;

        public WindowFactory(Camera camera)
        {
            _camera = camera;
        }
        
        public void AddWindow(string windowKey, IModel model)
        {
            var canvas = _config.LoadCanvas();
            var window = _config.LoadView(windowKey);

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = _camera;
            
            window.transform.SetParent(canvas.transform);
            
            
            var buttonSubscriber = window.GetComponent<IButtonSubscriber>();
            var modelView = window.GetComponent<IViewModel>();
            
            modelView.ResolveModel(model);
        
            _buttonSubscribers.Add(windowKey, buttonSubscriber);
            buttonSubscriber.SubscribeButtons();
            
            _models.Add(windowKey, modelView);
        }

        public void RemoveWindow(string windowKey)
        {
            _buttonSubscribers[windowKey].UnsubscribeButtons();
            _canvasViews[windowKey].Canvas.enabled = false;
            _models[windowKey].GameObject.SetActive(false);
        }
    }
}
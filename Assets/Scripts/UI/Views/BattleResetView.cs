﻿using Modules.BattleModule;
using UI.Interface;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class BattleResetView: MonoBehaviour, IViewModel, ICanvasView, IActorParameter
    {
        public GameObject GameObject { get; }
        public Canvas Canvas { get; set; }
        

        public Image BackGround;
        public Button ResetButton;
        private BattleResetModel _model;
        
        public void ResolveModel(IModel model)
        {
            _model = (BattleResetModel) model;
            ResetButton.onClick.AddListener(_model.BattleReset.Load);
            ResetButton.onClick.AddListener(DisableResetView);
            _model.DeathMatchRules.RulesComplete += OnRulesComplete;
        }

        private void DisableResetView()
        {
            ResetButton.gameObject.SetActive(false);
            BackGround.enabled = false;
        }
        
        public void OnRulesComplete(object sender, Rules rules)
        {
            if (rules == Rules.PlayerWin)
            {
                ResetButton.gameObject.SetActive(true);
                BackGround.enabled = true;
            }

            if (rules == Rules.PlayerLose)
            {
                ResetButton.gameObject.SetActive(true);
                BackGround.enabled = true;
            }
            Debug.Log(rules);
        }
        
        public void ResetCanvas()
        {
            
        }
        
        public void Clear()
        {
            
        }

        
        public void CreateActorParametersWindow()
        {
            
        }
    }
}
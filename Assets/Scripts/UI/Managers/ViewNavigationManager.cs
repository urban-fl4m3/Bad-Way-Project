using System;
using System.Collections.Generic;
using System.Linq;
using UI.Helpers;
using UI.Navigations;
using UI.Providers;

namespace UI.Managers
{
    public class ViewNavigationManager
    {
        private readonly ViewNavigationProvider _provider;
        private readonly Stack<BaseViewNavigation> _activeSchemes = new Stack<BaseViewNavigation>();

        public BaseViewNavigation CurrentScheme => _activeSchemes.Any() ? _activeSchemes.Peek() : null;

        public ViewNavigationManager(ViewNavigationProvider schemeProvider)
        {
            _provider = schemeProvider;
        }

        public void AddScheme(Navigation type)
        {
            var currentSchemeType = CurrentScheme?.SchemeType ?? Navigation.Unknown;

            var scenarioInStack = _activeSchemes
                .FirstOrDefault(s => s.SchemeType == type);

            if (scenarioInStack != null)
            {
                throw new Exception($"Behaviour '{type}' is already added!");
            }

            var newScheme = _provider.GetScheme(type);
            newScheme.Init(CompleteScheme);
            _activeSchemes.Push(newScheme);
            CurrentScheme?.Execute(currentSchemeType);
        }

        public void CompleteScheme(Navigation nextBehaviourType)
        {
            CompleteBehaviourInternal(nextBehaviourType);
        }

        private void CompleteBehaviourInternal(Navigation nextSchemeType)
        {
            var currentSchemeType = CurrentScheme.SchemeType;

            CurrentScheme.Finish();
            _activeSchemes.Pop();

            if (nextSchemeType != Navigation.Unknown)
            {
                var schemeInStack = _activeSchemes
                    .FirstOrDefault(s => s.SchemeType == nextSchemeType);
                if (schemeInStack != null)
                {
                    if (schemeInStack != _activeSchemes.Peek())
                    {
                        throw new Exception("Wrong next scheme type!");
                    }
                }
                else
                {
                    var nextBehaviour = _provider.GetScheme(nextSchemeType);
                    nextBehaviour.Init(CompleteScheme);
                    _activeSchemes.Push(nextBehaviour);
                    CurrentScheme.Execute(currentSchemeType);

                    return;
                }
            }

            throw new Exception("There is no active schemes!");
        }
    }
}
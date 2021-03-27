using System;
using System.Collections.Generic;
using System.Linq;
using UI.Navigations;
using UI.Helpers;

namespace UI.Providers
{
    public class ViewNavigationProvider
    {
        private readonly Dictionary<Navigation, BaseViewNavigation> _schemes;
        
        public ViewNavigationProvider(List<BaseViewNavigation> schemes)
        {
            if (schemes == null || !schemes.Any())
            {
                throw new ArgumentNullException(nameof(schemes));
            }

            _schemes = schemes.ToDictionary(
                s => s.SchemeType,
                s => s);
        }
        
        public BaseViewNavigation GetScheme(Navigation schemeType)
        {
            if (!_schemes.TryGetValue(schemeType, out var scenario))
            {
                throw new Exception($"There is no scheme with type {schemeType}!");
            }

            return scenario;
        }
    }
}
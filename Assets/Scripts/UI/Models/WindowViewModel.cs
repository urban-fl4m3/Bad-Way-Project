using System;
using UI.Helpers;
using UI.Views;

namespace UI.Models
{
    [Serializable]
    public struct WindowViewModel
    {
        public Window Type;
        public BaseView View;
    }
}
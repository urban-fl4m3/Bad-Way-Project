using Modules.TickModule;
using UnityEngine.EventSystems;

namespace UI.Components
{
    static class HoverCheck

    {
        public static bool IsCover => EventSystem.current.IsPointerOverGameObject();
    }
}
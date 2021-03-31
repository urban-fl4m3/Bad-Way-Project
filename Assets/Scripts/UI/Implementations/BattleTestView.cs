using UI.Views.Interfaces;
using UnityEngine;

namespace UI.Implementations
{
    public class BattleTestView : MonoBehaviour, ICompositeView, IViewCallbacks
    {
        public Canvas ViewCanvas { get; set; }
        public GameObject ViewObject { get; private set; }
        public string ViewToken { get; private set; }

        public void OnInitialize()
        {
            ViewObject = gameObject;
            ViewToken = "BattleTestView";
        }
    }
}
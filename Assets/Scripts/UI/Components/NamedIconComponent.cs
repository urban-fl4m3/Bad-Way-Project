using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class NamedIconComponent : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        public Text Name => _name;
        public Image Icon => _icon;
        public Button Button => _button;
    }
}
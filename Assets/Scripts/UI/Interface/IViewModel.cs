using UI.Models;
using UnityEngine;

namespace UI.Interface
{
    public interface IViewModel
    {
        GameObject GameObject { get; }
        void ResolveModel(IModel model);
        void Clear();
    }
}
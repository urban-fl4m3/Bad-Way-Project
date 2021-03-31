using UnityEngine;

namespace UI.Views.Interfaces
{
    public interface ICompositeView
    {
        Canvas ViewCanvas { get; set; }
        GameObject ViewObject { get; }  
        string ViewToken { get; }
    }
}
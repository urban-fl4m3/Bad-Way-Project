using System.Collections;
using System.Collections.Generic;
using Modules.BattleModule;
using Modules.BattleModule.Managers;
using Modules.GridModule;
using UnityEngine;

public class UIController : MonoBehaviour
{ 
    public GridController _GridController;
   public BattleActManager _BattleActManager;

   public void ShowMovementPosition(int a)
   {
       _GridController.HighlightRelativeCells(_BattleActManager.Actors[0].Placement, a);
   }
}

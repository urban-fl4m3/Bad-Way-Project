using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Modules.PlayerModule.Actors
{
    public class PlayerActorsCollection : List<PlayerActorData>
    {
        public PlayerActorsCollection(IEnumerable<PlayerActorData> actorsData)
        {
            foreach (var actorData in actorsData)
            {
                AddActorDataInternal(actorData);
            }
        }

        public PlayerActorsCollection()
        {
            
        }

        public void AddActorData(PlayerActorData actorData)
        {
            AddActorDataInternal(actorData);
        }

        private void AddActorDataInternal(PlayerActorData actorData)
        {
            var primaryStatsCount = StatConsts.PrimaryStatsCount;
            
            if (actorData.Upgrades.Count != primaryStatsCount)
            {
                Debug.LogError($"Actor can't contain more than {primaryStatsCount} primary stats upgrades." +
                               $"Actor's ID: {actorData.Id}");
                return;
            }
                
            Add(actorData);
        }
    }
}
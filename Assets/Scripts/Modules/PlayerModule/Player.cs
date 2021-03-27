using Modules.PlayerModule.Actors;

namespace Modules.PlayerModule
{
    public class Player
    {
        public readonly PlayerActorsCollection ActorsCollection;

        public Player(PlayerActorsCollection actorsCollection)
        {
            ActorsCollection = actorsCollection;
        }
    }
}
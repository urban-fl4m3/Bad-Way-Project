using Modules.PlayerModule.Actors;

namespace Modules.PlayerModule
{
    public class Player
    {
        private readonly PlayerActorsCollection _actorsCollection;

        public Player(PlayerActorsCollection actorsCollection)
        {
            _actorsCollection = actorsCollection;
        }
    }
}
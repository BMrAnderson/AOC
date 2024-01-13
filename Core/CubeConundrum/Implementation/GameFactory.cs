using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum.Implementation
{
    internal class GameFactory : IGameFactory
    {
        public Game Create(int id, IEnumerable<Subset> subsets)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var game = new Game(id, subsets);

            return game;
        }
    }
}

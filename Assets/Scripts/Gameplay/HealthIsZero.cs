using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player health reaches 0. This usually would result in a 
    /// PlayerDeath event.
    /// </summary>
    /// <typeparam name="HealthIsZero"></typeparam>
    /// 




    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Health health;

        public override void Execute()
        {
            if (model.player.health.IsAlive == false)
            {
                Schedule<PlayerDeath>();
            }

        }
    }
}
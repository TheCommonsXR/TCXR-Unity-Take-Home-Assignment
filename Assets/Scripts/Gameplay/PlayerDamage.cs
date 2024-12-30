using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Core;
using Platformer.Model;
using Platformer.Gameplay;

namespace Platformer.Gameplay
{
    public class PlayerDamage : Simulation.Event<PlayerDamage>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public int enemydamage;

        public override void Execute()
        {
            var player = model.player;

            if (player.health.IsAlive)
            {
                player.health.Decrement(enemydamage);

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);

                player.animator.SetTrigger("hurt");
                player.UpdateDamageText(enemydamage);
                player.ToggleTextPopup();
            }
        }
    }
}

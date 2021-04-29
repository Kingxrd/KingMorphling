using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Divine;
using Divine.SDK;
using System.Threading.Tasks;

namespace KingMorphling
{
    public class Bootstrap : Bootstrapper
    {
        protected override void OnActivate()
        {
            if (EntityManager.LocalHero.HeroId != HeroId.npc_dota_hero_morphling)
            {
                return;
            }
            new MainMenu();
        }
    }
}

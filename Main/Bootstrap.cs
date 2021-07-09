using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Service;

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

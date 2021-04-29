using System;
using System.Collections.Generic;
using System.Linq;
using Divine;
using Divine.Menu.Items;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace KingMorphling
{

    class Helper
    {

        public static bool myHero = EntityManager.LocalHero.HeroId != HeroId.npc_dota_hero_morphling;
        public static Hero LocalHero;
        
        public Ability ability;
        public Ability reversp;
        private bool IsMove;

        public static bool Entity(Unit unit , Ability ability,Hero hero)
        {
            if(ability.IsInAbilityPhase && !unit.IsAlly(LocalHero))
            {
                return true;
            }
            return false; 
        }

        public Helper()
        {
           //reversp = LocalHero.Spellbook.GetSpellById(AbilityId.magnataur_reverse_polarity);
        }
        public static bool IsCastable(Hero hero, Ability ability)
        {
            if (ability != null && ability.ManaCost < hero.Mana && ability.Level > 0 && (ability.Cooldown == 0))
            {
                return true;
            }

            return false;
        }
        public static Unit ClosestToMouse(Unit MyHero)
        {

            return EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(MyHero)
            && x.IsAlive
            && x.IsVisible
            && x.IsValid
            && !x.IsIllusion
            && x.Distance2D(GameManager.MousePosition) < 750).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
        }
        public static bool IsShift(Hero hero, Ability ability)
        {
            if (ability != null && ability.Level > 0)
            {
                return true;
            }

            return false;
        }
        //reversp = LocalHero.Spellbook.GetSpellById(AbilityId.magnataur_reverse_polarity);
        //var reversp = LocalHero.Spellbook.GetSpellById(AbilityId.magnataur_reverse_polarity);
        public static bool IsReversp(Hero hero, Ability ability, AbilityId abilityId, Unit unit)
            {
                if (ability == LocalHero.Spellbook.GetSpellById(AbilityId.magnataur_reverse_polarity) && ability.IsInAbilityPhase && unit.IsEnemy(LocalHero))
                {
                    return true;
                }

                return false;
            }

        public static bool InputManager_MouseKeyDown2(MouseEventArgs e)
        {
            if (e.MouseKey != MouseKey.Left)
            {
                return false;
            }

            return true;

        }
       
    }
    
       
    
}

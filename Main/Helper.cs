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
        public static bool ManaCheckItemAndHero(float manaCost, float manaPool)
        {
            if (manaPool - manaCost > 0)
                return true;
            return false;
        }
        public static Item FindItemMain(Unit unit, AbilityId abilityId)
        {
            var Item = unit.Inventory.GetItems((ItemSlot)0, (ItemSlot)5).FirstOrDefault(x => x.Id == abilityId);
            return Item;

        }
        public static bool CanBeCasted(AbilityId abilityId, Unit MyHero)
        {
            bool Value;
            Item item = FindItemMain(MyHero, abilityId);
            if (item != null)
            {
                if (item.Cooldown == 0 && ManaCheckItemAndHero(item.ManaCost, MyHero.Mana) && item.IsValid && FindItemMain(MyHero, item.Id) != null)
                {

                    Value = true;
                }
                else
                {
                    Value = false;
                }
            }
            else
            {
                Value = false;
            }

            return Value;
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

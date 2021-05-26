using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Divine.SDK;
using Divine;
using Divine.SDK.Orbwalker;
using System.Threading.Tasks;
using Divine.SDK.Helpers;
using Divine.SDK.Extensions;

namespace KingMorphling
{
    class Combo
    {
        private Hero LocalHero;
        private Sleeper sleeper = new Sleeper();
        private Sleeper sleeper2 = new Sleeper();
        private Sleeper SleeperOrbWalker = new Sleeper();
        private Item item_ethereal_blade;
        private Item item_manta;
        private Item item_bloodthorn;
        private Item item_nullifier;
        private Item item_black_king_bar;
        private Item item_minotaur_horn;
        private Item item_essence_ring;
        private Item item_mjollnir;
        public void Enable()
        {
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            LocalHero = EntityManager.LocalHero;
        }

        public void Disable()
        {
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }



        private void UpdateManager_IngameUpdate()
        {
           
            var Waveform = LocalHero.Spellbook.Spell1;
            var AdaptiveA = LocalHero.Spellbook.Spell2;
           

            foreach (var enemy in EntityManager.GetEntities<Hero>().Where(x => x.IsEnemy(LocalHero) && !x.IsIllusion))
            {
                
                if (Helper.IsCastable(LocalHero, Waveform) && !sleeper.Sleeping && MainMenu.ListSpellsToggler.FirstOrDefault(x => x.Key == Waveform.Id).Value)
                {
                    Waveform.Cast(LocalHero.Position.Extend(enemy.Position, LocalHero.Distance2D(enemy.Position) + 150));
                    sleeper.Sleep(100);

                }

                if (item_ethereal_blade == null || !item_ethereal_blade.IsValid)
                {
                    item_ethereal_blade = Helper.FindItemMain(LocalHero, AbilityId.item_ethereal_blade);
                }
                if (item_ethereal_blade != null && Helper.CanBeCasted(AbilityId.item_ethereal_blade, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_ethereal_blade.Id).Value)
                {
                    item_ethereal_blade.Cast(enemy);
                    return;
                }


                if (Helper.IsCastable(LocalHero, AdaptiveA) && !sleeper2.Sleeping && MainMenu.ListSpellsToggler.FirstOrDefault(x => x.Key == AdaptiveA.Id).Value)
                {
                        AdaptiveA.Cast(enemy);
                        sleeper2.Sleep(100);
                }
                
                if (!SleeperOrbWalker.Sleeping)
                {
                    OrbwalkerManager.OrbwalkTo(enemy);
                    SleeperOrbWalker.Sleep(75);
                }
                //manta
                if (item_manta == null || !item_manta.IsValid)
                {
                    item_manta = Helper.FindItemMain(LocalHero, AbilityId.item_manta);
                }
                if (item_manta != null && Helper.CanBeCasted(AbilityId.item_manta, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_manta.Id).Value)
                {
                    item_manta.Cast();
                    return;
                }
                //blood
                if (item_bloodthorn == null || !item_bloodthorn.IsValid)
                {
                    item_bloodthorn = Helper.FindItemMain(LocalHero, AbilityId.item_bloodthorn);
                }
                if (item_bloodthorn != null && Helper.CanBeCasted(AbilityId.item_bloodthorn, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_bloodthorn.Id).Value)
                {
                    item_bloodthorn.Cast(enemy);
                    return;
                }
                //Nulifier
                if (item_nullifier == null || !item_nullifier.IsValid)
                {
                    item_nullifier = Helper.FindItemMain(LocalHero, AbilityId.item_nullifier);
                }
                if (item_nullifier != null && Helper.CanBeCasted(AbilityId.item_nullifier, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_nullifier.Id).Value)
                {
                    item_nullifier.Cast(enemy);
                    return;
                }
                //Minot
                if (item_minotaur_horn == null || !item_minotaur_horn.IsValid)
                {
                    item_minotaur_horn = Helper.FindItemMain(LocalHero, AbilityId.item_minotaur_horn);
                }
                if (item_minotaur_horn != null && Helper.CanBeCasted(AbilityId.item_minotaur_horn, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_minotaur_horn.Id).Value)
                {
                    item_minotaur_horn.Cast();
                    return;
                }
                //Bkb
                    if (item_black_king_bar == null || !item_black_king_bar.IsValid)
                    {
                        item_black_king_bar = Helper.FindItemMain(LocalHero, AbilityId.item_black_king_bar);
                    }
                    if (item_black_king_bar != null && Helper.CanBeCasted(AbilityId.item_black_king_bar, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_black_king_bar.Id).Value)
                    {
                        item_black_king_bar.Cast();
                        return;
                    }
                
                //essence 
                if (item_essence_ring == null || !item_essence_ring.IsValid)
                {
                    item_essence_ring = Helper.FindItemMain(LocalHero, AbilityId.item_essence_ring);
                }
                if (item_essence_ring != null && Helper.CanBeCasted(AbilityId.item_essence_ring, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_essence_ring.Id).Value)
                {
                    item_essence_ring.Cast();
                    return;
                }
                //
                if (item_mjollnir == null || !item_mjollnir.IsValid)
                {
                    item_mjollnir = Helper.FindItemMain(LocalHero, AbilityId.item_mjollnir);
                }
                if (item_mjollnir != null && Helper.CanBeCasted(AbilityId.item_mjollnir, LocalHero) && MainMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_mjollnir.Id).Value)
                {
                    item_mjollnir.Cast(LocalHero);
                    return;
                }
                

                sleeper.Sleep(1000);


            }


        }
        public void returnperc()
        {
            MainMenu.Perc.Value = MainMenu.perkValue;
        }

    }
}

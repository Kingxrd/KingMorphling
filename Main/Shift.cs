﻿using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Update;

namespace KingMorphling
{
    class Shift 
    {
        private Hero LocalHero;

        public Sleeper sleeper = new Sleeper();
        
        
       

        public void Enable()
        {
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            LocalHero = EntityManager.LocalHero;
            
        }

        public void Disable()
        {
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }
        public void UpdateManager_IngameUpdate()
        {
          
            var toAgi = LocalHero.Spellbook.Spell4;
            var toStr = LocalHero.Spellbook.Spell5;
            var Hptoogle = MainMenu.Perc.Value;
            var curentHp = LocalHero.Health;

          

            if (LocalHero.HasModifier("modifier_fountain_aura_buff"))
            {
                return;
            }
            if (LocalHero.IsSilenced())
            {
                return;
            }
            
            if (LocalHero.Modifiers.Any(x => x.Name == "modifier_morphling_replicate" || x.Name == "modifier_bashed" || x.Name == "modifier_eul_cyclone" || x.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison" || x.Name == "modifier_shadow_demon_disruption" || x.Name == "modifier_invoker_tornado" ||
                 x.Name == "modifier_legion_commander_duel" || x.Name == "modifier_axe_berserkers_call" || x.Name == "modifier_winter_wyvern_winters_curse" || x.Name == "modifier_bane_fiends_grip" || x.Name == "modifier_bane_nightmare" ||
                 x.Name == "modifier_faceless_void_chronosphere_freeze" || x.Name == "modifier_enigma_black_hole_pull" || x.Name == "modifier_magnataur_reverse_polarity" || x.Name == "modifier_pudge_dismember" || x.Name == "modifier_shadow_shaman_shackles"
                 || x.Name == "modifier_techies_stasis_trap_stunned" || x.Name == "modifier_storm_spirit_electric_vortex_pull" || x.Name == "modifier_tidehunter_ravage" || x.Name == "modifier_windrunner_shackle_shot" || x.Name == "modifier_item_nullifier_mute"))
            {

                return;

            }

            if (LocalHero.HeroId != HeroId.npc_dota_hero_morphling || !LocalHero.IsAlive || LocalHero.IsStunned() || LocalHero.IsHexed() || LocalHero.IsInvulnerable() || LocalHero.IsInvisible())
            {
                return;
            }
            if (sleeper.Sleeping)
            {
                return;
            }
            
            

            if (Helper.IsShift(LocalHero, toStr))
            {
                if (curentHp < Hptoogle && !LocalHero.HasModifier("modifier_morphling_morph_str"))
                {
                    toStr.CastToggle();
                }
                else if (LocalHero.HasModifier("modifier_morphling_morph_str") && curentHp > Hptoogle)
                {
                    toStr.CastToggle();
                }

            }
            if (Helper.IsShift(LocalHero, toAgi) && curentHp > Hptoogle)
            {
                if (curentHp > Hptoogle + 100 && !LocalHero.HasModifier("modifier_morphling_morph_agi"))
                {
                    toAgi.CastToggle();
                }
                else if (LocalHero.HasModifier("modifier_morphling_morph_agi") && curentHp < Hptoogle)
                {
                    toAgi.CastToggle();
                }
            }
            sleeper.Sleep(100);
        }
        
  

       

       
    }
}
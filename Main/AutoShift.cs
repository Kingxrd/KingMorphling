using System.Collections.Generic;
using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Components;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Order.EventArgs;
using Divine.Particle.EventArgs;
using Divine.Update;

namespace KingMorphling
{
    class AutoShift
    {
        public static OrderAddingEventArgs e;
        private static Hero LocalHero;
        public Sleeper autoshsleeper = new Sleeper();
        public Sleeper sleeper = new Sleeper();
        public ParticleAddedEventArgs particleAdded;
       
        public void Enable()
        {
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            LocalHero = EntityManager.LocalHero;
            
        }

        public void Disable()
        {
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
            
        }


        public static readonly List<AbilityId> StunList = new List<AbilityId>
        {
             
         {AbilityId.lion_impale},
         {AbilityId.sven_storm_bolt},
         {AbilityId.magnataur_reverse_polarity},
         {AbilityId.pudge_dismember},
         {AbilityId.axe_berserkers_call},
         {AbilityId.axe_culling_blade},
         {AbilityId.ancient_apparition_ice_blast_release},
         {AbilityId.alchemist_unstable_concoction},
         {AbilityId.brewmaster_earth_hurl_boulder},
         {AbilityId.earth_spirit_boulder_smash},
         {AbilityId.earth_spirit_geomagnetic_grip},
         {AbilityId.lion_finger_of_death},
         {AbilityId.beastmaster_primal_roar},
         {AbilityId.bane_fiends_grip},
         {AbilityId.batrider_flaming_lasso},
         {AbilityId.rattletrap_battery_assault},
         {AbilityId.rattletrap_hookshot},
         {AbilityId.chaos_knight_chaos_bolt},
         {AbilityId.dragon_knight_dragon_tail},
         {AbilityId.doom_bringer_doom},
         {AbilityId.disruptor_static_storm},
         {AbilityId.enigma_black_hole},
         {AbilityId.enigma_malefice},
         {AbilityId.elder_titan_earth_splitter},
         {AbilityId.elder_titan_echo_stomp_spirit},
         {AbilityId.elder_titan_echo_stomp},
         {AbilityId.earthshaker_echo_slam},
         {AbilityId.earthshaker_fissure},
         {AbilityId.faceless_void_chronosphere},
         {AbilityId.gyrocopter_call_down},
         {AbilityId.gyrocopter_homing_missile},
         {AbilityId.invoker_chaos_meteor},
         {AbilityId.invoker_sun_strike},
         {AbilityId.jakiro_ice_path},
         {AbilityId.kunkka_torrent},
         {AbilityId.kunkka_ghostship},
         {AbilityId.lina_light_strike_array},
         {AbilityId.lina_laguna_blade},
         {AbilityId.leshrac_split_earth},
         {AbilityId.legion_commander_duel},
         {AbilityId.nyx_assassin_impale},
         {AbilityId.necrolyte_reapers_scythe},
         {AbilityId.obsidian_destroyer_astral_imprisonment},
         {AbilityId.obsidian_destroyer_sanity_eclipse},
         {AbilityId.ogre_magi_fireblast},
         {AbilityId.slardar_slithereen_crush},
         {AbilityId.skywrath_mage_mystic_flare},
         {AbilityId.skeleton_king_hellfire_blast},
         {AbilityId.winter_wyvern_winters_curse},
         {AbilityId.shadow_shaman_shackles},
         {AbilityId.lion_voodoo},
         {AbilityId.shadow_shaman_voodoo},
         {AbilityId.sandking_burrowstrike},
         {AbilityId.sandking_epicenter},
         {AbilityId.tiny_avalanche},
         {AbilityId.tidehunter_ravage},
         {AbilityId.techies_suicide},
         {AbilityId.vengefulspirit_magic_missile},
         {AbilityId.witch_doctor_paralyzing_cask},
         {AbilityId.windrunner_shackleshot},
         {AbilityId.warlock_rain_of_chaos},
         {AbilityId.lich_sinister_gaze},
         {AbilityId.queenofpain_scream_of_pain},
         {AbilityId.mirana_arrow}
         



    };
        

       

        public void UpdateManager_IngameUpdate()
        {
            

            var toStr1 = LocalHero.Spellbook.Spell5;
           


            if (LocalHero.ClassId != ClassId.CDOTA_Unit_Hero_Morphling)
            {
                return;
            }
            
            if (!autoshsleeper.Sleeping)
            {
                if (LocalHero.Modifiers.Any(x => x.Name == "modifier_morphling_replicate" || x.Name == "modifier_bashed" || x.Name == "modifier_eul_cyclone" || x.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison" || x.Name == "modifier_shadow_demon_disruption" || x.Name == "modifier_invoker_tornado" ||
                 x.Name == "modifier_legion_commander_duel" || x.Name == "modifier_axe_berserkers_call" || x.Name == "modifier_winter_wyvern_winters_curse" || x.Name == "modifier_bane_fiends_grip" || x.Name == "modifier_bane_nightmare" ||
                 x.Name == "modifier_faceless_void_chronosphere_freeze" || x.Name == "modifier_enigma_black_hole_pull" || x.Name == "modifier_magnataur_reverse_polarity" || x.Name == "modifier_pudge_dismember" || x.Name == "modifier_shadow_shaman_shackles"
                 || x.Name == "modifier_techies_stasis_trap_stunned" || x.Name == "modifier_storm_spirit_electric_vortex_pull" || x.Name == "modifier_tidehunter_ravage" || x.Name == "modifier_windrunner_shackle_shot" || x.Name == "modifier_item_nullifier_mute"))
                {

                    return;

                }

                if (LocalHero.HeroId != HeroId.npc_dota_hero_morphling || !LocalHero.IsAlive || LocalHero.IsStunned() || LocalHero.IsHexed() || LocalHero.IsInvulnerable()|| LocalHero.IsInvisible())
                {
                    return;
                }
                foreach (var enemy in EntityManager.GetEntities<Hero>().Where(x => x.IsEnemy(LocalHero) && !x.IsIllusion && x.Spellbook.Spells.Any(y => y.IsInAbilityPhase)))
                {
                    Ability ability = enemy.Spellbook.Spells.Where(x => x.IsInAbilityPhase).FirstOrDefault();
 
                   
                    foreach (var stunabi in StunList)
                    {
                        
                        if (LocalHero.IsInRange(enemy, 750) && stunabi == ability.Id && ability.IsInAbilityPhase)
                        {
                           MainMenu.Perc.Value = MainMenu.Perc.MaxValue;
   
                        }
                        else if(ability.IsCooldownFrozen)
                        {  
                          continue;
                        }

                        autoshsleeper.Sleep(1000);
                    }
 
                }
            }
            if (!autoshsleeper.Sleeping && MainMenu.Perc.Value == MainMenu.Perc.MaxValue)
            {
                returnperc();
            }
        }
        public void returnperc()
        {
            MainMenu.Perc.Value = MainMenu.perkValue;
        }

    }
}

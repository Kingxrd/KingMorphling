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
    class Save
    {
        private Hero LocalHero;
        private Sleeper sleeper;


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
            
            if (LocalHero.Modifiers.Any(x => x.Name == "modifier_bashed" || x.Name == "modifier_eul_cyclone" || x.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison" || x.Name == "modifier_shadow_demon_disruption" || x.Name == "modifier_invoker_tornado" ||
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
            var replicate = LocalHero.GetAbilityById(AbilityId.morphling_morph_replicate);
            var minHp = MainMenu.shiftSlider2.Value;
            if (LocalHero.HasModifier("modifier_morphling_replicate") && !replicate.IsHidden && replicate.Cooldown == 0)
            {
                if ((float)LocalHero.Health / LocalHero.MaximumHealth * 100f < minHp )
                {
                    replicate.Cast();
                    
                }
                
            }
            
        }

    }

}


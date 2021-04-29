using Divine;
using Divine.SDK;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMorphling
{
    class Abusev2
    {
        public Hero LocalHero;
        public Item item;
        public Unit unit;
        public Sleeper sleeper = new Sleeper();
        public Sleeper sleeper2 = new Sleeper();
        private int percValue = MainMenu.perkValue;
        private float[] Orders = new float[6];


        public void Enable()
        {
            UpdateManager.CreateIngameUpdate(30, UpdateManager_IngameUpdate);

            LocalHero = EntityManager.LocalHero;
        }

        public void Disable()
        {
            UpdateManager.DestroyIngameUpdate(UpdateManager_IngameUpdate);
        }
        public static readonly List<AbilityId> ItemList = new List<AbilityId>
        {
            {AbilityId.item_branches},
            {AbilityId.item_gauntlets},
            {AbilityId.item_power_treads},
            {AbilityId.item_circlet},
            {AbilityId.item_belt_of_strength},
            {AbilityId.item_ogre_axe},
            {AbilityId.item_wraith_band},
            {AbilityId.item_black_king_bar},
            {AbilityId.item_manta},
            {AbilityId.item_sange_and_yasha},
            {AbilityId.item_sange},
            {AbilityId.item_satanic},
            {AbilityId.item_ultimate_scepter},
            {AbilityId.item_lotus_orb},
            {AbilityId.item_skadi},

        };
        private void UpdateManager_IngameUpdate()
        {

            unit = LocalHero;

             

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

            if (LocalHero.IsValid)
            {
               foreach(var itemch in ItemList)
                {
                    IsKeyDown();
                }
                
            }


        }

        public void IsKeyDown()
        {
            unit.Inventory.Move(ItemSlot.MainSlot_1, ItemSlot.BackPack_1);
            unit.Inventory.Move(ItemSlot.MainSlot_2, ItemSlot.BackPack_1);
            unit.Inventory.Move(ItemSlot.MainSlot_3, ItemSlot.BackPack_1);
            unit.Inventory.Move(ItemSlot.MainSlot_4, ItemSlot.BackPack_1);
            unit.Inventory.Move(ItemSlot.MainSlot_5, ItemSlot.BackPack_1);
            unit.Inventory.Move(ItemSlot.MainSlot_6, ItemSlot.BackPack_1);
        }
        public void returnperc2()
        {
            MainMenu.Perc.Value = MainMenu.perkValue;
        }
        public static bool FindItemBool(Unit unit)
        {
            var Item = unit.Inventory.Items.FirstOrDefault();

            bool BoolItem;

            if (Item != null && !Item.IsInIndefiniteCooldown)
            {
                BoolItem = true;
            }
            else
            {
                BoolItem = false;
            }

            return BoolItem;
        }
    }
}

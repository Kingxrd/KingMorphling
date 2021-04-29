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
    class Abuse
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
        private async void UpdateManager_IngameUpdate()
        {
            
            unit = LocalHero;

            
            if(LocalHero.Modifiers.Any(x => x.Name == "modifier_morphling_replicate" || x.Name == "modifier_bashed" || x.Name == "modifier_eul_cyclone" || x.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison" || x.Name == "modifier_shadow_demon_disruption" || x.Name == "modifier_invoker_tornado"||
                x.Name == "modifier_legion_commander_duel"|| x.Name == "modifier_axe_berserkers_call" || x.Name == "modifier_winter_wyvern_winters_curse" || x.Name == "modifier_bane_fiends_grip" || x.Name == "modifier_bane_nightmare"||
                x.Name == "modifier_faceless_void_chronosphere_freeze"|| x.Name == "modifier_enigma_black_hole_pull"|| x.Name == "modifier_magnataur_reverse_polarity"|| x.Name == "modifier_pudge_dismember"|| x.Name == "modifier_shadow_shaman_shackles"
                || x.Name == "modifier_techies_stasis_trap_stunned"|| x.Name == "modifier_storm_spirit_electric_vortex_pull" || x.Name == "modifier_tidehunter_ravage" || x.Name == "modifier_windrunner_shackle_shot" || x.Name == "modifier_item_nullifier_mute"))
            {

                return;

            }

            if (LocalHero.HeroId != HeroId.npc_dota_hero_morphling || !LocalHero.IsAlive  || LocalHero.IsStunned() || LocalHero.IsHexed() || LocalHero.IsInvulnerable() || LocalHero.IsInvisible())
            {
                return;
            }

            if (LocalHero.IsValid && !sleeper.Sleeping)
            {
                if (!sleeper2.Sleeping)
                {

                    var lochstr = LocalHero.TotalStrength;

                    
                    if (LocalHero.IsValid)
                    {
                        MainMenu.Perc.Value = 100;
                        if (!sleeper.Sleeping)
                        {
                            
                            foreach (var listitem in ItemList)
                            {
                                for (int i = 0; i < Orders.Length; i++)
                                {
                                    sleeper.Sleep(300);
                                    var order = Orders.OrderBy(x => x).FirstOrDefault();
                                    var index = Array.IndexOf(Orders, order);

                                    unit.Inventory.Move((ItemSlot)index, ItemSlot.BackPack_1);

                                    await Task.Delay(100);

                                    unit.Inventory.Move(ItemSlot.BackPack_1, (ItemSlot)index);

                                    Orders[index] = GameManager.RawGameTime;
                                }

                                goto Finish;

                            }
                           
                        }
                    }
                    
                }
            }
            
            Finish:
            if (LocalHero.Strength <= 2 || LocalHero.Health < 200 && LocalHero.Strength < 30)
            {
                MainMenu.Perc.Value = 5999;
                
                
            }
            if(LocalHero.Strength >= 120 && FindItemBool(LocalHero))
            {
                MainMenu.Perc.Value = percValue;
                MainMenu.HpToggleIntervalhd.Value = false;
            }
            sleeper2.Sleep(300);
            
            
            
            



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

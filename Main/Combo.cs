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
        private Hero TargetHero;
        private Sleeper sleeper = new Sleeper();
        private Sleeper sleeper2 = new Sleeper();
        private Sleeper sleeper3 = new Sleeper();
        private Sleeper SleeperOrbWalker = new Sleeper();
        
       
        public Combo ()
        {
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            LocalHero = EntityManager.LocalHero;
            TargetHero = (Hero)TargetSelector.ClosestToMouse(LocalHero);
        }

        private void UpdateManager_IngameUpdate()
        {
            if(!MainMenu.ComboPressed)
            {
                return;
            }
            var Waveform = LocalHero.Spellbook.Spell1;
            var AdaptiveA = LocalHero.Spellbook.Spell2;
            var AdaptiveS = LocalHero.Spellbook.Spell3;
            var ShiftA = LocalHero.Spellbook.Spell4;
            var ShiftS = LocalHero.Spellbook.Spell5;
           
            
            if (Helper.IsCastable(LocalHero, Waveform) && !sleeper.Sleeping)
            {
                Waveform.Cast(LocalHero.Position.Extend(TargetHero.Position, LocalHero.Distance2D(TargetHero.Position)+150));
                sleeper.Sleep(100); 
                
            }
            if (Helper.IsCastable(LocalHero, AdaptiveA) && !sleeper2.Sleeping)
            {
                AdaptiveA.Cast(TargetHero);
                sleeper2.Sleep(100);

            }
            
            if (!SleeperOrbWalker.Sleeping)
            {
                OrbwalkerManager.OrbwalkTo(TargetHero);
                SleeperOrbWalker.Sleep(75);
            }
            sleeper.Sleep(1000);
        }

        public void Dispose ()
        {
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }

    }
}

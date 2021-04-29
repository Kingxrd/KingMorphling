using Divine;
using Divine.SDK.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMorphling
{
    class TargetSelector
    {


        public static Unit ClosestToMouse(Unit MyHero)
        {

            return EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(MyHero)
            && x.IsAlive
            && x.IsVisible
            && x.IsValid
            && !x.IsIllusion
            && x.Distance2D(GameManager.MousePosition) < 750).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
        }

        //public static Hero CastToMe(Hero MyHero, AbilityId ability)
        //{

        //    return EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(MyHero)
        //    && x.IsAlive
        //    && x.IsVisible
        //    && x.IsValid
        //    && !x.IsIllusion
        //    && x.GetAbilityById
        //    && x.Distance2D(GameManager.MousePosition) < 750).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault()
            

        //}
    }
}
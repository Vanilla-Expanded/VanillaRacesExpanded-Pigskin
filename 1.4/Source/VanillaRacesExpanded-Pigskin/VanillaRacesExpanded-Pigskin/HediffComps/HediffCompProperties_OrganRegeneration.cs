using System;
using Verse;
using System.Collections.Generic;

namespace VanillaRacesExpandedPigskin
{
    public class HediffCompProperties_OrganRegeneration : HediffCompProperties
    {

        public IntRange rateInTicks;
        public float healAmount = 1f;
      

        public HediffCompProperties_OrganRegeneration()
        {
            this.compClass = typeof(HediffComp_OrganRegeneration);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaRacesExpandedPigskin
{
    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }

        public static GeneDef VRE_PorkFlesh;

        public static ThingDef Meat_Pig;

        public static ThoughtDef AteHumanlikeMeatDirectCannibal;

        public static XenotypeDef Pigskin;
        public static XenotypeDef VRE_Boarskin;

        public static BodyPartDef Liver;
        public static BodyPartDef Heart;
        public static BodyPartDef Lung;
        public static BodyPartDef Kidney;
        public static BodyPartDef Stomach;
        public static BodyPartDef Brain;

        [MayRequireIdeology]
        public static PreceptDef Cannibalism_Acceptable;
        [MayRequireIdeology]
        public static PreceptDef Cannibalism_Preferred;
        [MayRequireIdeology]
        public static PreceptDef Cannibalism_RequiredStrong;
        [MayRequireIdeology]
        public static PreceptDef Cannibalism_RequiredRavenous;
        [MayRequireIdeology]
        public static PreceptDef VRE_OrganUse_PigskinOnly;

    }
}
using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using RimWorld.Planet;


namespace VanillaRacesExpandedPigskin
{


    [HarmonyPatch(typeof(ThoughtUtility))]
    [HarmonyPatch("GiveThoughtsForPawnOrganHarvested")]
    public static class VanillaRacesExpandedPigskin_ThoughtUtility_GiveThoughtsForPawnOrganHarvested_Patch
    {
        [HarmonyPrefix]
        public static bool SkipIfPigskinAndPrecept(Pawn victim, Pawn billDoer)

        {

            if (victim.HasActiveGene(InternalDefOf.VRE_PorkFlesh) && Current.Game.World.factionManager.OfPlayer?.ideos?.PrimaryIdeo?.GetPrecept(InternalDefOf.VRE_OrganUse_PigskinOnly) != null)
            {
                return false;
            }
            return true;

        }
    }


}

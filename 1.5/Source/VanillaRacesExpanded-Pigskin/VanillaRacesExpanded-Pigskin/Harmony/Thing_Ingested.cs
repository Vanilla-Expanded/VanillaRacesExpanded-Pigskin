using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace VanillaRacesExpandedPigskin
{
    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("Ingested")]
    public static class VanillaRacesExpandedPigskin_Thing_Ingested_Patch
    {
     
        [HarmonyPostfix]
        public static void AddPorkThought(Pawn ingester, Thing __instance)
        {
            if (__instance.def == InternalDefOf.Meat_Pig && ingester.genes?.HasActiveGene(InternalDefOf.VRE_PorkFlesh) == true)
            {              
                    ingester.mindState.lastHumanMeatIngestedTick = Find.TickManager.TicksGame;
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeat, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeatDirect, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);

            }
        }       
    }

    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("AddThoughtsFromIdeo")]
    public static class VanillaRacesExpandedPigskin_FoodUtility_AddThoughtsFromIdeo_Patch
    {

        [HarmonyPrefix]
        public static bool DisableNonCannibalFoodThought(HistoryEventDef eventDef, Pawn ingester, ThingDef foodDef)
        {
            if (foodDef == InternalDefOf.Meat_Pig && ingester.genes?.HasActiveGene(InternalDefOf.VRE_PorkFlesh) == true && eventDef == HistoryEventDefOf.AteNonCannibalFood)
            {
                return false;
            }
            return true;
        }
    }











}


using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace VanillaRacesExpandedPigskin
{
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("ThoughtsFromIngesting")]
    public static class VanillaRacesExpandedPigskin_FoodUtility_ThoughtsFromIngesting_Patch
    {
     
        [HarmonyPostfix]
        public static void AddPorkThought(Pawn ingester, Thing foodSource, ThingDef foodDef)
        {
            if (foodDef == InternalDefOf.Meat_Pig && ingester.genes?.HasGene(InternalDefOf.VRE_PorkFlesh) == true)
            {              
                    ingester.mindState.lastHumanMeatIngestedTick = Find.TickManager.TicksGame;
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeat, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);               
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
            if (foodDef == InternalDefOf.Meat_Pig && ingester.genes?.HasGene(InternalDefOf.VRE_PorkFlesh) == true && eventDef == HistoryEventDefOf.AteNonCannibalFood)
            {
                return false;
            }
            return true;
        }
    }











}


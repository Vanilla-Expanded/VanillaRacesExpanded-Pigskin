
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;

namespace VanillaRacesExpandedPigskin
{
    class HediffComp_OrganRegeneration : HediffComp
    {

        public HediffCompProperties_OrganRegeneration Props
        {
            get
            {
                return (HediffCompProperties_OrganRegeneration)this.props;
            }
        }
        public int tickCounter = 0;
        public const int initialRate = 900000;
        public int rate = initialRate; 


        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterOrganRegen", 0, false);
            Scribe_Values.Look<int>(ref this.rate, "rate", 0, false);


        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            tickCounter++;

            if (tickCounter >= rate)
            {
                Pawn pawn = parent.pawn;

                if (pawn.health != null)
                {
                    List<Hediff_Injury> injuries = GetInjuries(pawn);

                    if (injuries.Count > 0)
                    {


                        Hediff_Injury injury = injuries.RandomElement();
                        injury.Severity = injury.Severity - Props.healAmount;


                    }
                    else
                    {
                        BodyPartRecord bodyPartRecord = FindFirstMissingBodyPart(pawn);
                        if (bodyPartRecord != null) { 
                            
                            pawn.health.RestorePart(bodyPartRecord);
                            int num = (int)pawn.health.hediffSet.GetPartHealth(bodyPartRecord)-1;
                            DamageInfo damageInfo = new DamageInfo(DamageDefOf.Cut, (float)num, 999f, -1f, null, bodyPartRecord, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true);
                            damageInfo.SetAllowDamagePropagation(false);
                            pawn.TakeDamage(damageInfo);



                        }

                    }
                }
                rate = Props.rateInTicks.RandomInRange;
                tickCounter = 0;
            }

        }

        public List<Hediff_Injury> GetInjuries(Pawn pawn)
        {
            List<Hediff_Injury> injuries = new List<Hediff_Injury>();
            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                Hediff_Injury hediff_Injury = pawn.health.hediffSet.hediffs[i] as Hediff_Injury;
                if (hediff_Injury != null)
                {
                    injuries.Add(hediff_Injury);
                }

            }
            return injuries;
        }

        public static BodyPartRecord FindFirstMissingBodyPart(Pawn pawn)
        {
            BodyPartRecord bodyPartRecord = null;
            foreach (Hediff_MissingPart missingPartsCommonAncestor in pawn.health.hediffSet.GetMissingPartsCommonAncestors())
            {
                if ( !pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(missingPartsCommonAncestor.Part) && (bodyPartRecord == null || missingPartsCommonAncestor.Part.coverageAbsWithChildren > bodyPartRecord.coverageAbsWithChildren))
                {
                    bodyPartRecord = missingPartsCommonAncestor.Part;
                }
            }
            return bodyPartRecord;
        }


    }
}

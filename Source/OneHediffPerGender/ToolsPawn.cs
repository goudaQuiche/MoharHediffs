﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using UnityEngine;


namespace OHPG
{
    public static class ToolsPawn
    {
        public static bool IsRaceMember(this Pawn pawn, string raceDefName)
        {
            return pawn?.def.defName == raceDefName;
        }
        public static bool IsHuman(this Pawn pawn)
        {
            return pawn?.def.defName == "Human";
        }

        public static bool HasHediff(this Pawn pawn, HediffDef hediffDef)
        {
            return pawn.health.hediffSet.HasHediff(hediffDef);
        }
        public static bool Has_OHPG(this Pawn pawn)
        {
            return pawn.health.hediffSet.HasHediff(MyDefs.OHPG_HediffDef);
        }
        public static Hediff Get_OHPG(this Pawn pawn)
        {
            return pawn.health.hediffSet.GetFirstHediffOfDef(MyDefs.OHPG_HediffDef);
        }

        public static string PawnResumeString(this Pawn pawn)
        {
            return (pawn?.LabelShort.CapitalizeFirst() +
                    ", " +
                    (int)pawn?.ageTracker?.AgeBiologicalYears + " y/o" +
                    " " + pawn?.gender.ToString() +
                    ", " + pawn?.def?.label + "(" + pawn.kindDef + ")"
                    );
        }
    }
}

﻿using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;
using System.Collections.Generic;

namespace MoharAiJob
{
    public static class FindGrave
    {
        private static bool ValidateGrave(Thing t, Pawn worker, GraveSpecification GS, bool myDebug = false)
        {
            string debugStr = myDebug ? "ValidateGrave - " : "";

            Faction pFaction = worker.Faction;
            Map map = worker.Map;

            if (t.NegligibleThing())
            {
                if (myDebug) Log.Warning(debugStr + "negligible thing");
                return false;
            }

            if (myDebug)
                debugStr += t.ThingID+ " ";
            if (!(t is Building))
            {
                if (myDebug) Log.Warning(debugStr + "is not building");
                return false;
            }

            if (GS.HasEligibleGraves && !GS.eligibleGraves.Contains(t.def))
            {
                if (myDebug) Log.Warning(debugStr + "is not within allowed categories");
                return false;
            }

            if (t is Building_Casket BC)
            {
                if (!BC.HasAnyContents)
                {
                    if (myDebug) Log.Warning(debugStr + "is casket but empty");
                    return false;
                }
                else
                {
                    if(!(BC.ContainedThing is Corpse))
                    {
                        if (myDebug) Log.Warning(debugStr + "is casket but contains no corpse");
                        return false;
                    }
                }
            }
            else { 
                if (myDebug) Log.Warning(debugStr + "is not a casket");
                return false;
            }

            if (map != null && GS.HasReservation && GS.reservation.respectsThingReservation)
            {
                if (myDebug) Log.Warning(debugStr + " checking reservations");

                //foreach (Thing reservedT in map.reservationManager.AllReservedThings()) Log.Warning(reservedT.ThingID);

                LocalTargetInfo LTI = new LocalTargetInfo(t);
                if (!map.reservationManager.ReservationsReadOnly
                    .Where(r => r.Target == LTI)
                    .Where(r => GS.reservation.respectsPawnKind ? r.Claimant.kindDef == worker.kindDef : false)
                    .Where(r => GS.reservation.respectsFaction ? r.Claimant.Faction == pFaction : false)
                    .EnumerableNullOrEmpty())
                {
                    if (myDebug) Log.Warning(debugStr + "is reserved");
                    return false;
                }
                else
                {
                    if (myDebug) Log.Warning(debugStr + " found no reservation for " + t);
                }
            }

            return true;
        }

        public static bool GetClosestCompatibleGrave(this Pawn pawn, GraveSpecification GS, out Thing grave, out Thing corpse, bool myDebug = false)
        {
            grave = null;
            corpse = null;
            if (pawn.NegligiblePawn())
                return false;

            grave = (Building)GenClosest.ClosestThingReachable(
                pawn.Position,
                pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.Grave),
                PathEndMode.ClosestTouch,
                TraverseParms.For(pawn),
                GS.maxDistance,
                delegate (Thing graveBuilding) {
                    return ValidateGrave(graveBuilding, pawn, GS, myDebug);
                }
            );

            if(grave is Building_Casket BC && BC.HasAnyContents && BC.ContainedThing is Corpse newCorpse)
                corpse = newCorpse;

            return grave != null && corpse != null;             
        }


    }
}

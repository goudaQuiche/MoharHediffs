﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using AlienRace;

namespace MoHarRegeneration
{
    public class HealingWithMaxParams : HealingParams
    {
        public float BPMaxHealth = .35f;
        public bool RegenBodyPartChildrenAtOnce = false;
        public string techHediffTag = string.Empty;
    }
}

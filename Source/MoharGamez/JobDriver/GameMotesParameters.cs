﻿using Verse;
using System;
using RimWorld;
using System.Collections.Generic;

namespace MoharGamez
{
    //[StaticConstructorOnStartup]

    public class GameProjectileClass : Thing
    {
        public GameProjectileDef Def
        {
            get
            {
                return def as GameProjectileDef;
            }
        }
    }

    public class GameProjectileDef : ThingDef
    {
        public List<GameProjectile> gameProjectileList;
    }
    
    public class GameProjectile
    {
        public Type driverClass;
        public JobDef jobDef;

        public SkillDef skillDefScaling;
        public IntRange throwInterval;

        public List<ProjectileOption> projectileOptionList;
        public bool Debug = false;
    }

    public class ProjectileOption
    {
        public MoteParameter moteParam = null;
        public MoteParameter shadowMoteParam = null;

        public ImpactMoteParameter impactMoteParam = null;
        public SoundDef throwSound = null;

        public float weight;

        public bool HasMoteProjectiles => moteParam != null;
        public bool HasShadowMoteProjectiles => shadowMoteParam != null;
    }

    public class MoteParameter
    {
        public ThingDef moteDef;
        public FloatRange speed;
        public FloatRange rotation;
    }

    public class ImpactMoteParameter
    {
        public ThingDef moteDef;
        public FloatRange speed;
        public FloatRange angle;
        public FloatRange scale;
        public FloatRange rotationRate;
    }
    /*
    public class EffecterParameter
    {
        public EffecterDef effecterDef;
    }
    */

}
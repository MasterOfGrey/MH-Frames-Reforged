using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;
using Verse.Noise;
using Verse.Grammar;
using RimWorld;
using RimWorld.Planet;

// using System.Reflection;
// using HarmonyLib;

// I am quite sure this is all terrible code etiquette and hardcoded crap but it's modified from a template and only does one startup thing so eh.
namespace MH_Frames_Reforged
{
    [DefOf]
    public class ConfirmationDefOf
    {
        public static LetterDef MH_FR_success_letter;
    }

    public class MyMapComponent : MapComponent
    {
        public MyMapComponent(Map map) : base(map){}
        public override void FinalizeInit()
        {
            Messages.Message("Frames", null, MessageTypeDefOf.PositiveEvent);
            Find.LetterStack.ReceiveLetter("Frames", ConfirmationDefOf.MH_FR_success_letter.description, ConfirmationDefOf.MH_FR_success_letter, null);
        }
    }

    [StaticConstructorOnStartup]
    public static class Start
    {
        static Start()
        {
            Log.Message("Frames Reforged loaded successfully!");
        }
    }

}

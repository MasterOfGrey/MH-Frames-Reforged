using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MH_Frames_Reforged
{
	[StaticConstructorOnStartup]
	public static class MH_FR_HarmonyPatch
	{
		// Define the harmony patch and what it's called.
		static MH_FR_HarmonyPatch()
		{
			// Note: needs to be in Postfix slot of the harmony.Patch method.
			Harmony harmony = new Harmony("ExtraFrames.CaravanCap");
			harmony.Patch(AccessTools.Method(typeof(MassUtility), nameof(MassUtility.Capacity), null, null), null, new HarmonyMethod(typeof(MH_FR_HarmonyPatch), "FrameCarryWeight", null), null, null);

			Log.Message("Frames Reforged patch engaged!");
		}

		// Method to append value of EF_CaravanCapacity applied by the HediffDef to MassUtility.Capacity
		public static void FrameCarryWeight(Pawn p, StringBuilder explanation, ref float __result)
        {
            // 300 tick recache used so that if the status of the pawn's framework changes it takes effect somewhat reasonably, and because someone told me >100 was ok for performance. 
			__result += p.GetStatValue(MH_Frames_Reforged_StatDefOf.EF_CaravanCapacity, true, 300);
        }
     }
}

using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MH_Frames_Reforged
{
	// Mod extension for enabling Vacuum Frame featurres.
	public class FR_VacuumExtension : DefModExtension
	{
		public bool isVacuumFrame = false;
	}
	
	[StaticConstructorOnStartup]
	public static class FRVacuumListUtils
	{
		public static List<HediffDef> vacuumHediffs;
		static FRVacuumListUtils()
		{
			vacuumHediffs = new List<HediffDef>();

			foreach (HediffDef hediffDef in DefDatabase<HediffDef>.AllDefsListForReading)
			{
				if (hediffDef.GetModExtension<FR_VacuumExtension>()?.isVacuumFrame == true)
				{
					FRVacuumListUtils.vacuumHediffs.Add(hediffDef);
				}
			}
			Log.Message($"Frames Reforged SoS2 Content: {vacuumHediffs.Count(h => true)} Vacuum Framework Hediffs found.");
        }
    }


}

using System.Collections.Generic;
using RimWorld;
using Verse;
using UnityEngine;

namespace MH_Frames_Reforged
{
	// Mod extension for enabling Vacuum Frame features.
	public class FR_VacuumExtension : DefModExtension
	{
		// Boolean flag indicating if the vacuum frame effects are enabled for the HediffDef with this extension
		public bool isVacuumFrame = false;
	}
	
	// Utility class for managing vacuum-related hediffs
	[StaticConstructorOnStartup]
	public static class FRVacuumListUtils
	{
		// List to store vacuum-related HediffDefs
		public static List<HediffDef> vacuumHediffs;

		// Static constructor to initialize the vacuumHediffs list
		static FRVacuumListUtils()
		{
			// Initialize the vacuumHediffs list
			vacuumHediffs = new List<HediffDef>();

			// Iterate through all HediffDefs in the DefDatabase
			foreach (HediffDef hediffDef in DefDatabase<HediffDef>.AllDefsListForReading)
			{
				// Check if the current HediffDef has the FR_VacuumExtension mod extension
				if (hediffDef.GetModExtension<FR_VacuumExtension>()?.isVacuumFrame == true)
				{
					// If the vacuum frame is enabled for the current HediffDef, add it to the vacuumHediffs list
					FRVacuumListUtils.vacuumHediffs.Add(hediffDef);
				}
			}

			// Log the number of vacuum framework HediffDefs found
			Log.Message($"Frames Reforged SoS2 Content: {vacuumHediffs.Count(h => true)} Vacuum Framework Hediffs found.");
		}
	}
	
	public class ThoughtWorker_VacuumOverheat : ThoughtWorker
	{
		// Constructor to subscribe to apparel-related events
		public ThoughtWorker_VacuumOverheat()
		{
			// Subscribe to the apparel events
			ApparelChangedWatcher.OnApparelAdded += RecalculateThoughtState;
			ApparelChangedWatcher.OnApparelRemoved += RecalculateThoughtState;
		}

		// Event handler for recalculating the thought state when apparel changes
		private void RecalculateThoughtState(Pawn pawn)
		{
			// Recalculate the thought state based on the pawn's current apparel
			pawn.needs.mood.thoughts.situational.Notify_SituationalThoughtsDirty();
		}

		// Override the CurrentStateInternal method to define the conditions under which a pawn should have the associated thought.
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			bool hasVacuumHediff = false;
			
			// Check if the pawn has any vacuum-related Hediffs
			foreach (Hediff hediff in p.health.hediffSet.hediffs)
			{
				// Check if the pawn has a vacuum-related Hediff
				if (FRVacuumListUtils.vacuumHediffs.Contains(hediff.def))
				{
					hasVacuumHediff = true;
					break; // Exit the loop once a vacuum-related Hediff is found
				}
			}

			// Construct and return the appropriate ThoughtState based on whether the pawn has vacuum-related Hediffs or not
			if (hasVacuumHediff)
			{
				// Now check against apparrel.
				int numUnsafeApparel = 0;
				foreach (Apparel apparel in p.apparel.WornApparel)
				{
					// Check if the apparel has greater than 40% flammability or less than 150% armor rating against heat
					if (apparel.GetStatValue(StatDefOf.Flammability, true) > 0.4f || apparel.GetStatValue(StatDefOf.ArmorRating_Heat, true) < 1.5f)
					{
						numUnsafeApparel++;
					}
				}

				if (numUnsafeApparel == 0)
				{
					// If the pawn does not have any unsafe clothes, exit with ThoughtState Inactive.
					return ThoughtState.Inactive;
				}
				else
				{
					// Define the maximum number of unsafe apparel allowed to map to the highest thought stage
					const int MaxUnsafeApparel = 5;
					// Calculate the stage based on the number of unsafe apparel, with a maximum value of MaxUnsafeApparel
					// Note: This triggers at numUnsafeApparel = 1 but ThoughtStage 1 in the xml is stage 0, hence -1.
					int thoughtStage = Mathf.Min(numUnsafeApparel, MaxUnsafeApparel)-1;
					
					// Return the appropriate ThoughtState based on the calculated thought stage
					return ThoughtState.ActiveAtStage(thoughtStage);
				}
			}
			else
			{
				// If the pawn does not have any vacuum-related HediffDefs, return a ThoughtState object indicating that the pawn should not have the thought.
				return ThoughtState.Inactive;
			}
		}
	}
}

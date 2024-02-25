using Verse;
using RimWorld;
using RimWorld.Planet;

// using System.Reflection;
// using HarmonyLib;

// I am quite sure this is all terrible code etiquette and hardcoded crap but it's modified from a template and only does one startup thing so eh.
namespace MH_Frames_Reforged
{
	// Definition class for LetterDef
	[DefOf]
	public class ConfirmationDefOf
	{
		// Define the LetterDef field
		public static LetterDef MH_FR_success_letter;
	}

	// Custom GameComponent class for managing message display
	public class FramesGameComponent : GameComponent
	{
		// Flag to track if the message has been shown
		private bool FR_shownMessage = false;

		// Constructor - RimWorld will handle the instantiation
		public FramesGameComponent(Game game){}

		// Method called when the game starts
		public override void FinalizeInit()
		{
			// Check if the message has already been shown
			if (!FR_shownMessage)
			{
				// Display the message using the specified LetterDef
				Messages.Message("Frames", null, MessageTypeDefOf.PositiveEvent);
				Find.LetterStack.ReceiveLetter("Frames", ConfirmationDefOf.MH_FR_success_letter.description, ConfirmationDefOf.MH_FR_success_letter, null);

				// Set the flag to indicate that the message has been shown
				FR_shownMessage = true;
			}
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

using System;
using Verse;

public static class ApparelChangedWatcher
{
    // Define events for apparel added and removed
    public static event Action<Pawn> OnApparelAdded;
    public static event Action<Pawn> OnApparelRemoved;

    // Method to trigger the OnApparelAdded event
    public static void NotifyApparelAdded(Pawn pawn)
    {
        OnApparelAdded?.Invoke(pawn);
    }

    // Method to trigger the OnApparelRemoved event
    public static void NotifyApparelRemoved(Pawn pawn)
    {
        OnApparelRemoved?.Invoke(pawn);
    }
}

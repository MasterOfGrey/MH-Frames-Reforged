using RimWorld;

namespace MH_Frames_Reforged
{
    [DefOf]
    public static class MH_Frames_Reforged_StatDefOf
    {
        static MH_Frames_Reforged_StatDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MH_Frames_Reforged_StatDefOf));
        }

        public static StatDef EF_CaravanCapacity;
    }
}

using Verse;

namespace FernyFrantic
{
    public static class Utils
    {
        public static bool FertyStorytellerActive => Find.Storyteller?.def == DefsOf.FernyFrantic;
    }
}
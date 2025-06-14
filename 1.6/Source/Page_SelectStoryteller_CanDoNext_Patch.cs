using HarmonyLib;
using RimWorld;

namespace FernyFrantic
{
    [HarmonyPatch(typeof(Page_SelectStoryteller), "CanDoNext")]
    public static class Page_SelectStoryteller_CanDoNext_Patch
    {
        public static void Postfix(Page_SelectStoryteller __instance)
        {
            if (Utils.FertyStorytellerActive && __instance.next is not Page_FernyFranticOptions)
            {
                var next = __instance.next;
                next.prev = __instance.next = new Page_FernyFranticOptions { prev = __instance, next = next };
            }
            else if (__instance.next is Page_FernyFranticOptions { next: var next })
            {
                __instance.next = next;
            }
        }
    }
}
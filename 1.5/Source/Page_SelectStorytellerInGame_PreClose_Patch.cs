using HarmonyLib;
using RimWorld;
using Verse;

namespace FernyFrantic
{
	[HarmonyPatch(typeof(Page_SelectStorytellerInGame), "PreClose")]
	public static class Page_SelectStorytellerInGame_PreClose_Patch
	{
		public static void Postfix(Page_SelectStorytellerInGame __instance)
		{
			if (Utils.FertyStorytellerActive) Find.WindowStack.Add(new Page_FernyFranticOptions());
		}
	}
}
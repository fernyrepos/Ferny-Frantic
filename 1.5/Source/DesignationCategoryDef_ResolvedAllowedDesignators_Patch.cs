using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FernyFrantic
{
	[HarmonyPatch(typeof(DesignationCategoryDef), "ResolvedAllowedDesignators", MethodType.Getter)]
	public static class DesignationCategoryDef_ResolvedAllowedDesignators_Patch
	{
		public static IEnumerable<Designator> Postfix(IEnumerable<Designator> result)
		{
			foreach (var item in result)
			{
				if (item is Designator_Build build && build.PlacingDef == DefsOf.FernyMonument && Utils.FertyStorytellerActive is false)
				{
					continue;
				}
				else
				{
					yield return item;
				}
			}
		}
	}
}
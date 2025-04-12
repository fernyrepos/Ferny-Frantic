using HarmonyLib;
using Verse;

namespace FernyFrantic
{
	public class FernyFranticMod : Mod
	{
		public FernyFranticMod(ModContentPack pack) : base(pack)
		{
			new Harmony("FernyFranticMod").PatchAll();
		}
	}
}
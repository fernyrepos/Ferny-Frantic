using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FernyFrantic
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class HotSwappableAttribute : Attribute
	{
	}

	[HotSwappable]
	public class Page_FernyFranticOptions : Page
	{
		public static bool tenseMusicMode;
		public Page_FernyFranticOptions()
		{
			if (Current.Game != null && Current.Root is Root_Play && FernyFranticStoryteller.Instance.alwaysTenseMusic.HasValue)
			{
				tenseMusicMode = FernyFranticStoryteller.Instance.alwaysTenseMusic.Value;
			}
		}
		public override void DoWindowContents(Rect inRect)
		{
			var labelRect = new Rect(inRect.x, inRect.y, 400, 24);
			Widgets.Label(labelRect, "Time between explosions (in minutes): ");
			var sliderRect = new Rect(labelRect.xMax, labelRect.y - 3, inRect.width - labelRect.width, labelRect.height);
			Widgets.FloatRange(sliderRect, this.GetHashCode(), ref FernyFranticStoryteller.Instance.timeBetweenExplosions, min: 0.1f, max: 200f);
			labelRect.y += 24;
			sliderRect.y += 24;
			Widgets.Label(labelRect, "Monument influence (chance to hit nearest human): ");
			FernyFranticStoryteller.Instance.monumentInfluence = Widgets.HorizontalSlider(sliderRect,
			 FernyFranticStoryteller.Instance.monumentInfluence, 0f, 1f, middleAlignment: true, label:
				FernyFranticStoryteller.Instance.monumentInfluence.ToStringPercent());
			labelRect.y += 24;
			labelRect.width = inRect.width;
			Widgets.CheckboxLabeled(labelRect, "Only tense music", ref tenseMusicMode);
			if (Current.Game != null && Current.Root is Root_Play && FernyFranticStoryteller.Instance.alwaysTenseMusic.HasValue)
			{
				FernyFranticStoryteller.Instance.alwaysTenseMusic = tenseMusicMode;
				Find.MusicManagerPlay.OverrideDangerMode = tenseMusicMode;
			}
			DoBottomButtons(inRect);
		}
	}
}
using System.Linq;
using RimWorld;
using Verse;

namespace FernyFrantic
{
	public class FernyFranticStoryteller : GameComponent
	{
		public FloatRange timeBetweenExplosions = new FloatRange(20, 40);
		public float monumentInfluence = 0.5f;

		public bool? alwaysTenseMusic;
		public static FernyFranticStoryteller Instance;

		public int tickToHit;
		public FernyFranticStoryteller()
		{
			Instance = this;
		}

		public FernyFranticStoryteller(Game game)
		{
			Instance = this;
		}

		public override void StartedNewGame()
		{
			base.StartedNewGame();
			alwaysTenseMusic = Page_FernyFranticOptions.tenseMusicMode;
			Find.MusicManagerPlay.OverrideDangerMode = Page_FernyFranticOptions.tenseMusicMode;
		}
		public override void GameComponentTick()
		{
			base.GameComponentTick();
			if (Utils.FertyStorytellerActive)
			{
				var curTicks = Find.TickManager.TicksGame;
				if (curTicks > tickToHit)
				{
					tickToHit = curTicks + (int)(timeBetweenExplosions.RandomInRange * 60 * 60);
				}
				else if (curTicks == tickToHit)
				{
					bool pickNearest = Rand.Chance(monumentInfluence);
					var human = GetHumanTarget(pickNearest);
					if (human != null)
					{
						GenExplosion.DoExplosion(human.Position, human.Map, 5f, DamageDefOf.Bomb, null);
						var damage = new DamageInfo(DamageDefOf.Bomb, 99999, 1, hitPart: human.health.hediffSet.GetBrain());
						human.TakeDamage(damage);
					}
				}
			}
		}

		public Pawn GetHumanTarget(bool pickNearest)
		{
			foreach (var map in Find.Maps.Where(x => x.IsPlayerHome).InRandomOrder())
			{
				var mapHumans = map.mapPawns.AllHumanlikeSpawned;
				if (mapHumans.Any())
				{
					if (pickNearest && map.listerThings.ThingsOfDef(DefsOf.FernyMonument).TryRandomElement(out var monument))
					{
						return mapHumans.OrderBy(x => x.Position.DistanceTo(monument.Position)).First();
					}
					else
					{
						return mapHumans.RandomElement();
					}
				}
			}
			return null;
		}
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref timeBetweenExplosions, "timeBetweenExplosions", new FloatRange(20, 40));
			Scribe_Values.Look(ref monumentInfluence, "monumentInfluence", 0.5f);
			Scribe_Values.Look(ref tickToHit, "tickToHit");
			Scribe_Values.Look(ref alwaysTenseMusic, "alwaysTenseMusic");
			if (alwaysTenseMusic.HasValue)
			{
				Find.MusicManagerPlay.OverrideDangerMode = alwaysTenseMusic.Value;
			}
		}
	}
}
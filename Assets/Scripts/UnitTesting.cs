
using System.Collections.Generic;
using Prizes;
using UnityEngine;

	public static class UnitTesting
	{
		private static Dictionary<Prize, int> _prizeUnitTestResult = new Dictionary<Prize, int>();
		
		
		public static void OutputSectorData(WheelSector[] sectors)
		{
			float totalChance = 0;
			for (int i = 0; i < sectors.Length; i++)
			{
				var currSector = sectors[i];
				totalChance += currSector.DropChance;
				Debug.Log($" Sector<color=white> {i}</color> has Prize <color=green>{currSector.Prize.PrizeName}{currSector.Prize.GetUIRewardDisplay(true)}</color> with dropchance of <color=orange>{currSector.DropChance}%</color>  ");
			}
			
			if(totalChance>100 || totalChance < 100)
				Debug.Log($" <color=yellow>[WARNING] Sector drop chance doesnt add up recheck math!!</color>  totalChance={totalChance}  ");
		}

		public static void AddPrize(Prize p)
		{
			
		}

		public static void OutputPrizeResults(float elapsedTime)
		{
			
		}
	}

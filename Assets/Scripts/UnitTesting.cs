/*
* AUTHOR : Steve Datz
* DESC :
*/

using System.Collections.Generic;
using Prizes;
using UnityEngine;

	public static class UnitTesting
	{
		private static readonly Dictionary<string, int> _prizeUnitTestResult = new Dictionary<string, int>();
		/************************************************************************************************************************/
		
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

		public static void KeepTrackOfPrize(Prize p)
		{
			string prizeKey = $"{p.PrizeName}_{p.GetUIRewardDisplay(true)}";
			if (!_prizeUnitTestResult.TryGetValue(prizeKey, out int occurances))
			{
				occurances = 0;
				_prizeUnitTestResult.Add(prizeKey, occurances);
			}

			_prizeUnitTestResult[prizeKey] = ++occurances;
		}

		public static void OutputPrizeResults(float elapsedTime, int totalIterationsRan)
		{
			Debug.Log($"Test {totalIterationsRan} iterations in {elapsedTime}ms");
			int totalOccurances = 0;
			foreach (var prizeKey in _prizeUnitTestResult.Keys)
			{
				int prizeOccurance = _prizeUnitTestResult[prizeKey];
				totalOccurances += prizeOccurance;
				Debug.Log($"<color=green>{prizeKey}</color> occurred [{prizeOccurance} / {totalIterationsRan}]");
			}
			
			if(totalOccurances!=totalIterationsRan)
				Debug.Log($"<color=red>Failed!</color> total occurances does not match iterations!! {totalOccurances} vs {totalIterationsRan} ");
			else
				Debug.Log($"<color=green>Passed!</color> totalOccurances matches totalIterationsRan");
		}
		/************************************************************************************************************************/
	}


using System.Collections.Generic;
using Prizes;
using UnityEngine;

	public static class UnitTesting
	{
		private static Dictionary<Prize, int> _prizeUnitTestResult = new Dictionary<Prize, int>();
		
		
		public static void OutputSectorData(WheelSector[] sectors)
		{
			for (int i = 0; i < sectors.Length; i++)
			{
				var currSector = sectors[i];
				Debug.Log($"==Sector{i} has Prize {currSector.Prize.PrizeName} with dropchance of {currSector.DropChance}%");
			}
		}

		public static void AddPrize(Prize p)
		{
			
		}

		public static void OutputPrizeResults(float elapsedTime)
		{
			
		}
	}

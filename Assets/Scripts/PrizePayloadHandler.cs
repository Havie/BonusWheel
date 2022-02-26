using System;
using System.Reflection;
using Prizes;


public static class PrizePayloadHandler
{
	public static void IssuePrize(Prize prize)
	{
		//NOTE: In a real game this class would be responsible for unpacking the prize and handing it out to whatever other classes
		//In this project its just a glorified record keeper/console logger per instructions

		bool result = prize switch
		{
			LifePrize lifePrize => HandleLifePrize(lifePrize),
			IntPrize intPrize => HandleIntPrize(intPrize),
			_ => throw new ArgumentOutOfRangeException()
		};

	}

	private static bool HandleIntPrize(IntPrize intPrize)
	{
		return true;
	}

	private static bool HandleLifePrize(LifePrize lifePrize)
	{
		return true;
	}
}

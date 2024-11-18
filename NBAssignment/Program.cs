// See https://aka.ms/new-console-template for more information
using NBAssignment.Model;
using System;

class program
{
	static void Main(string[] args)
	{
		Vehicle car = new Car();
		Vehicle	motorbike = new Motorbike();
		Vehicle diplomat = new Diplomat();
		Vehicle em = new Emergency();
		Vehicle military = new Military();
		Vehicle tractor = new Tractor();

		TollCalculator tollCalculator = new TollCalculator();
			DateTime[] passes = {
			//new DateTime(2024, 11, 15, 5, 0, 0), // 0 kr
			new DateTime(2024, 11, 15, 6, 0, 0), // 8 kr
			new DateTime(2024, 11, 15, 6, 15, 0), // 8 kr
			new DateTime(2024, 11, 15, 6, 30, 0), // 13 kr
			new DateTime(2024, 11, 15, 7, 30, 0),  // 18 kr

			//Holidays
			//new datetime(2024, 1, 1), //new year's day 
			//new datetime(2024, 1, 6), //epiphany
			//new datetime(2024, 5, 1), //1 may
			//new datetime(2024, 6, 1), //national day 
			//new datetime(2024, 12, 24), //christmas eve
			//new datetime(2024, 12, 25), //christmas day           
			//new datetime(2024, 12, 26), //boxing day
			//new datetime(2024, 12, 31), //new year eve
			//new datetime(2024, 3, 29), //easter friday
			//new datetime(2024, 3, 30), //easter eve
			//new datetime(2024, 3, 31), //easter day         
			//new datetime(2024, 4, 1), //easter monday
			//new datetime(2024, 5, 19), //whitsun
			//new datetime(2024, 6, 21), //midsummer eve
			//new datetime(2024, 6, 22), //midsummer day
			//new datetime(2024, 11, 2), //all saints day
			//new datetime(2024, 3, 28), //day before easter
			//new datetime(2024, 11, 1) //day before all saints day
		};

		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes)}"); //31
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes)}"); //0
		Console.WriteLine($"Diplomat Toll: {tollCalculator.GetTollFee(diplomat, passes)}"); //0
		Console.WriteLine($"Emergency Toll: {tollCalculator.GetTollFee(em, passes)}"); //0
		Console.WriteLine($"Military Toll: {tollCalculator.GetTollFee(military, passes)}"); //0
		Console.WriteLine($"Tractor Toll: {tollCalculator.GetTollFee(tractor, passes)}"); //0
	}
}



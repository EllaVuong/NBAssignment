// See https://aka.ms/new-console-template for more information
using NBAssignment.Model;
using System;

class program
{
	static void Main(string[] args)
	{
		Vehicle car = new Car();
		Vehicle	motorbike = new Motorbike();

		TollCalculator tollCalculator = new TollCalculator();
			DateTime[] passes = {
			//new DateTime(2024, 11, 15, 5, 0, 0), // 0 kr
			new DateTime(2024, 11, 15, 6, 0, 0), // 8 kr
			new DateTime(2024, 11, 15, 6, 15, 0), // 8 kr
            new DateTime(2024, 11, 15, 6, 30, 0), // 13 kr
            new DateTime(2024, 11, 15, 7, 30, 0),  // 18 kr
			new DateTime(2024, 12, 24, 6, 0, 0) // christmas eve
		};

		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes)}"); //31
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes)}"); //0
	}
}



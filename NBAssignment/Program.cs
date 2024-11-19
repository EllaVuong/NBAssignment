using NBAssignment.Model;

class program
{
	private const int Year = 2024;

	static void Main(string[] args)
	{
		Vehicle car = new Car();
		Vehicle	motorbike = new Motorbike();
		Vehicle diplomat = new Diplomat();
		Vehicle em = new Emergency();
		Vehicle military = new Military();
		Vehicle tractor = new Tractor();

		TollCalculator tollCalculator = new TollCalculator();

		DateTime[] passes1 = {
			new DateTime(Year, 11, 23), //Saturday
			new DateTime(Year, 11, 24), //Sunday
			new DateTime(Year, 1, 1), //new year's day 
			new DateTime(Year, 1, 6), //epiphany
			new DateTime(Year, 5, 1), //1 may
			new DateTime(Year, 6, 1), //national day 
			new DateTime(Year, 12, 24), //christmas eve
			new DateTime(Year, 12, 25), //christmas day           
			new DateTime(Year, 12, 26), //boxing day
			new DateTime(Year, 12, 31), //new year eve
			new DateTime(Year, 3, 29), //easter friday
			new DateTime(Year, 3, 30), //easter eve
			new DateTime(Year, 3, 31), //easter day         
			new DateTime(Year, 4, 1), //easter monday
			new DateTime(Year, 5, 19), //whitsun
			new DateTime(Year, 6, 21), //midsummer eve
			new DateTime(Year, 6, 22), //midsummer day
			new DateTime(Year, 11, 2), //all saints day
			new DateTime(Year, 3, 28), //day before easter
			new DateTime(Year, 11, 1) //day before all saints day
		};
		Console.WriteLine("----Holiday Test----");
		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes1)}"); //0
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes1)}"); //0
		Console.WriteLine($"Diplomat Toll: {tollCalculator.GetTollFee(diplomat, passes1)}"); //0
		Console.WriteLine($"Emergency Toll: {tollCalculator.GetTollFee(em, passes1)}"); //0
		Console.WriteLine($"Military Toll: {tollCalculator.GetTollFee(military, passes1)}"); //0
		Console.WriteLine($"Tractor Toll: {tollCalculator.GetTollFee(tractor, passes1)}"); //0

		DateTime[] passes2 = {
			new DateTime(Year, 11, 18, 10, 0, 0), //8
			new DateTime(Year, 11, 18, 10, 30, 0), //8
			new DateTime(Year, 11, 18, 11, 0, 0), //8
		};
		Console.WriteLine("-----Multi passes within 60 min Test------");
		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes2)}"); //8
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes2)}"); //0
		Console.WriteLine($"Diplomat Toll: {tollCalculator.GetTollFee(diplomat, passes2)}"); //0
		Console.WriteLine($"Emergency Toll: {tollCalculator.GetTollFee(em, passes2)}"); //0
		Console.WriteLine($"Military Toll: {tollCalculator.GetTollFee(military, passes2)}"); //0
		Console.WriteLine($"Tractor Toll: {tollCalculator.GetTollFee(tractor, passes2)}"); //0


		DateTime[] passes3 = {
			new DateTime(Year, 11, 18, 9, 0, 0), //8
			new DateTime(Year, 11, 18, 11, 0, 0), //8
			new DateTime(Year, 11, 18, 14, 20, 0) //8
		};
		Console.WriteLine("-----Passes beyond 60 min Test------");
		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes3)}"); //24
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes3)}"); //0
		Console.WriteLine($"Diplomat Toll: {tollCalculator.GetTollFee(diplomat, passes3)}"); //0
		Console.WriteLine($"Emergency Toll: {tollCalculator.GetTollFee(em, passes3)}"); //0
		Console.WriteLine($"Military Toll: {tollCalculator.GetTollFee(military, passes3)}"); //0
		Console.WriteLine($"Tractor Toll: {tollCalculator.GetTollFee(tractor, passes3)}"); //0

		DateTime[] passes4 = {
			new DateTime(Year, 11, 18, 6, 0, 0), //8
			new DateTime(Year, 11, 18, 6, 30, 0), //13
			new DateTime(Year, 11, 18, 7, 0, 0), //18
			new DateTime(Year, 11, 18, 7, 30, 0), //18
			new DateTime(Year, 11, 18, 8, 0, 0), //13
			new DateTime(Year, 11, 18, 8, 30, 0), //8
			new DateTime(Year, 11, 18, 9, 0, 0), //8
			new DateTime(Year, 11, 18, 9, 30, 0), //8
			new DateTime(Year, 11, 18, 10, 0, 0), //8
			new DateTime(Year, 11, 18, 10, 30, 0), //8
			new DateTime(Year, 11, 18, 11, 0, 0), //8
			new DateTime(Year, 11, 18, 11, 30, 0), //8
			new DateTime(Year, 11, 18, 12, 0, 0), //8
			new DateTime(Year, 11, 18, 12, 30, 0), //8
			new DateTime(Year, 11, 18, 13, 0, 0), //8
			new DateTime(Year, 11, 18, 14, 30, 0), //8
		};
		Console.WriteLine("-----Max daily cap Test------");
		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes4)}"); //60 max cap
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes4)}"); //0
		Console.WriteLine($"Diplomat Toll: {tollCalculator.GetTollFee(diplomat, passes4)}"); //0
		Console.WriteLine($"Emergency Toll: {tollCalculator.GetTollFee(em, passes4)}"); //0
		Console.WriteLine($"Military Toll: {tollCalculator.GetTollFee(military, passes4)}"); //0
		Console.WriteLine($"Tractor Toll: {tollCalculator.GetTollFee(tractor, passes4)}"); //0

		DateTime[] passes5 = {
			new DateTime(Year, 11, 18, 8, 29, 0), //13
			new DateTime(Year, 11, 18, 8, 30, 0), //8
			new DateTime(Year, 11, 18, 14, 59, 0), //8
			new DateTime(Year, 11, 18, 15, 29, 0) //13
		};
		Console.WriteLine("-----Time Boundry test------");
		Console.WriteLine($"Car Toll: {tollCalculator.GetTollFee(car, passes5)}"); //26
		Console.WriteLine($"Motorbike Toll: {tollCalculator.GetTollFee(motorbike, passes5)}"); //0
		Console.WriteLine($"Diplomat Toll: {tollCalculator.GetTollFee(diplomat, passes5)}"); //0
		Console.WriteLine($"Emergency Toll: {tollCalculator.GetTollFee(em, passes5)}"); //0
		Console.WriteLine($"Military Toll: {tollCalculator.GetTollFee(military, passes5)}"); //0
		Console.WriteLine($"Tractor Toll: {tollCalculator.GetTollFee(tractor, passes5)}"); //0
	}
}



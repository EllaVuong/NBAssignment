using NBAssignment.Model;
using System;
using System.Globalization;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    const int MAX_DAILY_FEE = 60;
    const int TOLL_FREE_WINDOW = 60;

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
		DateTime intervalStart = dates[0];
		int totalFee = 0;
		int intervalFee = GetTollFee(intervalStart, vehicle);

		foreach (DateTime vehiclePass in dates)
		{
			int currentFee = GetTollFee(vehiclePass, vehicle);

			//long diffInMillies = date.Millisecond - intervalStart.Millisecond;
			//long minutes = diffInMillies / 1000 / 60;
			TimeSpan timeDiff = vehiclePass - intervalStart;
            long minutes = (long)timeDiff.TotalMinutes;

			if (minutes <= TOLL_FREE_WINDOW)
			{
				if (totalFee > 0) 
                    totalFee -= intervalFee;
				if (currentFee >= intervalFee) 
                    intervalFee = currentFee;
				totalFee += intervalFee;
			}
			else
			{
				totalFee += currentFee;
			}
		}
		//if (totalFee > MAX_DAILY_FEE) totalFee = MAX_DAILY_FEE;
		//return totalFee;
		return Math.Min(totalFee, MAX_DAILY_FEE);
	}

	private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
	{
		if (!IsTollFreeDate(date) && !IsTollFreeVehicle(vehicle))
		{
			int hour = date.Hour;
			int minute = date.Minute;

			if (hour == 6 && minute >= 0 && minute <= 29) return 8;
			else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
			else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
			else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
			else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
			else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
			else if ((hour == 15 && minute >= 0) || (hour == 16 && minute <= 59)) return 18;
			else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
			else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
			else return 0;
		}
		return 0; 
	}

	private Boolean IsTollFreeDate(DateTime date)
    {
        //int year = date.Year;
        //int month = date.Month;
        //int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) 
            return true;

        if (date.Month == 7) //July
            return true;

        return IsPublicHoliday(date);
        //if (year == 2013)
        //{
        //    if (month == 1 && day == 1 ||
        //        month == 3 && (day == 28 || day == 29) ||
        //        month == 4 && (day == 1 || day == 30) ||
        //        month == 5 && (day == 1 || day == 8 || day == 9) ||
        //        month == 6 && (day == 5 || day == 6 || day == 21) ||
        //        month == 7 ||
        //        month == 11 && day == 1 ||
        //        month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }

    private Boolean IsPublicHoliday(DateTime date)
    {
        var publicHolidays = new List<DateTime>
        {
            new DateTime(date.Year, 1, 1), //New Year 
            new DateTime(date.Year, 1, 6), //Thirteenth day of Christmas
            new DateTime(date.Year, 1, 5), // 1 May
            new DateTime(date.Year, 6, 1), // National day 
            new DateTime(date.Year, 12, 24), //Christmas eve
            new DateTime(date.Year, 12, 25), //Christmas day           
            new DateTime(date.Year, 12, 26), //Boxing day
            new DateTime(date.Year, 12, 31) //New Year eve
		};
        return publicHolidays.Contains(date.Date);
    }

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}
﻿using NBAssignment.Model;
using System;
using System.Globalization;

/* 
 * Syftet till ändringarna i koden är att det i slutändan ska vara lättläsligt, lätt att underhålla och återanvända och
 * att implementera ändringar/ skalbar.
 * 
 * Detta är anledningen till att jag har försökt att dela upp logiken
 * i olika metoder och försökt att få varje metod att göra en grej. Jag har försökt att undervika att använda
 * hårdkoda värden, exempel i metoden IsTollFreeDate kollade man om året är 2013 och andra "hårdkodade datum",
 * som röda dagar infaller. Detta fungerar tyvärr inte lika bra med röda dagar som är rörliga såsom Påsk. 
 * 
 * Jag har också valt att byta namn på vissa variabler, exempel date --> vehiclePass, för att göra koden mer läsbart.
 * Gäller också namnkonvention och att jag har valt att lägga till konstanter för läsbarheten (+ lättare att ändra 
 * de värden i framtiden ifall det skulle behövas). 
 * 
 * Jag har lagt till lite null-check:ar och InvalidOperationException för att programmet inte ska krascha ifall 
 * något går snett, utan att den ska fortsätta/ visa felmeddelanden istället.
 */

public class TollCalculator
{
	const int MAX_DAILY_FEE = 60;
	const int TOLL_FREE_WINDOW_FOR_THE_DAY = 60;

	/**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
	public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
		if (vehicle == null || dates == null)
            return 0;
        
        DateTime intervalStart = dates[0];
		int totalFee = 0;
		int intervalFee = GetTollHourFee(intervalStart, vehicle);

		foreach (DateTime vehiclePass in dates)
		{
			int currentFee = GetTollHourFee(vehiclePass, vehicle);
			TimeSpan timeDiff = vehiclePass - intervalStart;
            long minutes = (long)timeDiff.TotalMinutes;

			if (minutes <= TOLL_FREE_WINDOW_FOR_THE_DAY) {
                intervalFee = Math.Max(intervalFee, currentFee);
            }
			else {
                totalFee += intervalFee;
                intervalStart = vehiclePass;
                intervalFee = currentFee;
			}
            if (totalFee >= MAX_DAILY_FEE)
                break;
        }
		totalFee += intervalFee;
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

    public int GetTollHourFee(DateTime date, Vehicle vehicle)
	{
		if (!IsTollFreeDate(date) && !IsTollFreeVehicle(vehicle))
		{
			int hour = date.Hour;
			int minute = date.Minute;

			if (hour == 6 && minute < 30) return 8;
			else if (hour == 6 && minute >= 30) return 13;
			else if (hour == 7) return 18;
			else if (hour == 8 && minute < 30) return 13;
			else if ((hour == 8 && minute >= 30) || (hour >= 9 && hour < 15)) return 8;
			else if (hour == 15 && minute < 30) return 13;
			else if (hour == 15 || hour == 16) return 18;
			else if (hour == 17) return 13;
			else if (hour == 18 && minute < 30) return 8;
			else return 0;
		}
		return 0; 
	}

    private Boolean IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            return true;
        
		if (date.Month == 7) //July
            return true;

        if (IsDayBeforeHoliday(date))
            return true;
        
        return IsPublicHoliday(date);
	}

    private Boolean IsPublicHoliday(DateTime date)
    {
        var publicHoliday = GetPublicHolidays(date.Year);
        return publicHoliday.Contains(date.Date);
    }

    private List<DateTime> GetPublicHolidays(int year)
    {
        var publicHolidays = new List<DateTime>
        {
            new DateTime(year, 1, 1), //New Year's day 
            new DateTime(year, 1, 6), //Epiphany
            new DateTime(year, 5, 1), //1 May
            new DateTime(year, 6, 1), //National day 
            new DateTime(year, 12, 24), //Christmas eve
            new DateTime(year, 12, 25), //Christmas day           
            new DateTime(year, 12, 26), //Boxing day
            new DateTime(year, 12, 31) //New Year eve
		};

        //Easter
        DateTime goodFriday = GetEasterSunday(year).AddDays(-2);
        DateTime easterEve = GetEasterSunday(year).AddDays(-1);
        DateTime easterMonday = GetEasterSunday(year).AddDays(1);
        publicHolidays.Add(goodFriday);
        publicHolidays.Add(easterEve);
        publicHolidays.Add(easterMonday);
        publicHolidays.Add(GetEasterSunday(year));

        //Ascension Day/ Kristi Himmelfärds dag
        //6th thursday/ 40 days after easter sunday
        DateTime ascensionDay = GetEasterSunday(year).AddDays(39);
        publicHolidays.Add(ascensionDay);

        //Whitsun/Pingstdagen
        //7 weeks after Easter Sunday
        DateTime whitSunday = GetEasterSunday(year).AddDays(49);
        publicHolidays.Add(whitSunday);

        //Midsummer eve + Midsummer day
        DateTime midsummerEve = GetMidsummerDay(year).AddDays(-1);
        publicHolidays.Add(midsummerEve);
		publicHolidays.Add(GetMidsummerDay(year));

        //All Saints day
        publicHolidays.Add(GetAllSaintsDay(year));

		return publicHolidays;
    }

    private DateTime GetEasterSunday(int year)
    {
		int a = year % 19;
		int b = year / 100;
		int c = year % 100;
		int d = b / 4;
		int e = b % 4;
		int f = (b + 8) / 25;
		int g = (b - f + 1) / 3;
		int h = (19 * a + b - d - g + 15) % 30;
		int i = c / 4;
		int k = c % 4;
		int l = (32 + 2 * e + 2 * i - h - k) % 7;
		int m = (a + 11 * h + 22 * l) / 451;
		int month = (h + l - 7 * m + 114) / 31;
		int day = ((h + l - 7 * m + 114) % 31) + 1;

		return new DateTime(year, month, day);
	}

    private DateTime GetMidsummerDay(int year)
    {
		//the saturday between 20 june - 26 june
		DateTime june20 = new DateTime(year, 6, 20);
        for (int i = 0; i <= 6; i++) { 
            if (june20.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                return june20.AddDays(i);
        }
        throw new InvalidOperationException("Cannot calculate the Midsummer Day.");
    }

    private DateTime GetAllSaintsDay(int year)
    {
		//the saturday between 31 oct - 5 nov
		DateTime oct31 = new DateTime(year, 10, 31);
        for (int i = 0; i <= 6; i++) {
            if (oct31.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                return oct31.AddDays(i);
        }
        throw new InvalidOperationException("Cannont calculate the All Saints Day.");
    }

    private Boolean IsDayBeforeHoliday(DateTime date)
    {
        var publicHolidays = GetPublicHolidays(date.Year);
        foreach (var holiday in publicHolidays) {
            if (holiday.AddDays(-1) == date.Date)
                return true;
        }
        return false;
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
using System;

namespace Date
{
    class Date
    {
        int day, month, year;
        static int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        //Instantiate object  with int parameters in form of day,month,year
        public Date(int day, int month, int year)
        {
            if(IsValidDate(day,month,year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
            else
            {
                this.day = 1;
                this.month = 1;
                this.year = 1;
                Console.WriteLine("Invalid Date Entered -> Changed to 1/1/1");
            }
        }

        /*Instantiate object with string paramter of date followed by a seperator string
         Date string can be feed as dd-mm-yyyy with seperator seperating these dates values in string
        */
        public Date(String date, char seperator='/')
        {
            try
            {
                String[] dateString = date.Split(seperator);
                int day = int.Parse(dateString[0]);
                int month = int.Parse(dateString[1]);
                int year = int.Parse(dateString[2]);
                if (IsValidDate(day, month, year))
                {
                    this.day = day;
                    this.month = month;
                    this.year = year;
                }
                else
                {
                    this.day = 1;
                    this.month = 1;
                    this.year = 1;
                    Console.WriteLine("Invalid Date Entered -> Changed to 1/1/1");
                }
            }
            catch
            {
                Console.WriteLine("ERROR:: Cannot extract Day ,Month and Year from Date String. Please Check Seperator");
            }
        }

        public int Day
        {
            get
            {
                return this.day;
            }
            set
            {
                this.day = value;
            }
        }
        public int Month
        {
            get
            {
                return this.month;
            }
            set
            {
                this.month = value;
            }
        }
        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
            }
        }

        /*
         This method is magic.....
         it makes the dates equivalent to each other in terms of days and month ie..
         if  date 1-  " 12-05-1998 "
         date 2-  "10-1-2019" ==> "12-05-2019"
         the date 2 is converted to equivalent of day 1 in terms of month and date and it will return
         the redundant value which is added or subtracted from date 2 to make it equivalent which will be used
         later in calculation
        */
        private static int FindMagicNumber(Date start,Date end)
        {
            /* if date month is equal to end date we will simply return difference of dates
             ex- date= 12-05-1998 and end= 10-05-2018
             since month in both dates is same so we will return difference of dates
             ie.. 12 - 10 = 2
             now end becomes 12-05-2018
            */
            if (start.Month == end.month)
            {
                return end.day - start.day;
            }
            else if (start.month > end.month)
            {
                if (CheckLeapYear(end.year))
                {
                    monthDays[1] = 29;
                }
                int a, b, number = 0;
                a = end.month;
                b = start.month;
                while (a < (b - 1))
                {
                    number += monthDays[a];
                    a += 1;
                }
                number += monthDays[end.month - 1] - end.day;
                number += start.day;
                return number;
            }
            else if (start.month < end.month)
            {
                if (CheckLeapYear(start.year))
                {
                    monthDays[1] = 29;
                }
                int a, b, number = 0;
                a = start.month;
                b = end.month;
                while (a < (b - 1))
                {
                    number += monthDays[a];
                    a += 1;
                }
                number += monthDays[start.month - 1] - start.day;
                number += end.day;
                return number;
            }
            return -1;
        }

        /*Calculate the next hundredth year ie.. if year in date is
                           1807
        then the next hundredth year will be   1900
        */
        private static int HundredYear(int year)
        {
            return (int)(Math.Floor((year + 99.0f) / 100) * 100);
        }

        //Custom roundoff function for the purpose because builtin are not functioning as supposed
        private static int RoundOff(double number)
        {
            double diff = number - Math.Floor(number);
            if (diff < 0.5)
                return (int)Math.Floor(number);
            return (int)Math.Ceiling(number);
        }

        //Check for leap year
        public static Boolean CheckLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        /*
        Calculates total leap year b/w range of years with minimum use of iterative statements
        TRY TO MAKE IT FAST!!!
        */
        public static int LeapYearBetween(int start, int end)
        {
            float diff = end - start;
            if (diff < 0)
                return 0;
            else if (diff == 0)
            {
                if (CheckLeapYear(start) && CheckLeapYear(end))
                    return 1;
                else
                    return 0;
            }
            else if (diff == 1)
            {
                if (CheckLeapYear(start) || CheckLeapYear(end))
                    return 1;
                else
                    return 0;
            }
            int leap = RoundOff(diff / 4);
            int i = HundredYear(start);
            while (i <= end)
            {
                if (!CheckLeapYear(i))
                    leap -= 1;
                i += 100;
            }
            if (CheckLeapYear(start) && CheckLeapYear(end))
                leap += 1;
            return leap;
        }
        public static int LeapYearBetween(Date start, Date end)
        {
            float diff = end.Year - start.Year;
            if (diff < 0)
                return 0;
            else if (diff == 0)
            {
                if (CheckLeapYear(start.Year) && CheckLeapYear(end.Year))
                    return 1;
                else
                    return 0;
            }
            else if (diff == 1)
            {
                if (CheckLeapYear(start.Year) || CheckLeapYear(end.Year))
                    return 1;
                else
                    return 0;
            }
            int leap = RoundOff(diff / 4);
            int i = HundredYear(start.Year);
            while (i <= end.Year)
            {
                if (!CheckLeapYear(i))
                    leap -= 1;
                i += 100;
            }
            if (CheckLeapYear(start.Year) && CheckLeapYear(end.Year))
                leap += 1;
            return leap;
        }

        /*Calculate date b/w days with param Date toDate and Boolean include_startday which tell to
        include start day or not while calculating days between dates
        */
        public static int DaysBetweenDate(Date startDate,Date endDate, Boolean include_startday=true)
        {
            if (startDate < endDate)
            {
                int diff, leapFactor, magic;
                diff = endDate.year - startDate.Year;
                leapFactor = LeapYearBetween(startDate.Year, endDate.year);
                magic = FindMagicNumber(startDate,endDate);
                if (include_startday)
                    magic += 1;
                if (endDate.year == startDate.Year)
                {
                    return magic + leapFactor;
                }
                else if (endDate.month < startDate.Month)
                {
                    // subtract the redundant magic number which was added to make it equivalent to date
                    return (diff * 365) + leapFactor - magic;
                }
                else if (endDate.month >= startDate.Month)
                {
                    // add the redundant magic number which was subtracted to make it equivalent to date
                    return (diff * 365) + leapFactor + magic;
                }
            }
            return -1;
        }
        
        //Check if date is greater(comes after) date entered
        public static Boolean IsGreaterDate(Date date,Date fromDate)
        {
            if (date.year > fromDate.year)
                return true;
            else
            {
                if (date.year == fromDate.year && date.month > fromDate.month)
                    return true;
                else
                {
                    if (date.year == fromDate.year && date.month == fromDate.month && date.day > fromDate.day)
                        return true;
                }
            }
            return false;
        }
        public static Boolean IsLesserDate(Date date, Date fromDate)
        {
            return !IsGreaterDate(date, fromDate);
        }

        //Check if date is in valid format or not
        public static Boolean IsValidDate(Date d)
        {
            if (d.month < 13 || d.month > 0)
                return true;
            if (d.year > 0)
                return true;
            if (d.day < 32)
            {
                if (d.day == monthDays[d.month])
                    return true;
            }
            return false;
        }
        public static Boolean IsValidDate(int day,int month,int year)
        {
            if (month < 13 || month > 0 && year > 0)
            {
                if (day <= monthDays[month] || day > 0)
                {
                    return true;
                }
            }
            return false;
        }

        // Operator Overloading
        public override string ToString() 
        {
            return $"{this.Day}/{this.Month}/{this.Year}";
        }
        public static int operator -(Date startDate, Date endDate)
        {
            return DaysBetweenDate(startDate,endDate);
        }
        public static bool operator >(Date date, Date fromDate)
        {
            return IsGreaterDate(date, fromDate);
        }
        public static bool operator <(Date date, Date fromDate)
        {
            return !IsGreaterDate(date, fromDate);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Date Module Testing\n\n");
            Date d1 = new Date("12/05/1998");
            Date d2 = new Date("12/10/2019");
            int days = d1 - d2;
            int leap_years = Date.LeapYearBetween(d1, d2);
            Console.WriteLine($"Number of Days Spend in Earth are : {days}\n");
            Console.WriteLine($"Is {d1} is greater than {d2} : {d1>d2}");
            Console.WriteLine($"Number of Leap Years between {d1} and {d2} are : {leap_years}");
        }
    }
}

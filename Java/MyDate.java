package java_date;
/**
 *
 * @author Tarun Bisht
 */

 //Custom Date Class
public class MyDate
{
    int day,month,year;
    int[] monthDays={31,28,31,30,31,30,31,31,30,31,30,31};
    //Instantiate object  with integer parameters in form of day,month,year
    public MyDate(int day,int month,int year)
    {
        this.day=day;
        this.month=month;
        this.year=year;
    }
    /*Instantiate object with string paramter of date followed by a seperator string
     Date string can be feed as yyyy-mm-dd with seperator seperating these dates values in string
    */
    public MyDate(String date,String seperator)
    {
        String[] dateString =date.split(seperator);
        this.day=Integer.parseInt(dateString[2]);
        this.month=Integer.parseInt(dateString[1]);
        this.year=Integer.parseInt(dateString[0]);
    }
    //Return Year of date
    public int Year()
    {
        return this.year;
    }
    //Return Month of date
    public int Month()
    {
        return this.month;
    }
    //Return Day of date
    public int Day()
    {
        return this.day;
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
    private int FindMagicNumber(MyDate end)
    {
        /* if date month is equal to end date we will simply return difference of dates
         ex- date= 12-05-1998 and end= 10-05-2018
         since month in both dates is same so we will return difference of dates
         ie.. 12 - 10 = 2
         now end becomes 12-05-2018
        */
        if(this.month==end.month)
        {
            return end.day - this.day;
        }
        else if(this.month>end.month)
        {
            if(CheckLeapYear(end.year))
            {
                this.monthDays[1]=29;
            }
            int a,b,number=0;
            a=end.month;
            b=this.month;
            while(a<(b-1))
            {
                number+=this.monthDays[a];
                a+=1;
            }
            number+=this.monthDays[end.month-1]-end.day;
            number+=this.day;
            return number;
        }
        else if(this.month<end.month)
        {
            if(CheckLeapYear(this.year))
            {
                this.monthDays[1]=29;
            }
            int a,b,number=0;
            a=this.month;
            b=end.month;
            while(a<(b-1))
            {
                number+=this.monthDays[a];
                a+=1;
            }
            number+=this.monthDays[this.month-1]-this.day;
            number+=end.day;
            return number;
        }
        return -1;
    }
    /*
    Calculates total leap year b/w range of years with minimum use of iterative statements
    TRY TO MAKE IT FAST!!!
    */
    private int TotalLeapYear(int start,int end)
    {
        float diff=end-start;
        if (diff<0)
                return 0;
        else if(diff==0)
        {
            if (CheckLeapYear(start) && CheckLeapYear(end))
                return 1;
            else
                return 0;
        }
        else if(diff==1)
        {
            if (CheckLeapYear(start) || CheckLeapYear(end))
                return 1;
            else
                return 0;
        }
        int leap= roundoff(diff/4);
        int i=HundredYear(start);
        while (i<=end)
        {
            if (!CheckLeapYear(i))
                leap-=1;
            i+=100;
        }
        if (CheckLeapYear(start) && CheckLeapYear(end))
            leap+=1;
        System.out.println("leap factor: "+leap);
        return leap;
    }
    /*Calculate the next hundredth year ie.. if year in date is
                       1807
    then the next hundredth year will be   1900
    */
    private int HundredYear(int year)
    {
        return (int) (Math.floor((year+99)/100)*100);
    }
    //Custom roundoff function for the purpose because builtin are not functioning as supposed
    private int roundoff(double number)
    {
        double diff= number-Math.floor(number);
        if(diff<0.5)
            return (int) Math.floor(number);
        return (int) Math.ceil(number);
    }
    //Validate if entered start date < end date
    private boolean ValidateDate(MyDate end)
    {
        return end.year>=this.year;
    }
    //Check for leap year
    public boolean CheckLeapYear(int year)
    {
        return (year%4==0 && year%100!=0) || year%400==0;
    }
    //Calculate date b/w days with param MyDate object
    public int DaysBetweenDate(MyDate toDate)
    {
        if(ValidateDate(toDate))
        {
            int diff,leapFactor,magic;
            diff=toDate.year-this.year;
            leapFactor=TotalLeapYear(this.year,toDate.year);
            magic= FindMagicNumber(toDate);
            if(toDate.year == this.year)
            {
                return magic+leapFactor;
            }
            else if(toDate.month < this.month)
            {
                return (diff*365)+leapFactor-magic;
            }
            else if(toDate.month >= this.month)
            {
                return (diff*365)+leapFactor+magic;
            }
        }
        return -1;
    }
    /*Calculate date b/w days with param Mydate and boolean startday which tell to
    include start day or not while calculating days between dates
    */
    public int DaysBetweenDate(MyDate toDate,boolean include_startday)
    {
        if(ValidateDate(toDate))
        {
            int diff,leapFactor,magic;
            diff=toDate.year-this.year;
            leapFactor=TotalLeapYear(this.year,toDate.year);
            magic= FindMagicNumber(toDate);
            if(include_startday)
                magic+=1;
            if(toDate.year == this.year)
            {
                return magic+leapFactor;
            }
            else if(toDate.month < this.month)
            {
              // subtract the redundant magic number which was added to make it equivalent to date
                return (diff*365)+leapFactor-magic;
            }
            else if(toDate.month >= this.month)
            {
              // add the redundant magic number which was subtracted to make it equivalent to date
                return (diff*365)+leapFactor+magic;
            }
        }
        return -1;
    }
    //check if date month came after or before month index entered
    public boolean isGreaterMonth(int monthindex)
    {
        return monthindex<this.month;
    }
    //Check if date is greater(comes after) date entered
    public boolean isGreaterDateThan(MyDate date)
    {
        if(this.year>date.year)
            return true;
        else
        {
            if(this.year==date.year && this.month>date.month)
                return true;
            else
            {
                if(this.year==date.year && this.month == date.month && this.day>date.day)
                    return true;
            }
        }
        return false;
    }
    //Check if date is in valid format or not
    public boolean isValidDate()
    {
        if(this.month<13 || this.month>0)
            return true;
        if(this.year>0)
            return true;
        if(this.day<32)
        {
            if(this.day==monthDays[this.month])
                return true;
        }
        return false;
    }
}

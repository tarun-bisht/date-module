import java.util.Scanner;
import java_date.MyDate;
/**
 *
 * @author Tarun Bisht
 */
public class Test
{
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args)
    {
        System.out.println("***HELLO WORLD***");
        // TODO code application logic here
        Scanner s=new Scanner(System.in);//enter start date
        System.out.println("Enter start Date String: ");
        String date1=s.next();
        System.out.println("Enter end Date String: ");//enter end date
        String date2=s.next();
        MyDate start=new MyDate(date1,"-");
        MyDate end=new MyDate(date2,"-");
        //Calculate days
        int days=start.TotalDaysBetweenDate(start, end,false);
        System.out.println("number of days: "+days);
    }
}

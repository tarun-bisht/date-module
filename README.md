# Date Module
* Date module Recreation.
* All the math stuff behind the scene is pretty simple algebra.
* The algorithm is result of my analysis of some different dates and finding days between them quickly as possible.
# How it Works
* Consider dates: 12-05-1998  and  10-2-2019
* To calculate number of days between these dates.
* First we will make both the dates equivalent in month and day by adding or subtracting some number of days from a date. 
* In our example number of days are needed to be added or subtracting from the two 10-02-xxxx and 12-05-xxxx, make them equivalent.
* Number of days which are added to 10-02-2019 are 91 which makes it 12-05-2019 ie.. equivalent to 12-05=1998 in terms of days and month. 
* Now we will subtract years of both dates to get difference in years. ie.. 2019 - 1998 =  21 years
* We can convert years in days while taking care of leap years and finally we will add 91 to get total days
#Implementation
* ### [C#](https://github.com/tarun-bisht/date-module/tree/master/C%23)
* ### [JAVA](https://github.com/tarun-bisht/date-module/tree/master/Java)

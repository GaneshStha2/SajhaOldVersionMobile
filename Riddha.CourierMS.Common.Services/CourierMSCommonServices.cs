using System;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Common.Services
{
    public class CourierMSCommonServices
    {
        public async Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await Task.Delay(milisec);
            actionToExecute();
        }


        public TimeSpan StringToTimeSpan(string time)
        {
            string[] hourinfirstindexandminuteandampminsecondindex = time.Split(':');


            string[] minuteinfirstandampminsecondindex = (hourinfirstindexandminuteandampminsecondindex[1]).Split(' ');

            string time2 = hourinfirstindexandminuteandampminsecondindex[0] + ":" + minuteinfirstandampminsecondindex[0] + ":" + "00" + " " + minuteinfirstandampminsecondindex[1];


            string[] timeArr = time2.Split(':');

            var timespan = new TimeSpan(Convert.ToInt32(timeArr[0]), Convert.ToInt32(timeArr[1]), 0);

            if (timeArr[2].Split(' ')[1] == "PM")
            {

                timespan.Add(new TimeSpan(12, 0, Convert.ToInt32(timeArr[2].Split(' ')[0])));
            }


            return timespan;

        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RestfulAPIWithAspNet.Test
{
    [TestClass]
    public class UnixTimeTest
    {
    
        [TestMethod]
        public void TestUnixTime()
        {
            Double unixTimeStamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            var d = dtDateTime;

        }

        public DateTime UnixTimeStampToDateTime(Double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}

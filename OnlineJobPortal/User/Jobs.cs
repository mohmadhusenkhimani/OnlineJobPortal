using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.User
{
    public class Jobs
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Salary { get; set; }
        public string JobType { get; set; }
        public string CompanyName { get; set; }
        public string CompanyImage { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
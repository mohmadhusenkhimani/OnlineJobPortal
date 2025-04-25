using System;

namespace OnlineJobPortal.Admin
{
    public class JobModel
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public int NoOfPost { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public DateTime LastDateToApply { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public DateTime CreateDate { get; set; }

        public JobModel() { }

        public JobModel(int jobId, string title, int noOfPost, string qualification, string experience, DateTime lastDateToApply,
                        string companyName, string country, string state, DateTime createDate)
        {
            this.JobId = jobId;
            this.Title = title;
            this.NoOfPost = noOfPost;
            this.Qualification = qualification;
            this.Experience = experience;
            this.LastDateToApply = lastDateToApply;
            this.CompanyName = companyName;
            this.Country = country;
            this.State = state;
            this.CreateDate = createDate;
        }
    }
}

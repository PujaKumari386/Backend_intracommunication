using System;

namespace IntraCommunication.ViewModels
{
    public class GroupRequestModel
    {
        public int SentTo { get; set; }
        public int GroupId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsApproved { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

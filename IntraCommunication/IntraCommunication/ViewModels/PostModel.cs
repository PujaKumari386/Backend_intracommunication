using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace IntraCommunication.ViewModels
{
    public class PostModel
    {
        public string PostDescription { get; set; }
        //public string PostType { get; set; }
        public int PostedOn { get; set; }
        //public DateTime PostedAt { get; set; }
        public int PostedBy { get; set; }
        //public DateTime? StartTime { get; set; }
        //public DateTime? EndTime { get; set; }
        //public string Url { get; set; }
    }
}

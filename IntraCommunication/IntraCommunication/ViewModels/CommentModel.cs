using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace IntraCommunication.ViewModels
{
    public class CommentModel
    {
        public int CommentedBy { get; set; }
        public int PostId { get; set; }
        public string Comment1 { get; set; }
        public DateTime CommentedAt { get; set; }
    }
}

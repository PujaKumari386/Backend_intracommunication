using System.ComponentModel.DataAnnotations;

namespace IntraCommunication.ViewModels
{
    public class LikeModel
    {
        [Required]
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}

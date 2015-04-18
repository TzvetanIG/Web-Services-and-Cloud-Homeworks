namespace News.Servicies.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;

    public class BindingNewsModel
    {
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
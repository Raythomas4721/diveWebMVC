using System.ComponentModel.DataAnnotations;

namespace diveWebMVC.Models
{
    internal class TCcourseMetadata
    {
        [Required(ErrorMessage = "必須填寫課程名稱")]
        [Display(Name = "課程名稱")]
        public int CourseCategoryId { get; set; }

        
        //[DisplayFormat(DataFormatString = "{0:N0}")]
        [DisplayFormat(DataFormatString = "NT${0:N0}")]
        [Range(3000, 90000, ErrorMessage = "訂購單位必須介於{1}~{2}之間")]
        [Display(Name = "課程價格")]
        public decimal? CoursePrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }

    }
}
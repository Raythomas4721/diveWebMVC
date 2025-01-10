using System.ComponentModel.DataAnnotations;

namespace diveWebMVC.Models
{
    internal class TCcourseMetadata
    {
        [Display(Name = "課程編號")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "必須填寫課程名稱")]
        [Display(Name = "課程名稱")]
        public int CourseCategoryId { get; set; }

        [Required(ErrorMessage = "必須填寫課程等級")]
        [Display(Name = "課程等級")]
        public int? LevelId { get; set; }

        [Display(Name = "課程教練")]
        public int? CoachId { get; set; }
        
        //[DisplayFormat(DataFormatString = "{0:N0}")]
        [DisplayFormat(DataFormatString = "NT${0:N0}")]
        [Range(3000, 90000, ErrorMessage = "訂購單位必須介於{1}~{2}之間")]
        [Display(Name = "課程價格")]
        public decimal? CoursePrice { get; set; }

        [Display(Name = "課程圖片")]
        public byte[] Photo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "創建時間")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "更新時間")]
        public DateTime? UpdatedAt { get; set; }

    }
}
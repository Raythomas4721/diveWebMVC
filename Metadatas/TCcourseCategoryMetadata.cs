using System.ComponentModel.DataAnnotations;

namespace diveWebMVC.Models
{
    internal class TCcourseCategoryMetadata
    {
        [Required(ErrorMessage = "必須填寫種類名稱")]
        [Display(Name = "種類名稱")]
        [StringLength(10)]
        public string CategoryName { get; set; }
        
        [Display(Name = "種類描述")]
        [StringLength(100)]
        public string? Description { get; set; }


        [Required(ErrorMessage = "必須填寫種類天數")]     
        [Range(1, 6, ErrorMessage = "訂購單位必須介於{1}~{2}之間")] //勞基法規定不得連續工作逾6日
        [Display(Name = "天數")]
        public int? Duration { get; set; }

        [Required(ErrorMessage = "必須填寫種類名額")]   
        [Range(1, 4, ErrorMessage = "訂購單位必須介於{1}~{2}之間")]//水域遊憩活動管理辦法，水肺潛水：教練每次以指導八人為限。自由潛水：教練每次以指導四人為限。浮潛，教練每次以指導十人為限。
        [Display(Name = "名額")]
        public int? Quota { get; set; }
    }
}
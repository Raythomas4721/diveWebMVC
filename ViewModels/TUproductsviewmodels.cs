using diveWebMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace diveWebMVC.ViewModels
{
    public class TUproductsviewmodels
    {
       
        [Display(Name = "商品編號")]
        [Required(ErrorMessage = "商品編號未填寫")]
        public int ProductId { get; set; }

        [Display(Name = "賣家編號")]
        [Required(ErrorMessage = "賣家編號未填寫")]
        public int? SellerId { get; set; }

        [Display(Name = "賣家名稱")]
        [Required(ErrorMessage = "賣家名稱未填寫")]
        public string SellerName { get; set; }

        [Display(Name = "分類編號")]
        [Required(ErrorMessage = "分類編號未填寫")]
        public int? CategoryId { get; set; }

        [Display(Name = "商品名稱")]
        [Required(ErrorMessage = "商品名稱未填寫")]
        public string ProductName { get; set; }

        [Display(Name = "商品描述")]
        [Required(ErrorMessage = "商品描述未填寫")]
        public string ProductDescription { get; set; }

        [Display(Name = "商品價格")]
        [Required(ErrorMessage = "商品價格未填寫")]
        public decimal? ProductPrice { get; set; }

        [Display(Name = "更新時間")]
        [Required(ErrorMessage = "更新時間未填寫")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "建立時間")]
        [Required(ErrorMessage = "建立時間未填寫")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "商品狀態編號")]
        [Required(ErrorMessage = "商品狀態編號未填寫")]
        public int? ProductConditionId { get; set; }

        [Display(Name = "商品狀態")]
        [Required(ErrorMessage = "商品狀態未填寫")]
        public bool? ProductStatus { get; set; }

        [Display(Name = "主圖片")]
        [Required(ErrorMessage = "主圖片未上傳")]
        public byte[] Image { get; set; }

        [Display(Name = "商品分類")]
        [Required(ErrorMessage = "商品分類未填寫")]
        public virtual TUcategory Category { get; set; }

        [Display(Name = "商品狀態")]
        [Required(ErrorMessage = "商品狀態未填寫")]
        public virtual TUproductCondition ProductCondition { get; set; }

        [Display(Name = "賣家資訊")]
        [Required(ErrorMessage = "賣家資訊未填寫")]
        public virtual TMmemberList Seller { get; set; }

        [Display(Name = "訂單明細")]
        public virtual ICollection<TUorderDetail> TUorderDetails { get; set; } = new List<TUorderDetail>();

        [Display(Name = "商品圖片集合")]
        public virtual ICollection<TUproductImage> TUproductImages { get; set; } = new List<TUproductImage>();
        public List<TUproduct> Products { get; set; }

    }
}

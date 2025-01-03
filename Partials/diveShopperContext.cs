using Microsoft.EntityFrameworkCore;

namespace diveWebMVC.Models

{
    public partial class diveShopperContext : DbContext
    {
        public diveShopperContext() { }//要寫建構函式
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)//讀取設定檔的資料連線
            {
                IConfiguration Config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(Config.GetConnectionString("diveShopper"));
            }
        }

    }
}


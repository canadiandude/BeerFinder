using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerFinder.Models
{
    public class BarsRecord
    {
        public long Id { get; set; }
        [Display(Name = "Nom du Bar")]
        [StringLength(50), Required]
        public String NomBar { get; set; }
        [Display(Name = "Logo")]
        public String Logo { get; set; }
        [Display(Name = "Adresse")]
        [StringLength(100), Required]
        public String Adresse { get; set; }
        ImageGUIDReference ImgRef;

        public BarsRecord()
        {
            ImgRef = new ImageGUIDReference(@"/Images/", @"Anonymous.png");
            Logo = "";
        
        }
        public String GetLogoURL()
        {
            return ImgRef.GetImageURL(Logo);      
        }

        public void UploadLogo(HttpRequestBase Request)
        {
           Logo = ImgRef.UpLoadImage(Request, Logo);
        }
        public void RemoveLogo()
        {
            ImgRef.Remove(Logo);
        }
    }
    public class BarsTable:SqlExpressUtilities.SqlExpressWrapper
    {
        public BarsRecord bar { get; set; }

        public BarsTable(object conn)
            : base(conn)
        {
            bar = new BarsRecord();
            SetTableName("Bars");
        }
        public BarsTable()          
        {
            bar = new BarsRecord();
            SetTableName("Bars");
        }

       public List<BarsRecord> ToList()
        {
            List<object> list = this.RecordsList();

            List<BarsRecord> bars_list = new List<BarsRecord>();
            foreach (BarsRecord bar in list)
            {
                bars_list.Add(bar);
            }
            return bars_list;

        }
       public override void DeleteRecordByID(String ID)
       {
           if (this.SelectByID(ID))
           {
               this.bar.RemoveLogo();
               base.DeleteRecordByID(ID);
           }
       }
        
    }
}
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
        public String Nom { get; set; }
        [Display(Name = "Logo")]
        [StringLength(50), Required]
        public String Logo { get; set; }
        [Display(Name = "Adresse")]
        [StringLength(100), Required]
        public String Adresse { get; set; }

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


    }
}
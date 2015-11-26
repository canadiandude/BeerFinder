using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerFinder.Models
{
    public class SelectionsRecord
    {
        long Id { get; set; }
        long Idbar {get;set; }
        long IdBiere { get; set; }
        decimal Prix { get; set; }


    }
    public class SelectionTable : SqlExpressUtilities.SqlExpressWrapper
    {
        SelectionsRecord Selection { get; set; }
        public SelectionTable()
        {
            Selection = new SelectionsRecord();
            SetTableName("Selections");
        
        }
        public SelectionTable(object cs) :base(cs) 
        {
            Selection = new SelectionsRecord();
            SetTableName("Selections");
        }
    
    }
}
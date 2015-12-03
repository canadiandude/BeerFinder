using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerFinder.Models
{
    public class SelectionsRecord
    {
        public long Id { get; set; }
        public long IdBar {get;set; }
        [Display(Name="Bière")]
        public long IdBiere { get; set; }
        [Required]
        public decimal Prix { get; set; }

        public List<BieresRecord> ListeBieres;
    }
    public class SelectionTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public SelectionsRecord Selection { get; set; }
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

        public List<String> ListPrix(string IdBar)
        {
            List<String> list = new List<String>();
            String SQL = "SELECT * FROM Selections WHERE IdBar=" + IdBar;
            QuerySQL(SQL);

            while (Next())
            {
                String prix = Math.Round(Selection.Prix, 2, MidpointRounding.AwayFromZero).ToString() + " $";
                list.Add(prix);
            }

            return list;
        }

        public void DeleteSelection(String IdBar, String IdBiere)
        {
            String SQL = "DELETE FROM Selections WHERE IdBar=" + IdBar + " AND IdBiere=" + IdBiere;
            NonQuerySQL(SQL);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerFinder.Models
{
    public class BieresRecord
    {
        public long Id { get; set; }

        [Display(Name="Nom")]
        [StringLength(50), Required]
        public String NomBiere { get; set; }

        public long IdType { get; set; }

        public String NomType { get; set; }

        [StringLength(50), Required]
        public String Brasserie { get; set; }

        public int VolumeAlcool { get; set; }

        public String Etiquette { get; set; }

        private ImageGUIDReference ImageReference;

        public BieresRecord()
        {
            NomBiere = "";
            Brasserie = "";
            Etiquette = "";
            ImageReference = new ImageGUIDReference(@"/Images/", "defaultbottle.jpg");
        }

        public String GetEtiquetteURL()
        {
            return ImageReference.GetImageURL(Etiquette);
        }

        public void UploadEtiquette(HttpRequestBase Request)
        {
            Etiquette = ImageReference.UpLoadImage(Request, Etiquette);
        }

        public void RemoveEtiquette()
        {
            ImageReference.Remove(Etiquette);
        }
    }

    public class BieresTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public BieresRecord bieres { get; set; }

        public BieresTable(object cs) : base(cs)
        {
            bieres = new BieresRecord();
            SetTableName("Bieres");
        }

        public BieresTable()
        {
            bieres = new BieresRecord();
            SetTableName("Bieres");
        }

        public override void SelectAll(string orderBy = "")
        {
            String sql = "SELECT " +
                            "Bieres.NomBiere, " +
                            "Bieres.IdType, " +
                            "Types.NomType, " +
                            "Bieres.Brasserie, " +
                            "Bieres.VolumeAlcool, " +
                            "Bieres.Etiquette " +
                            "FROM Bieres " +
                            "INNER JOIN Types ON Bieres.IdType=Types.Id";

            if (orderBy != "")
            {
                sql += " ORDER BY " + orderBy;
            }

            QuerySQL(sql);
        }

        public List<BieresRecord> ToList()
        {
            List<object> list = this.RecordsList();
            List<BieresRecord> bieres_list = new List<BieresRecord>();
            foreach (BieresRecord biere in list)
            {
                bieres_list.Add(biere);
            }
            return bieres_list;
        }
    }
}
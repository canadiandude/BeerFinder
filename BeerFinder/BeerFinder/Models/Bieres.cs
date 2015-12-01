using SqlExpressUtilities;
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

        [Display(Name="Volume d'alcool")]
        [Range(0,100), Required]
        public int VolumeAlcool { get; set; }

        public String Etiquette { get; set; }

        private ImageGUIDReference ImageReference;

        public List<TypesRecord> ListeTypes { get; set; }

        public BieresRecord()
        {
            NomBiere = "";
            Brasserie = "";
            Etiquette = "";
            ImageReference = new ImageGUIDReference(@"/Images/", "defaultbottle.jpg");
            ListeTypes = new List<TypesRecord>();
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
        public BieresRecord biere { get; set; }

        public BieresTable(object cs) : base(cs)
        {
            biere = new BieresRecord();
            SetTableName("Bieres");
        }

        public BieresTable()
        {
            biere = new BieresRecord();
            SetTableName("Bieres");
        }

        public override void SelectAll(string orderBy = "")
        {
            String sql = "SELECT " +
                            "Bieres.Id, " +
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

        public override bool SelectByID(string ID)
        {
            String sql = "SELECT " +
                "Bieres.Id, " +
                "Bieres.NomBiere, " +
                "Bieres.IdType, " +
                "Types.NomType, " +
                "Bieres.Brasserie, " +
                "Bieres.VolumeAlcool, " +
                "Bieres.Etiquette " +
                "FROM Bieres " +
                "INNER JOIN Types ON Bieres.IdType=Types.Id " +
                "WHERE Bieres.Id=" + ID;
            QuerySQL(sql);

            if (reader != null)
            {
                Next();
                return true;
            }
            return false;
        }

        public override void Insert()
        {
            String sql = "INSERT INTO " + SQLTableName + "(NomBiere,IdType,Brasserie,VolumeAlcool,Etiquette) VALUES(" +
                            "'" + SQLHelper.PrepareForSql(biere.NomBiere) + "', " +
                            biere.IdType + ", " +
                            "'" + SQLHelper.PrepareForSql(biere.Brasserie) + "', " +
                            biere.VolumeAlcool + ", " +
                            "'" + SQLHelper.PrepareForSql(biere.Etiquette) + "')";

            NonQuerySQL(sql);                
        }

        public override void Update()
        {
            String sql = "UPDATE " + SQLTableName + " SET " +
                            "NomBiere='" + SQLHelper.PrepareForSql(biere.NomBiere) + "', " +
                            "IdType=" + biere.IdType + ", " +
                            "Brasserie='" + SQLHelper.PrepareForSql(biere.Brasserie) + "', " +
                            "VolumeAlcool=" + biere.VolumeAlcool + ", " +
                            "Etiquette='" + biere.Etiquette + "' " +
                            "WHERE Id=" + biere.Id;
            NonQuerySQL(sql);
        }

        public override void DeleteRecordByID(string ID)
        {
            if (this.SelectByID(ID))
            {
                this.biere.RemoveEtiquette();
                base.DeleteRecordByID(ID);
            }
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
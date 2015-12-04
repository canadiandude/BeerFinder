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

        [Display(Name = "Nom")]
        [StringLength(50), Required]
        public String NomBiere { get; set; }

        public long IdType { get; set; }

        public String NomType { get; set; }

        [StringLength(50), Required]
        public String Brasserie { get; set; }

        [Display(Name = "Volume d'alcool")]
        [Range(0, 100), Required]
        public int VolumeAlcool { get; set; }

        public String Etiquette { get; set; }

        public long IdSelections { get; set; }

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

        public BieresTable(object cs)
            : base(cs)
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

        public void SelectFromSelection(String BarId, String orderBy = "")
        {
            String sql = "SELECT " +
                            "Bieres.Id, " +
                            "Bieres.NomBiere, " +
                            "Bieres.IdType, " +
                            "Types.NomType, " +
                            "Bieres.Brasserie, " +
                            "Bieres.VolumeAlcool, " +
                            "Bieres.Etiquette, " +
                            "Selections.Id AS IdSelections " +
                            "FROM Bieres " +
                            "INNER JOIN Types ON Bieres.IdType=Types.Id " +
                            "INNER JOIN Selections ON Selections.IdBiere=Bieres.Id AND Selections.IdBar=" + BarId + " " +
<<<<<<< HEAD
                            "WHERE Bieres.Id IN (SELECT IdBiere FROM Selections WHERE IdBar="+BarId+") ORDER BY " + orderBy;
=======
                            "WHERE Bieres.Id IN (SELECT IdBiere FROM Selections WHERE IdBar=" + BarId + ")";
>>>>>>> origin/master

            QuerySQL(sql);
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

        public void DeleteAllByType(String IdType)
        {
            SelectByFieldName("IdType", long.Parse(IdType));
            SelectionTable selection = new SelectionTable(connexionString);
            while (Next())
            {
                selection.DeleteAllRecordByFieldName("IdBiere", biere.Id);
                DeleteRecordByID(biere.Id);
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

    public class BieresParBar
    {
        public String NomBiere { get; set; }
        public String NomBar { get; set; }
        public decimal Prix { get; set; }
    }

    public class BieresParBarTable : SqlExpressWrapper
    {
        public BieresParBar bieresParBar {get; set;}
        public BieresParBarTable(object cs)
            : base(cs)
        {
            bieresParBar = new BieresParBar();
        }

        public void SelectBieres(String IdBiere)
        {
            String SQL = "SELECT NomBiere, NomBar, Prix FROM Bieres " +
                            "INNER JOIN Selections ON Selections.IdBiere=Bieres.Id " +
                            "INNER JOIN Bars ON Selections.IdBar=Bars.Id " +
                            "WHERE Selections.IdBiere=" + IdBiere;
            QuerySQL(SQL);
        }

        public List<BieresParBar> ToList()
        {
            List<object> list = this.RecordsList();
            List<BieresParBar> bieres_list = new List<BieresParBar>();
            foreach (BieresParBar biere in list)
            {
                bieres_list.Add(biere);
            }
            return bieres_list;
        }
    }
}
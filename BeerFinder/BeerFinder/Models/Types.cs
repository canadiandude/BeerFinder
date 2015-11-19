using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerFinder.Models
{
    public class TypesRecord
    {
        public long Id { get; set; }

        [Display(Name="Nom")]
        [StringLength(50), Required]
        public String NomType { get; set; }

        [StringLength(250), Required]
        public String Description { get; set; }

        public TypesRecord()
        {
            NomType = "";
            Description = "";
        }
    }

    public class TypesTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public TypesRecord type { get; set; }

        public TypesTable(object cs) : base(cs)
        {
            type = new TypesRecord();
            SetTableName("Types");
        }

        public TypesTable()
        {
            type = new TypesRecord();
            SetTableName("Types");
        }

        public List<TypesRecord> ToList()
        {
            List<object> list = this.RecordsList();
            List<TypesRecord> types_list = new List<TypesRecord>();
            foreach (TypesRecord type in list)
            {
                types_list.Add(type);
            }
            return types_list;
        }
    }
}
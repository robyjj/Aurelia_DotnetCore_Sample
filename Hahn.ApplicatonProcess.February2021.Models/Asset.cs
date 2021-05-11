using Hahn.ApplicatonProcess.February2021.Models.Enums;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hahn.ApplicatonProcess.February2021.Models
{
    public class Asset
    {
        public int ID { get; set; }
        public string AssetName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Department? Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAdressOfDepartment { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; set; }
    }
}
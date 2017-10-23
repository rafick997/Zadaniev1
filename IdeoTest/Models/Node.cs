using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace IdeoTest
{
    [Table("Nodes")]
    public partial class Node
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NodeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Kind { get; set; }
        public int? ParentId { get; set; }




    }
}
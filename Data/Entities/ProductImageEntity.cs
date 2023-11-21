﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("tblProductImages")]
    public class ProductImageEntity:BaseEntity<int>
    {
        [Required,StringLength(255)]
        public string Name { get; set; }
        public int? ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}

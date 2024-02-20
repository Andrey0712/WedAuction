using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("tblCategories")]
    public class CategoryEntity:BaseEntity<int>
    {
       /* [Key]
        public int Id { get; set; }*/
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Image { get; set; }
        public virtual ICollection<ProductEntity> Product { get; set; }
    }
}

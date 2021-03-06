﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        [Column("SectionOrder")]
        public int Order { get; set; }
        
        [Column("ParentId")]
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Section Parent { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

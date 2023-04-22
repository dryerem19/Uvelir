namespace Uvelir.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        [Key]
        [StringLength(50)]
        public string Article { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int UnitId { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        public byte MaxDiscount { get; set; }

        public int ManufacturerId { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        public byte Discount { get; set; }

        public int StorageCount { get; set; }

        [Required]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Unit Unit { get; set; }
    }
}

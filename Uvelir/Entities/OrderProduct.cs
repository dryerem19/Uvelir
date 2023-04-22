namespace Uvelir.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderProduct")]
    public partial class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string Article { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}

namespace Uvelir.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DeliveryDate { get; set; }

        public int PickpointId { get; set; }

        public int UserId { get; set; }

        public int Code { get; set; }

        public int StatusId { get; set; }

        public virtual Pickpoint Pickpoint { get; set; }

        public virtual Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Uvelir.Entities
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<Category> Category { get; set; }
		public virtual DbSet<Manufacturer> Manufacturer { get; set; }
		public virtual DbSet<Order> Order { get; set; }
		public virtual DbSet<OrderProduct> OrderProduct { get; set; }
		public virtual DbSet<Pickpoint> Pickpoint { get; set; }
		public virtual DbSet<Product> Product { get; set; }
		public virtual DbSet<Role> Role { get; set; }
		public virtual DbSet<Status> Status { get; set; }
		public virtual DbSet<Supplier> Supplier { get; set; }
		public virtual DbSet<Unit> Unit { get; set; }
		public virtual DbSet<User> User { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(e => e.Product)
				.WithRequired(e => e.Category)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Manufacturer>()
				.HasMany(e => e.Product)
				.WithRequired(e => e.Manufacturer)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.OrderProduct)
				.WithRequired(e => e.Order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pickpoint>()
				.HasMany(e => e.Order)
				.WithRequired(e => e.Pickpoint)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.Property(e => e.Cost)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.OrderProduct)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Status>()
				.HasMany(e => e.Order)
				.WithRequired(e => e.Status)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Supplier>()
				.HasMany(e => e.Product)
				.WithRequired(e => e.Supplier)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Unit>()
				.HasMany(e => e.Product)
				.WithRequired(e => e.Unit)
				.WillCascadeOnDelete(false);
		}
	}
}

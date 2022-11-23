using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wize.commerce.data.v1.Models;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data
{
    public class WizeContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;
        public WizeContext(DbContextOptions<WizeContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Address>().Property(p => p.AddressId).ValueGeneratedOnAdd();
            modelBuilder.Entity<CategoryRelation>()//.HasQueryFilter()
                .HasKey(pcr => new { pcr.ParentCategoryId, pcr.ChildCategoryId });
            modelBuilder.Entity<CategoryDiscount>()
                .HasKey(cd => new { cd.CategoryId, cd.DiscountId });
            modelBuilder.Entity<ProductCategory>()
                .HasKey(ppc => new { ppc.ProductId, ppc.CategoryId });
            modelBuilder.Entity<Category>()
                .HasMany(pc => pc.Parents).WithOne(pc => pc.Child)
                .HasForeignKey(pc => pc.ChildCategoryId);
            modelBuilder.Entity<Category>()
                .HasMany(pc => pc.Children)
                .WithOne(pcr => pcr.Parent)
                .HasForeignKey(c => c.ParentCategoryId);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Files)
                .WithOne(f => f.Product);
            modelBuilder.Entity<ProductFile>()
                .HasOne(ppf => ppf.Product)
                .WithMany(p => p.Files)
                .HasForeignKey(ppf => ppf.ProductId);
            modelBuilder.Entity<ProductFile>()
                .HasOne(ppf => ppf.File)
                .WithMany(f => f.Products)
                .HasForeignKey(ppf => ppf.FileId);
            modelBuilder.Entity<ProductTrait>()
                .HasIndex(pt => new { pt.ProductId, pt.TraitId }).IsUnique();
            modelBuilder.Entity<ProductDiscount>()
                .HasKey(pd => new { pd.DiscountId, pd.ProductId });

            modelBuilder.Entity<OrderProductTraitOption>()
                .HasKey(opto => new { opto.OrderId, opto.OrderProductId, opto.ProductTraitId, opto.ProductTraitOptionId });

            modelBuilder.Entity<ProductFile>()
                .HasKey(ppf => new { ppf.ProductId, ppf.FileId }); ;

            modelBuilder.Entity<ShoppingCartItemOption>()
                .HasKey(scio => new { scio.ShoppingCartItemId, scio.ProductTraitId, scio.ProductTraitOptionId });
            modelBuilder.Entity<ProductTaxRate>()
                .HasKey(ptr => new { ptr.ProductId, ptr.TaxRateId });
            modelBuilder.Entity<TaxRateStates>()
                .HasKey(trs => new { trs.TaxRateId, trs.StateId });
            modelBuilder.Entity<Discount>()
                .HasIndex(p => new { p.Code })
                .IsUnique(true);
            modelBuilder = AddTenancy(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        //public override TEntity Find<TEntity>(params object[] keyValues)
        //{
        //    var model = base.Find<TEntity>(keyValues);
        //    var tenantId = _tenantProvider.GetTenantId();
        //    var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
        //    if (!tenantId.HasValue || modelTenantId != tenantId.Value)
        //        return default;

        //    return model;
        //}

        //public override object Find(Type entityType, params object[] keyValues)
        //{
        //    var model = base.Find(entityType, keyValues);
        //    var tenantId = _tenantProvider.GetTenantId();
        //    var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
        //    if (!tenantId.HasValue || modelTenantId != tenantId.Value)
        //        return default;

        //    return model;
        //}

        //public override ValueTask<object> FindAsync(Type entityType, params object[] keyValues)
        //{
        //    var model = base.FindAsync(entityType, keyValues);
        //    var tenantId = _tenantProvider.GetTenantId();
        //    var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
        //    if (!tenantId.HasValue || modelTenantId != tenantId.Value)
        //        return default;

        //    return model;
        //}

        //public override ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
        //{
        //    var model = base.FindAsync<TEntity>(keyValues);
        //    var tenantId = _tenantProvider.GetTenantId();
        //    var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
        //    if (!tenantId.HasValue || modelTenantId != tenantId.Value)
        //        return default;

        //    return model;
        //}

        //public override ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        //{
        //    var model = base.FindAsync<TEntity>(keyValues, cancellationToken);
        //    var tenantId = _tenantProvider.GetTenantId();
        //    var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
        //    if (!tenantId.HasValue || modelTenantId != tenantId.Value)
        //        return default;

        //    return model;
        //}

        //public override ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        //{
        //    var model = base.FindAsync(entityType, keyValues, cancellationToken);
        //    var tenantId = _tenantProvider.GetTenantId();
        //    var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
        //    if (!tenantId.HasValue || modelTenantId != tenantId.Value)
        //        return default;

        //    return model;
        //}

        public override int SaveChanges()
        {
            ApplyTenancy();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyTenancy();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTenancy();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyTenancy();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyTenancy()
        {
            var modified = ChangeTracker.Entries<ITenantModel>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entity in modified)
            {
                var property = entity.Property("TenantId");
                if (property != null)
                {
                    property.CurrentValue = _tenantProvider.GetTenantId();
                }
            }
        }

        private ModelBuilder AddTenancy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().Property<Guid>("TenantId");
            modelBuilder.Entity<Category>().Property<Guid>("TenantId");
            modelBuilder.Entity<CategoryDiscount>().Property<Guid>("TenantId");
            modelBuilder.Entity<CategoryRelation>().Property<Guid>("TenantId");
            modelBuilder.Entity<Discount>().Property<Guid>("TenantId");
            modelBuilder.Entity<File>().Property<Guid>("TenantId");
            modelBuilder.Entity<FileType>().Property<Guid>("TenantId");
            modelBuilder.Entity<Inventory>().Property<Guid>("TenantId");
            modelBuilder.Entity<Order>().Property<Guid>("TenantId");
            modelBuilder.Entity<OrderProduct>().Property<Guid>("TenantId");
            modelBuilder.Entity<OrderProductShipping>().Property<Guid>("TenantId");
            modelBuilder.Entity<OrderProductTraitOption>().Property<Guid>("TenantId");
            modelBuilder.Entity<Product>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductCategory>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductDiscount>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductFile>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductTaxRate>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductTrait>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductTraitOption>().Property<Guid>("TenantId");
            modelBuilder.Entity<ProductView>().Property<Guid>("TenantId");
            modelBuilder.Entity<Review>().Property<Guid>("TenantId");
            modelBuilder.Entity<ShoppingCartItem>().Property<Guid>("TenantId");
            modelBuilder.Entity<ShoppingCartItemOption>().Property<Guid>("TenantId");
            modelBuilder.Entity<State>().Property<Guid>("TenantId");
            modelBuilder.Entity<TaxRate>().Property<Guid>("TenantId");
            modelBuilder.Entity<TaxRateStates>().Property<Guid>("TenantId");
            modelBuilder.Entity<Thumbnail>().Property<Guid>("TenantId");
            modelBuilder.Entity<Trait>().Property<Guid>("TenantId");
            modelBuilder.Entity<UserOrders>().Property<Guid>("TenantId");


            return modelBuilder;
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<OrderProductShipping> OrderProductShippings { get; set; }
        public virtual DbSet<OrderProductTraitOption> OrderProductTraitOptions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryRelation> CategoryRelations { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Thumbnail> Thumbnails { get; set; }
        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductFile> ProductFiles { get; set; }
        public virtual DbSet<ProductTrait> ProductTraits { get; set; }
        public virtual DbSet<ProductTraitOption> ProductTraitOptions { get; set; }
        public virtual DbSet<ProductView> ProductViews { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<ShoppingCartItemOption> ShoppingCartItemOptions { get; set; }
        public virtual DbSet<Trait> Traits { get; set; }
        public virtual DbSet<UserOrders> UserOrders { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<ProductTaxRate> ProductTaxRates { get; set; }
        public virtual DbSet<TaxRateStates> TaxRateStates { get; set; }
    }
}

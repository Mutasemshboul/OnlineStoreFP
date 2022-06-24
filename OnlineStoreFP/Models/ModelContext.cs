using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryFp> CategoryFps { get; set; }
        public virtual DbSet<Contactdynmic> Contactdynmics { get; set; }
        public virtual DbSet<ContactusFp> ContactusFps { get; set; }
        public virtual DbSet<ProductFp> ProductFps { get; set; }
        public virtual DbSet<ProductuserFp> ProductuserFps { get; set; }
        public virtual DbSet<RolesFp> RolesFps { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<TestimonialFp> TestimonialFps { get; set; }
        public virtual DbSet<UserFp> UserFps { get; set; }
        public virtual DbSet<UserloginFp> UserloginFps { get; set; }
        public virtual DbSet<VisacarFp> VisacarFps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User65;PASSWORD=ammri2014;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER65");

            modelBuilder.Entity<CategoryFp>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("CATEGORY_FP_PK");

                entity.ToTable("CATEGORY_FP");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contactdynmic>(entity =>
            {
                entity.ToTable("CONTACTDYNMIC");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Contactus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTUS");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Information)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("INFORMATION");

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONE1");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONE2");

                entity.Property(e => e.State1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATE1");

                entity.Property(e => e.State2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATE2");

                entity.Property(e => e.Street1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STREET1");

                entity.Property(e => e.Street2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STREET2");
            });

            modelBuilder.Entity<ContactusFp>(entity =>
            {
                entity.HasKey(e => e.Contactid)
                    .HasName("CONTACTUS_FP_PK");

                entity.ToTable("CONTACTUS_FP");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Subject)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");
            });

            modelBuilder.Entity<ProductFp>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("PRODUCT_FP_PK");

                entity.ToTable("PRODUCT_FP");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Sale)
                    .HasColumnType("FLOAT")
                    .HasColumnName("SALE");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STOREID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductFps)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCT_FP_FK1");
            });

            modelBuilder.Entity<ProductuserFp>(entity =>
            {
                entity.HasKey(e => e.Productuserid)
                    .HasName("PRODUCTUSER_FP_PK");

                entity.ToTable("PRODUCTUSER_FP");

                entity.Property(e => e.Productuserid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTUSERID");

                entity.Property(e => e.Datefrom)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEFROM");

                entity.Property(e => e.Dateto)
                    .HasColumnType("DATE")
                    .HasColumnName("DATETO");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductuserFps)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCTUSER_FP_FK1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProductuserFps)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCTUSER_FP_FK2");
            });

            modelBuilder.Entity<RolesFp>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("ROLES_FP_PK");

                entity.ToTable("ROLES_FP");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("roleid");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("SHOPPING_CART");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SHOPPING_CART_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SHOPPING_CART_FK1");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("STORES");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STOREID");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Storename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STORENAME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("STORES_FK1");
            });

            modelBuilder.Entity<TestimonialFp>(entity =>
            {
                entity.HasKey(e => e.Testimonialid)
                    .HasName("TESTIMONIAL_FP_PK");

                entity.ToTable("TESTIMONIAL_FP");

                entity.Property(e => e.Testimonialid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTIMONIALID");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TestimonialFps)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TESTIMONIAL_FP_FK1");
            });

            modelBuilder.Entity<UserFp>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("USER_FP_PK");

                entity.ToTable("USER_FP");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");
            });

            modelBuilder.Entity<UserloginFp>(entity =>
            {
                entity.HasKey(e => e.Loginid)
                    .HasName("USERLOGIN_FP_PK");

                entity.ToTable("USERLOGIN_FP");

                entity.Property(e => e.Loginid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGINID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserloginFps)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERLOGIN_FP_FK1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserloginFps)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERLOGIN_FP_FK2");
            });

            modelBuilder.Entity<VisacarFp>(entity =>
            {
                entity.HasKey(e => e.Visaid)
                    .HasName("VISACAR_FP_PK");

                entity.ToTable("VISACAR_FP");

                entity.Property(e => e.Visaid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VISAID");

                entity.Property(e => e.Balance)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Numbercard)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NUMBERCARD");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisacarFps)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("VISACAR_FP_FK1");
            });

            modelBuilder.HasSequence("DEPT");

            modelBuilder.HasSequence("SEQ");

            modelBuilder.HasSequence("SUPPLIER_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

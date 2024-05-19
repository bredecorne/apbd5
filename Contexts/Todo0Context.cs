using Microsoft.EntityFrameworkCore;
using Zadanie7.Models;

namespace Zadanie7.Contexts;

public partial class Todo0Context : DbContext
{
    public Todo0Context()
    {
    }

    public Todo0Context(DbContextOptions<Todo0Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductWarehouse> ProductWarehouses { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;User ID=user;Password=paswword;Database=todo0;Port=3306;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Area).HasMaxLength(200);
            entity.Property(e => e.Category).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PRIMARY");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Pesel, "Pesel").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(11);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("PRIMARY");

            entity.ToTable("Client_Trip");

            entity.HasIndex(e => e.IdTrip, "IdTrip");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("Client_Trip_ibfk_1");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .HasConstraintName("Client_Trip_ibfk_2");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("PRIMARY");

            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.IdTrips).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .HasConstraintName("Country_Trip_ibfk_2"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .HasConstraintName("Country_Trip_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdTrip").HasName("PRIMARY");
                        j.ToTable("Country_Trip");
                        j.HasIndex(new[] { "IdTrip" }, "IdTrip");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.IdProduct, "Receipt_Product");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FulfilledAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Receipt_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(25);
        });

        modelBuilder.Entity<ProductWarehouse>(entity =>
        {
            entity.HasKey(e => e.IdProductWarehouse).HasName("PRIMARY");

            entity.ToTable("Product_Warehouse");

            entity.HasIndex(e => e.IdOrder, "Product_Warehouse_Order");

            entity.HasIndex(e => e.IdProduct, "_Product");

            entity.HasIndex(e => e.IdWarehouse, "_Warehouse");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Price).HasPrecision(25);

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ProductWarehouses)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Warehouse_Order");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductWarehouses)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_Product");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.ProductWarehouses)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_Warehouse");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("PRIMARY");

            entity.ToTable("Trip");

            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.IdWarehouse).HasName("PRIMARY");

            entity.ToTable("Warehouse");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

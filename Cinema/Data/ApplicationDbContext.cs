using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountingReport> AccountingReport { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<MovieSession> MovieSession { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<PurchaseReport> PurchaseReport { get; set; }
        public virtual DbSet<StockReport> StockReport { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingReport>(entity =>
            {
                entity.ToTable("AccountingReport");

                entity.HasIndex(e => e.EmployeeId, "IX_AccountingReport_EmployeeId");
            });

            modelBuilder.Entity<Department>(entity => { entity.ToTable("Department"); });

            modelBuilder.Entity<MovieSession>(entity =>
            {
                entity.ToTable("MovieSession");

                entity.Property(e => e.SessionTime).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => e.BuyerId, "IX_Order_BuyerId");

                entity.HasIndex(e => e.TicketId, "IX_Order_TicketId");

                entity.HasOne(d => d.Ticket).WithMany(p => p.Orders).HasForeignKey(d => d.TicketId);

                entity.Property(e => e.IsConfirmed)
                    .HasDefaultValue(false);

                entity.Property(e => e.IsRejected)
                    .HasDefaultValue(false);
            });

            modelBuilder.Entity<PurchaseReport>(entity =>
            {
                entity.ToTable("PurchaseReport");

                entity.HasIndex(e => e.DepartmentId, "IX_PurchaseReport_DepartmentId");

                entity.HasOne(d => d.Department).WithMany(p => p.PurchaseReports).HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<StockReport>(entity => { entity.ToTable("StockReport"); });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => e.SessionId, "IX_Ticket_SessionId");

                entity.HasOne(d => d.Session).WithMany(p => p.Tickets).HasForeignKey(d => d.SessionId);
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "9e7e77c1-fcce-482f-8dcf-4ae4210adba0",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "f5c0df4d-9dbb-4722-b7a7-3ba0fc1c7e38"
                }
            );

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "fefb8578-c9ac-4b02-95f5-f7a4dc793ed6",
                    Email = "test@mail.ru",
                    NormalizedEmail = "TEST@MAIL.RU",
                    UserName = "test@mail.ru",
                    NormalizedUserName = "TEST@MAIL.RU",
                    PasswordHash = "AQAAAAIAAYagAAAAEI+WCmRIWIYtaQpqIlEPNQyq156jw2tEMChu5JMNzweCW0HRO5/AA0My2A7TcOfc6A==",
                    SecurityStamp = "4QVFWNN4AOCRLQCPBBLZUT4GM4VQLQ72",
                    ConcurrencyStamp = "6f0257a4-b493-47c5-8155-069376e0b464"
                },
                new IdentityUser
                {
                    Id = "3334e008-6158-4266-aaf2-55f5e1fdfd57",
                    Email = "admin@mail.ru",
                    NormalizedEmail = "ADMIN@MAIL.RU",
                    UserName = "admin@mail.ru",
                    NormalizedUserName = "ADMIN@MAIL.RU",
                    PasswordHash = "AQAAAAIAAYagAAAAEES0k4oZq42RlK1fJzvkj97agDjVoo0skoYL4iQEydCU0n7xi7ElDvp/ybmLRPpqpA==",
                    SecurityStamp = "MKR3YITLVUWEWWUI6J7EDM4744QA4P3G",
                    ConcurrencyStamp = "2e42b802-0b3b-4e64-9965-c0e6d232a004"
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "9e7e77c1-fcce-482f-8dcf-4ae4210adba0",
                    UserId = "3334e008-6158-4266-aaf2-55f5e1fdfd57"
                });

            modelBuilder.Entity<MovieSession>().HasData(
                new MovieSession
                {
                    Id = 1,
                    MovieName = "Titanic",
                    SessionTime = new DateTime(2024, 6, 15)
                });

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    SessionId = 1,
                    SeatNumber = 1,
                    RowNumber = 1
                },
                new Ticket
                {
                    Id = 2,
                    SessionId = 1,
                    SeatNumber = 2,
                    RowNumber = 1
                },
                new Ticket
                {
                    Id = 3,
                    SessionId = 1,
                    SeatNumber = 3,
                    RowNumber = 1
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    BuyerId = "fefb8578-c9ac-4b02-95f5-f7a4dc793ed6",
                    TicketId = 1,
                    IsConfirmed = true
                },
                new Order
                {
                    Id = 2,
                    BuyerId = "fefb8578-c9ac-4b02-95f5-f7a4dc793ed6",
                    TicketId = 2,
                },
                new Order
                {
                    Id = 3,
                    BuyerId = "fefb8578-c9ac-4b02-95f5-f7a4dc793ed6",
                    TicketId = 3,
                    IsRejected = true
                }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
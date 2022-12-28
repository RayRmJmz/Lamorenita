using Lamorenita.Data_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Data_Contexts
{
    public class LamorenitaDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public LamorenitaDbContext(DbContextOptions<LamorenitaDbContext> options, IPasswordHasher<ApplicationUser> passwordHasher) : base(options)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddRoles(modelBuilder);
            AddAdministradorMonitor(modelBuilder);


            modelBuilder.Entity<ContactEntity>()
                .Property(b => b.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<DirectionEntity>()
                .Property(b => b.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<ProductEntity>()
                .Property(b => b.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<ProductTypeEntity>()
                .Property(b => b.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<StoreEntity>()
                .Property(b => b.Active)
                .HasDefaultValue(true);

        }

        private void AddAdministradorMonitor(ModelBuilder builder)
        {
            // Se genera el Usuario Administrador
            var user = new ApplicationUser
            {
                Id = "99999999",
                UserName = "Administrador",
                NormalizedUserName = "Administrador".ToUpper(),
                Email = "myuser@domain.com",
                EmailConfirmed = true,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, "administrador");
            builder.Entity<ApplicationUser>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "99999999"
            });
            
        }
        private void AddRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Administrador", NormalizedName = "ADMINISTRADOR".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2", Name = "UsuarioInterno", NormalizedName = "UsuarioInterno".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3", Name = "UsuarioExterno", NormalizedName = "UsuarioExterno".ToUpper() });
        }
        #region My DbSet
        public  DbSet<ProductTypeEntity> ProductType { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<ContactEntity> Contact { get; set; }
        public DbSet<DirectionEntity> Direction { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumber { get; set; }
        public DbSet<ContactDirectionEntity> ContactDirection { get; set; }
        public DbSet<StoreEntity> Store { get; set; }
        public DbSet<StoreDirectionEntity> StoreDirection { get; set; }
        public DbSet<StoreManagerEntity> StoreManager { get; set; }
        #endregion}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMONIC_Desktop
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class gr602_chudedEntities : DbContext
    {
        public gr602_chudedEntities()
            : base("name=gr602_chudedEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Error> Error { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<SessionEndStatus> SessionEndStatus { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<SessionEnd> SessionEnd { get; set; }
        public virtual DbSet<Aircrafts> Aircrafts { get; set; }
        public virtual DbSet<Airports> Airports { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
    }
}

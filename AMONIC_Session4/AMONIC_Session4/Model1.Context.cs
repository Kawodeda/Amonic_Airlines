﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMONIC_Session4
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
    
        public virtual DbSet<Airports> Airports { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<CabinTypes> CabinTypes { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
    }
}
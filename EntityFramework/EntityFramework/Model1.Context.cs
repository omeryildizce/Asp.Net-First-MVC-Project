﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EntityFrameworkEntities : DbContext
    {
        public EntityFrameworkEntities()
            : base("name=EntityFrameworkEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ders> Ders { get; set; }
        public virtual DbSet<Notlar> Notlar { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Kulupler> Kulupler { get; set; }
    
        public virtual ObjectResult<NotListesi_Result> NotListesi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NotListesi_Result>("NotListesi");
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fuhrparkmanagement
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FuhrparkmanagementModelContainer : DbContext
    {
        public FuhrparkmanagementModelContainer()
            : base("name=FuhrparkmanagementModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Fahrzeugtyp> Fahrzeugtyp { get; set; }
        public virtual DbSet<Fahrzeug> Fahrzeug { get; set; }
        public virtual DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public virtual DbSet<Buchung> Buchung { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartGymSales.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SmartGymSalesEntities : DbContext
    {
        public SmartGymSalesEntities()
            : base("name=SmartGymSalesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdditionLookup> AdditionLookups { get; set; }
        public virtual DbSet<commission> commissions { get; set; }
        public virtual DbSet<knowledgeLookup> knowledgeLookups { get; set; }
        public virtual DbSet<possibleCustmer> possibleCustmers { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<SalesCustomer> SalesCustomers { get; set; }
    }
}

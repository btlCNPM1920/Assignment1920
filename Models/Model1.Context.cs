﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Computer_Shop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Computer_Management_SystemEntities : DbContext
    {
        public Computer_Management_SystemEntities()
            : base("name=Computer_Management_SystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OriginalOrder> OriginalOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User_Report> User_Report { get; set; }
        public virtual DbSet<AccountCustomerEmployeeOrder> AccountCustomerEmployeeOrders { get; set; }
        public virtual DbSet<AccountEmployee> AccountEmployees { get; set; }
    }
}

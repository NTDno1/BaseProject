﻿using DemoCICD.Domain.Entities;
using DemoCICD.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = DemoCICD.Domain.Entities.Identity.Action;

namespace DemoCICD.Persistance;
public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public DbSet<AppUser> AppUses { get; set; }

    public DbSet<Action> Actions { get; set; }

    public DbSet<Function> Functions { get; set; }

    public DbSet<ActionInFunction> ActionInFunctions { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<Product> Products { get; set; }
}

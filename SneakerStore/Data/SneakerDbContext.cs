using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SneakerStore.Models;

public class SneakerDbContext : DbContext
{
    public SneakerDbContext(DbContextOptions<SneakerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Brand> Brand { get; set; } = default!;

    public DbSet<Sneaker> Sneaker { get; set; } = default!;
}

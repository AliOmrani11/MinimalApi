﻿using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain;

namespace MinimalApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Book> Books =>Set<Book>();
}
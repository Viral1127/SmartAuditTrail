
# SmartAuditTrail

[![NuGet](https://img.shields.io/nuget/v/SmartAuditTrail)](https://www.nuget.org/packages/SmartAuditTrail/)

**SmartAuditTrail** is a .NET library for **automatic audit logging** of EF Core entities.  
It tracks **Create / Update / Delete** operations, stores changes, and works with **DbContext interceptors**.


## Features

- Tracks changes automatically for any entity
- Works with **Entity Framework Core**
- Easy to integrate with **Dependency Injection**
- Stores audit logs in `AuditLog` table


## Installation

Install from NuGet:

```bash
  Install-Package SmartAuditTrail -Version 0.1.0
```

Or using the Package Manager in Visual Studio:
```bash
  Manage NuGet Packages → Browse → SmartAuditTrail → Install
```
    
## Usage/Examples

1. Add DbSet for AuditLog in your DbContext

```javascript
public class AppDbContext : DbContext
{
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SmartAuditInterceptor());
    }
}
```
2. Mark Entities as Auditable

```javascript
using SmartAuditTrail.Attributes;

[Auditable]
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

3. Add / Update / Delete Data

```javascript
using var context = new AppDbContext();
context.Products.Add(new Product { Name = "Laptop", Price = 1000 });
context.SaveChanges();
```
Audit logs will automatically be added to AuditLogs table.


## Demo

This repository includes a demo project DemoApp showing the library in action with dummy data.

 **Steps**

- Open DemoApp in Visual Studio

- Restore NuGet packages (SmartAuditTrail + EF Core InMemory)

- Press F5 to run

- Expected output in console:
```powershell
=== Adding Dummy Data ===
=== Updating Dummy Data ===
=== Deleting Dummy Data ===

=== Audit Logs ===
[12/15/2025 7:27:04 AM] Added on DemoEntity Id=1
Changes: {"OldValues":{},"NewValues":{"Id":1,"Name":"Item1","Value":10}}
[12/15/2025 7:27:04 AM] Added on DemoEntity Id=2
Changes: {"OldValues":{},"NewValues":{"Id":2,"Name":"Item2","Value":20}}
[12/15/2025 7:27:04 AM] Unchanged on DemoEntity Id=2
Changes: {"OldValues":{},"NewValues":{}}
[12/15/2025 7:27:04 AM] Modified on DemoEntity Id=1
Changes: {"OldValues":{"Value":10},"NewValues":{"Value":100}}
[12/15/2025 7:27:04 AM] Unchanged on DemoEntity Id=1
Changes: {"OldValues":{},"NewValues":{}}
[12/15/2025 7:27:04 AM] Deleted on DemoEntity Id=2
Changes: {"OldValues":{"Id":2,"Name":"Item2","Value":20},"NewValues":{}}

Demo Finished!
```

**Demo Highlights**

- Adds, updates, and deletes dummy entities
- Automatic audit logs are recorded
- Uses InMemory database (no real DB setup required)
- Shows how to integrate SmartAuditTrail in a real project
## Contribution

Contributions are always welcome!

- Fork the repository
- Clone locally
- Make changes / add features
- Submit a pull request


## License

[MIT License © 2025](https://choosealicense.com/licenses/mit/)


## Feedback

If you have any feedback, please reach out at viralchauhan4438@gmail.com


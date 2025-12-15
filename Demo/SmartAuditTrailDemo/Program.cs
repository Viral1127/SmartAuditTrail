using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SmartAuditTrailDemo;

class Program
{
    static void Main()
    {
        var options = new DbContextOptionsBuilder<DemoDbContext>()
            .UseInMemoryDatabase("DemoDb")
            .Options;

        using var context = new DemoDbContext(options);

        Console.WriteLine("=== Adding Dummy Data ===");
        var entity1 = new DemoEntity { Name = "Item1", Value = 10 };
        var entity2 = new DemoEntity { Name = "Item2", Value = 20 };
        context.DemoEntities.AddRange(entity1, entity2);
        context.SaveChanges();

        Console.WriteLine("=== Updating Dummy Data ===");
        entity1.Value = 100;
        context.DemoEntities.Update(entity1);
        context.SaveChanges();

        Console.WriteLine("=== Deleting Dummy Data ===");
        context.DemoEntities.Remove(entity2);
        context.SaveChanges();

        Console.WriteLine("\n=== Audit Logs ===");
        foreach (var log in context.AuditLogs.ToList())
        {
            Console.WriteLine($"[{log.Timestamp}] {log.Action} on {log.EntityName} Id={log.EntityId}");
            Console.WriteLine($"Changes: {log.Changes}");
        }

        Console.WriteLine("\nDemo Finished!");
        Console.ReadLine();
    }
}

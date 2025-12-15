# SmartAuditTrail

SmartAuditTrail is a lightweight, extensible **EF Core audit trail library** that automatically tracks entity changes (Add, Update, Delete) with **minimal configuration**.

It is designed for **real-world enterprise applications**, keeping auditing logic out of your business code while remaining fully configurable.

---

## ✨ Key Features

* 🔍 Automatic auditing using EF Core `SaveChangesInterceptor`
* 🏷️ Attribute-based control (`[Auditable]`, `[IgnoreAudit]`)
* 🔐 Authentication-agnostic (works with JWT, cookies, or custom auth)
* 📦 Plug-and-play DI setup
* 🧾 JSON-based change history
* 🧠 Clean separation between library & consumer database
* ⚡ Lightweight & high-performance

---

## 📦 Installation

```bash
dotnet add package SmartAuditTrail
```

---

## 🚀 Quick Start

### 1️⃣ Register SmartAuditTrail

```csharp
builder.Services.AddSmartAudit(options =>
{
    options.ApplicationName = "MyApplication";
});
```

---

### 2️⃣ Add Interceptor to DbContext

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.AddInterceptors(
        this.GetService<SmartAuditInterceptor>());
}
```

---

### 3️⃣ Mark Entities as Auditable

```csharp
using SmartAuditTrail.Attributes;

[Auditable]
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    [IgnoreAudit]
    public string InternalCode { get; set; }
}
```

---

### 4️⃣ Add AuditLog Table (Consumer App)

```csharp
using SmartAuditTrail.Models;

public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
```

Run migration:

```bash
dotnet ef migrations add AddAuditLog
dotnet ef database update
```

---

## 🧾 AuditLog Structure

```csharp
public class AuditLog
{
    public Guid Id { get; set; }
    public string EntityName { get; set; }
    public string EntityId { get; set; }
    public string Action { get; set; }
    public string Changes { get; set; }
    public string? UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string? ApplicationName { get; set; }
}
```

---

## 🔐 User Identification

By default, SmartAuditTrail uses a system user.

You can provide your own user logic:

```csharp
public class CustomUserProvider : IUserProvider
{
    public string? GetUserId()
    {
        return "user-123"; // From JWT, HttpContext, etc.
    }
}
```

Register it:

```csharp
services.AddScoped<IUserProvider, CustomUserProvider>();
```

---

## 📂 How It Works Internally

1. EF Core calls `SaveChanges`
2. SmartAuditInterceptor detects entity state changes
3. Old & new values are captured
4. Changes are serialized as JSON
5. AuditLog is saved in the same transaction

---

## 🧠 Design Principles

* ❌ No automatic migrations
* ❌ No forced database schema
* ✅ Consumer controls database
* ✅ Library stays database-agnostic
* ✅ Production-ready architecture

---

## 🧪 Supported EF Core States

| State     | Audited |
| --------- | ------- |
| Added     | ✅       |
| Modified  | ✅       |
| Deleted   | ✅       |
| Unchanged | ❌       |

---

## 📈 Roadmap

* [ ] Async audit writer
* [ ] Multiple audit stores (DB / File / Queue)
* [ ] Audit dashboard UI
* [ ] Batch audit optimization
* [ ] v1.0 stable release

---

## 🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first.

---

## 📄 License

MIT License

---

## ⭐ Author

**Viral Chauhan**
.NET Developer | Full-Stack Engineer

---

If you find this library useful, consider giving it a ⭐ on GitHub.

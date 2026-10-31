# EventHub — EF Core Console App

A .NET 10 console application using Entity Framework Core 10 and SQL Server.

## Requirements

- .NET 10 SDK
- SQL Server LocalDB (or another SQL Server instance)

## Run

From this directory:

```bash
dotnet restore
dotnet run
```

The sample uses this connection string in `Program.cs`:

```text
Server=(localdb)\MSSQLLocalDB;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True;
```

Change it to match your SQL Server instance when LocalDB is not installed.

The application calls `EnsureCreated()` to create the database schema on first run, inserts a sample organizer/profile/event and attendee/badge, then prints the saved records. `EnsureCreated()` is used for this simple demonstration; do not combine it with EF Core migrations. For a migrations-based workflow, remove the `EnsureCreated()` call, install `Microsoft.EntityFrameworkCore.Design`, create an initial migration, and use `Database.Migrate()`.

## Entity configuration strategy

- `Organizer` and `Attendee`: Data Annotations only for their specified columns and constraints.
- `OrganizerProfile` and `Event`: Fluent API via separate `IEntityTypeConfiguration<T>` classes.
- `Badge`: Fluent API configured inline in `EventHubDbContext.OnModelCreating()`.

## Relationships

- `Organizer` 1 — 1 `OrganizerProfile`: `OrganizerProfile.OrganizerId` is both PK and FK, so a profile cannot exist without an organizer.
- `Organizer` 1 — many `Event`: `Event.OrganizerId` is a required FK.
- `Attendee` 1 — 0/1 `Badge`: `Badge.AttendeeId` is a required FK and EF Core configures it as unique for the one-to-one relationship. An attendee may have no badge yet, but cannot have more than one.

`BadgeNumber` also has a unique index, making each issued badge number unique across the platform.

`Attendee.FullAddress` is marked `[NotMapped]`, so EF Core will not create a database column for it. `PostalCode` maps to the `ZipCode` column.

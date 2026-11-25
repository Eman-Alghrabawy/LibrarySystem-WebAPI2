# Library System — Web API (Project 2)

The same architecture and style as the DEPI example Web API
(`Product / Category` store), applied to the Library System, with the
example's known bugs fixed (see the list at the bottom).

- .NET 10 / EF Core 10 / SQL Server
- Two DbContexts: `ApplicationDbContext` (library data) and
  `AuthenticationDbContext` (ASP.NET Core Identity + JWT), in a separate database
- Repository + Service layers, AutoMapper, `GenericResult<T>` / `PaginatedList<T>`
- Soft delete + audit interceptors, custom middlewares and action filters
- Swagger with JWT Bearer support

## Entities and relationships

| Entity | Properties | Relationships |
|---|---|---|
| Author | Id, Name, BirthDate | One-to-Many with Book |
| Book | Id, Title, ISBN, AuthorId | Many-to-One with Author; Many-to-Many with Borrower through Loan |
| Borrower | Id, Name, MembershipDate | Many-to-Many with Book through Loan |
| Loan | BookId, BorrowerId, LoanDate, ReturnDate (null = still borrowed) | Join entity; composite primary key (BookId + BorrowerId) |

Author, Book and Borrower inherit `BaseEntity` (Id + audit fields + IsDeleted).
Loan does not, because its key is composite — it is configured in
`Data/Configurations/LoanConfigurations.cs`.

All mapping is Fluent API in `Data/Configurations`, loaded with
`ApplyConfigurationsFromAssembly`. ISBN has a unique index.

## Endpoints

All library endpoints require a JWT token (`[Authorize]`).

| Controller | Endpoints |
|---|---|
| Account | POST `api/Account/register`, POST `api/Account/login` |
| Authors | GET `get-all`, GET `get-by-id/{id}`, POST `create`, PUT `update`, DELETE `delete/{id}` |
| Books | Same as Authors, plus GET `get-with-criteria?pageNumber&pageSize&searchTerm` (paginated) |
| Borrowers | Same as Authors |
| Loans | GET `get-all`, GET `get-by-id/{bookId}/{borrowerId}`, POST `create`, PUT `update`, DELETE `delete/{bookId}/{borrowerId}` |

Routes are under `api/<Controller>/...`, for example `api/Books/get-all`.

## How to run

You need a local SQL Server. The connection strings in `appsettings.json`
use `Server=.` with Windows authentication and create two databases:
`LibrarySystem` and `LibrarySystem_Auth`. If you use SQL authentication,
edit both connection strings first.

From the folder that contains `LibrarySystem.slnx`:

```bash
cd LibrarySystem

dotnet restore

# One migration per DbContext (same split as the example)
dotnet ef migrations add InitialCreate --context ApplicationDbContext --output-dir Data/Migrations
dotnet ef migrations add AddAuthentication --context AuthenticationDbContext --output-dir Migrations/AuthenticationDb

dotnet run --project LibrarySystem
```

If `dotnet ef` is not recognised: `dotnet tool install --global dotnet-ef`

You do NOT need `dotnet ef database update`: `Program.cs` runs
`MigrateAsync()` for BOTH contexts at startup and then seeds
2 authors, 2 borrowers and 2 books from `Data/Seeding/*.json`.

Note: the seeded Books use AuthorId 1 and 2, which assumes a fresh
database where the seeded authors are the first rows.

## Try it in Swagger

1. Open https://localhost:7289/swagger (or the http profile).
2. POST `api/Account/register` — password needs upper, lower, digit and
   a special character, e.g. `Emma@123`.
3. POST `api/Account/login` and copy the returned token.
4. Click **Authorize** and paste the token, then call e.g. `GET api/Books/get-all`.

## Fixes compared with the example

1. Seeding used a hard-coded absolute path (`C:\Users\Ahmed\Desktop\...`).
   Now the content root is passed to `SeedAsync`, so it works on any machine.
2. `CategoryRepository.UpdateAsync` forgot `await` on its existence check.
   Every repository here awaits it.
3. The JWT setting was misspelled `DurtionInMinutes`. It is
   `DurationInMinutes` in both `appsettings.json` and `JwtService`.
4. The Authentication database was never migrated automatically.
   `Program.cs` now migrates both contexts.
5. CORS was registered but `UseCors` was never called, and the policy mixed
   `WithOrigins(...)` with `AllowAnyOrigin()`. Both are fixed.
6. `Register` never saved `ApplicationUser.FullName`. The register request
   now includes it and it is stored.
7. One consistent architecture: in the example, Products used the DbContext
   directly while Categories used Service/Repository and only GetById was
   active. Here every controller uses Service -> Repository and the full
   CRUD is active. The template `WeatherForecast` files were removed.

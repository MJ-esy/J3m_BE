# J3m_BE

Repository: https://github.com/MJ-esy/J3m_BE

Deployed application (Azure): https://j3m-be-fuf6ckfsg2gharhw.swedencentral-01.azurewebsites.net/swagger/index.html


## Project overview

`J3m_BE` is an ASP.NET Core Web API backend for a recipe-sharing application. It provides user authentication and authorization (ASP.NET Identity + JWT), user/recipe relationships, and models for recipes, ingredients and diets. The API issues JWT tokens for client authentication and supports role assignment (default role: `User`).

Key technologies
- .NET 9, C# 13
- ASP.NET Core Web API
- ASP.NET Core Identity (with `AppUser` extending `IdentityUser`)
- JWT authentication
- EF Core (database migrations and models)
- Designed to run from Visual Studio 2022 or `dotnet` CLI

## Functionality / Features

- The web application will create a recipe from user-provided ingredients.
- User registration and login with password hashing (Identity).
- JWT token generation containing user id, username, email and role claims.
- Default role creation and assignment (`User`) during registration.
- Recipe model with validations: name, optional description, instructions, `PrepTimeMinutes` range, `ImageUrl` URL validation.
- Many-to-many relationships: Users ↔ Recipes, Recipes ↔ Ingredients, Recipes ↔ Diets.
- Favorite recipes flag via `UserRecipe.IsFavorite`.
- Error handling middleware for consistent API errors.
- Open AI GPT-4o implemented via Azure Open AI integration for weekly meal planning. 

## API highlights

- `POST /api/auth/register` — register new users (returns JWT).
- `POST /api/auth/login` — login (accepts email or username; returns JWT).
- Protected endpoints require the `Authorization: Bearer <token>` header.
(See `Controllers` in the repo for full list of endpoints.)

## Configuration

Required configuration keys (usually in `appsettings.json` or in Azure App Settings):

- `ConnectionStrings:DefaultConnection` — EF Core connection string (e.g., LocalDB or production DB).
- `Jwt:Key` — symmetric signing key (keep secret).
- `Jwt:Issuer` — token issuer string.
- `Jwt:Audience` — token audience string.
- `Jwt:ExpiresMinutes` — token lifetime in minutes.

Example `appsettings.json` snippet:
{ "ConnectionStrings": { "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=J3MDB;Trusted_Connection=True;MultipleActiveResultSets=true" }, "Jwt": { "Key": "REPLACE_WITH_A_STRONG_SECRET_KEY", "Issuer": "J3m_BE", "Audience": "J3m_BE_Client", "ExpiresMinutes": "60" } }


Security note: use strong secrets in production and store them in Azure App Service settings or Azure Key Vault. Do not commit secret keys to source control.

## Run locally

1. Clone the repo:
   - `git clone https://github.com/MJ-esy/J3m_BE`
2. Open `J3m_BE` in Visual Studio 2022 or use the `dotnet` CLI.
3. Update `appsettings.json` (or user secrets / environment) for `ConnectionStrings` and `Jwt`.
4. Apply EF Core migrations:
   - From Package Manager Console: `Update-Database`
   - Or using CLI: `dotnet ef database update`
5. Run the app:
   - In Visual Studio: press F5 or `Ctrl+F5`.
   - CLI: `dotnet run --project J3m_BE`

The app listens on the configured URLs. Use an API client (Postman / curl) to call the endpoints.

## Deploy to Azure (brief)

1. Create an Azure App Service (or Azure Web App for Containers) or Static Web App (if using a frontend separately).
2. Push code to GitHub (this repo).
3. Use the Azure portal or GitHub Actions for CI/CD deployment to the created App Service.
4. Configure app settings in Azure with the same keys as `appsettings.json` (`ConnectionStrings`, `Jwt` values).
5. After deployment, update the "Deployed application (Azure)" URL at the top of this README.

## Troubleshooting

- If you get authentication errors, verify `Jwt:Key`, `Issuer`, `Audience`, and device/system clock sync.
- If Identity migration or DB issues occur, ensure the connection string points to a valid SQL Server and run `dotnet ef database update`.

# ER Diagram

![ERD](https://github.com/user-attachments/assets/1ecfa6a1-9faf-44dd-9438-c98226483801)

Link : https://lucid.app/lucidchart/aa017ba7-3f55-43b3-bb88-349831ab3336/edit?viewport_loc=-2997%2C244%2C3625%2C1370%2C0_0&invitationId=inv_76d8da71-8d9a-4944-ba46-8d0fc200c6d1

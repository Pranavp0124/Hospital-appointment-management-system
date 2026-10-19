# Hospital Appointment Management System

Simple ASP.NET Core MVC project for CIE-2 Web .NET.

## Features

- Doctor model
- Extension method for total consultation charge
- Async JSONPlaceholder API request
- Cookie for patient name
- Session for specialization
- Query string for Doctor ID
- Hidden field for Doctor ID
- MemoryCache for 3 minutes
- ResponseCache for doctor details
- Bootstrap UI

## Run

Open the folder in VS Code and run:

```bash
dotnet restore
dotnet run
```

Then open the URL shown in the terminal.

## GitHub

```bash
git init
git add .
git commit -m "CIE-2 Hospital Appointment MVC Project"
git branch -M main
git remote add origin YOUR_GITHUB_REPOSITORY_URL
git push -u origin main
```

The project intentionally uses a simple in-memory doctor list instead of a database so it is easy to understand and explain in a CIE/viva.

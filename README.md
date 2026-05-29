# Quản Lý Học Sinh

Hệ thống quản lý học sinh toàn diện, xây dựng trên nền tảng ONENET.

## Tech Stack

| Layer | Công nghệ |
|-------|-----------|
| Runtime | .NET 10 |
| API | ASP.NET Core Controller API |
| CQRS | MediatR |
| Validation | FluentValidation |
| ORM | Entity Framework Core |
| Database | PostgreSQL |
| Frontend | React + Mantine UI |
| Auth | JWT Bearer |
| Logging | Serilog (structured) |
| Container | Docker |
| CI/CD | GitHub Actions |

## Cấu trúc Repo

`
project/
├── apps/
│   ├── web/          # Frontend (React)
│   └── api/          # Backend (.NET API)
├── packages/
│   ├── shared/       # Shared logic, DTOs
│   ├── ui/           # Shared UI components
│   └── config/       # Shared configuration
├── docs/             # Tài liệu dự án
├── scripts/          # Build, deploy scripts
├── tests/            # Integration & E2E tests
└── .github/workflows # CI/CD pipelines
`

## Cài đặt

### Backend
`ash
cd apps/api
dotnet restore
dotnet ef database update
dotnet run
`

### Frontend
`ash
cd apps/web
npm install
npm run dev
`

---
_Dự án được quản lý bởi ONENET AgentFactory._
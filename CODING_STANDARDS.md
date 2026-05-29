# Coding Standards

> Quy chuẩn code bắt buộc cho mọi Developer và Reviewer.

## Naming Convention (C#)

| Loại | Quy tắc | Ví dụ |
|------|---------|-------|
| Class, Interface | PascalCase | `StudentService`, `IStudentRepository` |
| Method | PascalCase + Async | `GetStudentByIdAsync()` |
| Property | PascalCase | `FirstName`, `IsActive` |
| Private field | _camelCase | `_studentRepository`, `_logger` |
| Local variable | camelCase | `studentDto`, `totalCount` |
| Constant | UPPER_SNAKE | `MAX_RETRY_COUNT` |
| Boolean | Is/Has/Can prefix | `IsDeleted`, `HasPermission` |

## Naming Convention (TypeScript/React)

| Loại | Quy tắc | Ví dụ |
|------|---------|-------|
| Component | PascalCase | `StudentListPage` |
| Hook | use prefix | `useStudents()` |
| Function | camelCase | `handleSubmit()` |
| File component | PascalCase.tsx | `StudentListPage.tsx` |

## Quy tắc chung

- Mỗi file chỉ chứa 1 class/component chính
- Tối đa 300 dòng/file
- Tối đa 30 dòng/method
- Tối đa 4 parameter/method
- KHÔNG dùng magic number/string — dùng constant hoặc enum
- KHÔNG để code comment-out

## Error Handling

- Dùng Result Pattern cho business logic (không throw exception)
- Global Exception Middleware cho unhandled errors
- Structured logging: `_logger.LogInformation(""Student created: {StudentId}"", id)`
- KHÔNG dùng string interpolation trong log

## Testing

- Unit test cho mọi Command/Query Handler
- Integration test cho API endpoints
- Naming: `MethodName_Scenario_ExpectedResult`
- Ví dụ: `CreateStudent_ValidInput_ReturnsGuid`

## Database

- Tên bảng: snake_case, số nhiều (`students`)
- Tên cột: snake_case (`first_name`)
- Primary Key: UUID/Guid
- Soft Delete: `is_deleted` + global query filter
- Audit: `created_at`, `created_by`, `updated_at`, `updated_by`

---
_Tham chiếu chi tiết: Technical Guideline của ONENET._
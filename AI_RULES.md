# AI Agent Rules

> Quy định riêng cho AI Agent khi làm việc với repo này.

## Cấu trúc Repo Bắt Buộc

Mọi file code phải tuân theo cấu trúc:

```text
apps/api/src/          # Backend .NET
apps/web/src/          # Frontend React
packages/shared/src/   # Shared DTOs, constants
tests/                 # Unit & Integration tests
scripts/               # Build & deploy scripts
docs/                  # Tài liệu dự án
.github/workflows/     # CI/CD
```

## Output Format

AI Agent CHỈ được trả về code dưới dạng:

```text
[FILE: <đường_dẫn_tương_đối>]
<nội dung file>
[ENDFILE]
```

- KHÔNG viết giải thích ngoài block
- KHÔNG dùng markdown code fence
- Đường dẫn PHẢI theo cấu trúc repo ở trên

## Ví dụ Đường Dẫn Hợp Lệ

- `apps/api/src/ONENET.Domain/Entities/Student.cs`
- `apps/api/src/ONENET.Application/Students/Commands/CreateStudentCommand.cs`
- `apps/api/src/ONENET.Infrastructure/Persistence/StudentRepository.cs`
- `apps/api/src/ONENET.WebAPI/Controllers/StudentsController.cs`
- `apps/web/src/pages/StudentsPage.tsx`
- `apps/web/src/components/StudentForm.tsx`
- `packages/shared/src/DTOs/StudentDto.cs`
- `tests/ONENET.UnitTests/Students/CreateStudentCommandTests.cs`

## Design Patterns Bắt Buộc

- Clean Architecture (Domain → Application → Infrastructure → WebAPI)
- CQRS + MediatR
- Repository Pattern
- FluentValidation
- Result Pattern (không throw exception cho business logic)

## Retry Policy

- Nếu model bị Rate Limit → tự động fallback sang model khác
- Tối đa 3 lần retry cho mỗi agent step
- Nếu tất cả model đều thất bại → trả về thông báo lỗi, KHÔNG dump raw content

## Context Window

- Developer Agent: BRD Summary + SRS/SAD extract (không truyền raw full docs)
- Reviewer Agent: Code + SRS extract + Technical Guideline
- QA Agent: Acceptance Criteria + DEV PR Diff

---
_File này được duy trì bởi ONENET AgentFactory._
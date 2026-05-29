# Contributing Guide

## Branch Convention

| Loại | Format | Ví dụ |
|------|--------|-------|
| Feature | `feature/{dev}/{feature-name}` | `feature/dev1/quan-ly-hoc-sinh` |
| Bugfix | `bugfix/{dev}/{issue-id}` | `bugfix/dev2/fix-login` |
| Hotfix | `hotfix/{version}/{description}` | `hotfix/v1.2/fix-crash` |
| BRD | `{dev}/brd/{project}-{timestamp}` | `vietduc1989/brd/quanlyhocsinh-202605291359` |
| SRS | `{dev}/srs/{project}-{timestamp}` | `vietduc1989/srs/quanlyhocsinh-202605291402` |
| DEV | `{dev}/dev/{project}-{timestamp}` | `vietduc1989/dev/quanlyhocsinh-202605291430` |

## Commit Message Convention

`
<type>(<scope>): <description>

[optional body]
`

**Types:** `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`, `ci`

**Ví dụ:**
- `feat(student): Add CRUD endpoints for student management`
- `fix(auth): Fix JWT token expiration handling`
- `docs(api): Update API documentation for v2`

## Pull Request Flow

1. Tạo branch từ `dev`
2. Commit theo convention ở trên
3. Tạo PR vào `dev` với title format: `[BRD|SRS|DEV|TEST|DEVOPS] Tên tính năng`
4. Mỗi PR chỉ phục vụ 1 mục tiêu duy nhất
5. Giữ trong khoảng 300-500 dòng thay đổi
6. Chờ ít nhất 1 Reviewer approve
7. Squash merge vào `dev`

## Code Review Checklist

- [ ] Code tuân thủ Clean Architecture (dependency rule)
- [ ] Naming convention đúng quy chuẩn
- [ ] Không có magic number/string
- [ ] Có validation đầu vào (FluentValidation)
- [ ] Có error handling hợp lý
- [ ] Có structured logging
- [ ] Soft delete thay vì hard delete
- [ ] API response đúng format chuẩn
- [ ] Không có N+1 query
- [ ] Test coverage cho critical path

---
_Tham chiếu chi tiết: Technical Guideline của ONENET._
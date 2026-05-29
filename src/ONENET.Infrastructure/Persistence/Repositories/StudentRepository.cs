/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Infrastructure (Repositories)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);
        }

        public async Task<Student?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.StudentCode == code && !s.IsDeleted, cancellationToken);
        }

        public async Task<(IEnumerable<Student> Items, int TotalCount)> GetPagedAsync(
            string? searchQuery, 
            int page, 
            int pageSize, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<Student> query = _context.Students.Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var search = searchQuery.Trim().ToLower();
                query = query.Where(s => s.FullName.ToLower().Contains(search) || s.StudentCode.ToLower() == search);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<IEnumerable<Student>> GetAllActiveAsync(string? searchQuery, CancellationToken cancellationToken = default)
        {
            IQueryable<Student> query = _context.Students.Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var search = searchQuery.Trim().ToLower();
                query = query.Where(s => s.FullName.ToLower().Contains(search) || s.StudentCode.ToLower() == search);
            }

            return await query.OrderByDescending(s => s.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
        {
            await _context.Students.AddAsync(student, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<Student> students, CancellationToken cancellationToken = default)
        {
            await _context.Students.AddRangeAsync(students, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        // BR-01: Phát hiện và sinh mã học sinh duy nhất (Serializable transaction block)
        public async Task<string> GenerateNextStudentCodeAsync(DateOnly date, CancellationToken cancellationToken = default)
        {
            var dateStr = date.ToString("yyyyMMdd");
            var prefix = $"HS-{dateStr}-";

            // Khởi tạo Transaction Serializable để triệt tiêu lỗi trùng mã (Zero Duplication Rate)
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
            try
            {
                // Lấy ra mã có số thứ tự lớn nhất trong ngày hiện tại
                var maxCodeStudent = await _context.Students
                    .Where(s => s.StudentCode.StartsWith(prefix))
                    .OrderByDescending(s => s.StudentCode)
                    .Select(s => s.StudentCode)
                    .FirstOrDefaultAsync(cancellationToken);

                int nextNumber = 1;
                if (!string.IsNullOrEmpty(maxCodeStudent))
                {
                    var lastPart = maxCodeStudent.Substring(prefix.Length);
                    if (int.TryParse(lastPart, out int currentNumber))
                    {
                        nextNumber = currentNumber + 1;
                    }
                }

                await transaction.CommitAsync(cancellationToken);
                return $"{prefix}{nextNumber:D4}";
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
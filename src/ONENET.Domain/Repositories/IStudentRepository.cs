/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Domain (Interfaces)
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ONENET.Domain.Entities;

namespace ONENET.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Student?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<(IEnumerable<Student> Items, int TotalCount)> GetPagedAsync(
            string? searchQuery, 
            int page, 
            int pageSize, 
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Student>> GetAllActiveAsync(string? searchQuery, CancellationToken cancellationToken = default);
        Task AddAsync(Student student, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<Student> students, CancellationToken cancellationToken = default);
        void Update(Student student);
        Task<string> GenerateNextStudentCodeAsync(DateOnly date, CancellationToken cancellationToken = default);
    }
}
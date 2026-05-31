// QUAN-20260530-2301
using ONENET.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default);
        IQueryable<Student> GetAll();
        Task AddAsync(Student student, CancellationToken ct = default);
        void Update(Student student);
        void Delete(Student student);
        Task<bool> ExistsByMaHocSinhAsync(string maHocSinh, Guid? excludeId = null, CancellationToken ct = default);
        
        // QUAN-20260530-2301-FR06: Check for related data
        Task<bool> HasRelatedDataAsync(Guid studentId, CancellationToken ct = default);
    }
}
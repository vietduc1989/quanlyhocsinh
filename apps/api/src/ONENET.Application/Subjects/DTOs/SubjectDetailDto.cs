// QUAN-20260531-154643
using AutoMapper;
using ONENET.Application.Common.Mappings;
using ONENET.Domain.Entities;

namespace ONENET.Application.Subjects.DTOs
{
    public class SubjectDetailDto : IMapFrom<Subject>
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int Credits { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subject, SubjectDetailDto>();
        }
    }
}
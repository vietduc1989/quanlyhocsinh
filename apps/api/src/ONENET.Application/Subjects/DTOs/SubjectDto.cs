// QUAN-20260531-154643
using AutoMapper;
using ONENET.Application.Common.Mappings;
using ONENET.Domain.Entities;

namespace ONENET.Application.Subjects.DTOs
{
    public class SubjectDto : IMapFrom<Subject>
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Credits { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subject, SubjectDto>();
        }
    }
}
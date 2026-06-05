// QUAN-20260604-153038
using AutoMapper;
using ONENET.Application.Common.Mappings;
using ONENET.Domain.Entities;
using System;

namespace ONENET.Application.Features.LopHocs.DTOs
{
    public class LopHocLookupDto : IMapFrom<LopHoc>
    {
        public Guid Id { get; set; }
        public string TenLop { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LopHoc, LopHocLookupDto>();
        }
    }
}
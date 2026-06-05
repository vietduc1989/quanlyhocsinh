// QUAN-20260604-153038
using AutoMapper;
using ONENET.Application.Common.Mappings;
using ONENET.Domain.Entities;
using ONENET.Domain.Enums;
using System;

namespace ONENET.Application.Features.HocSinhs.DTOs
{
    public class HocSinhDto : IMapFrom<HocSinh>
    {
        public Guid Id { get; set; }
        public string MaHocSinh { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public DateOnly NgaySinh { get; set; }
        public string GioiTinh { get; set; } = string.Empty; // Display enum as string
        public string? DiaChi { get; set; }
        public string? SoDienThoaiPH { get; set; }
        public string? EmailPH { get; set; }
        public Guid LopHocId { get; set; }
        public string TenLop { get; set; } = string.Empty; // From LopHoc
        public string TrangThai { get; set; } = string.Empty; // Display enum as string
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HocSinh, HocSinhDto>()
                .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => src.GioiTinh.ToString()))
                .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai.ToString()))
                .ForMember(dest => dest.TenLop, opt => opt.MapFrom(src => src.LopHoc.TenLop));
        }
    }
}
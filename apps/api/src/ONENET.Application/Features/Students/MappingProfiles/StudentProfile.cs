// QUAN-20260530-2301
using AutoMapper;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;

namespace ONENET.Application.Features.Students.MappingProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Map Entity to DTOs
            CreateMap<Student, StudentSummaryDto>()
                .ForMember(dest => dest.TenLop, opt => opt.MapFrom(src => src.Lop.TenLop))
                .ForMember(dest => dest.TenTrangThai, opt => opt.MapFrom(src => src.TrangThaiHocSinh.TenTrangThai));

            CreateMap<Student, StudentDetailDto>()
                .ForMember(dest => dest.TenLop, opt => opt.MapFrom(src => src.Lop.TenLop))
                .ForMember(dest => dest.TenTrangThai, opt => opt.MapFrom(src => src.TrangThaiHocSinh.TenTrangThai));

            // Map DTO to Entity (for creation - not directly used by AutoMapper, but good for completeness)
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
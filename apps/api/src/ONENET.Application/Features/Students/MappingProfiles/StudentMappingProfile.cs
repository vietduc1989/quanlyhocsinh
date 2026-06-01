using AutoMapper;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Enums;

namespace ONENET.Application.Features.Students.MappingProfiles
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            // Map Student entity to StudentDto (for list view)
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.LopHoc, opt => opt.MapFrom(src => src.Lop.TenLop))
                .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai.TenTrangThai));

            // Map Student entity to StudentDetailDto (for detail view)
            CreateMap<Student, StudentDetailDto>()
                .ForMember(dest => dest.TenLop, opt => opt.MapFrom(src => src.Lop.TenLop))
                .ForMember(dest => dest.TenTrangThai, opt => opt.MapFrom(src => src.TrangThai.TenTrangThai))
                .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => src.GioiTinh.ToString())); // Convert enum to string

            // Map CreateStudentDto to CreateStudentCommand (if needed, but DTO directly used in command for this feature)
            // If CreateStudentCommand takes more simplified types and DTO is richer, then this is useful.
            // Currently, CreateStudentDto is almost identical to CreateStudentCommand parameters.
            // Direct use of CreateStudentCommand for API request body is more efficient here.

            // Map Lop to LookupDto
            CreateMap<Lop, LookupDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TenLop));
            
            // Map TrangThaiHocSinh to LookupDto
            CreateMap<TrangThaiHocSinh, LookupDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TenTrangThai));
        }
    }
}

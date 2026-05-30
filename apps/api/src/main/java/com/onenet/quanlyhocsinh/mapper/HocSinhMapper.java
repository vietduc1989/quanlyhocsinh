// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.mapper;

import com.onenet.quanlyhocsinh.dto.StudentRequest;
import com.onenet.quanlyhocsinh.dto.StudentResponse;
import com.onenet.quanlyhocsinh.entity.HocSinh;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.MappingTarget;
import org.mapstruct.ReportingPolicy;

@Mapper(componentModel = "spring", unmappedTargetPolicy = ReportingPolicy.IGNORE)
public interface HocSinhMapper {

    @Mapping(target = "ngayTao", ignore = true)
    @Mapping(target = "nguoiTao", ignore = true)
    @Mapping(target = "ngayCapNhatCuoi", ignore = true)
    @Mapping(target = "nguoiCapNhatCuoi", ignore = true)
    HocSinh toEntity(StudentRequest studentRequest);

    StudentResponse toDto(HocSinh hocSinh);

    @Mapping(target = "maHocSinh", ignore = true) // MaHocSinh is not updatable via this method
    @Mapping(target = "ngayTao", ignore = true)
    @Mapping(target = "nguoiTao", ignore = true)
    void updateEntityFromDto(StudentRequest studentRequest, @MappingTarget HocSinh hocSinh);
}
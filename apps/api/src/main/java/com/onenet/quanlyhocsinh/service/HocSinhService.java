// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.service;

import com.onenet.quanlyhocsinh.dto.StudentRequest;
import com.onenet.quanlyhocsinh.dto.StudentResponse;
import com.onenet.quanlyhocsinh.entity.HocSinh;
import com.onenet.quanlyhocsinh.exception.DuplicateResourceException;
import com.onenet.quanlyhocsinh.exception.ResourceNotFoundException;
import com.onenet.quanlyhocsinh.mapper.HocSinhMapper;
import com.onenet.quanlyhocsinh.repository.HocSinhRepository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.Period;

@Service
public class HocSinhService {

    private final HocSinhRepository hocSinhRepository;
    private final HocSinhMapper hocSinhMapper;
    private final LopHocClient lopHocClient;

    public HocSinhService(HocSinhRepository hocSinhRepository, HocSinhMapper hocSinhMapper, LopHocClient lopHocClient) {
        this.hocSinhRepository = hocSinhRepository;
        this.hocSinhMapper = hocSinhMapper;
        this.lopHocClient = lopHocClient;
    }

    @Transactional(readOnly = true)
    public Page<StudentResponse> searchAndPaginateStudents(String keyword, String maLopHoc, Pageable pageable) {
        // Filter out 'Đã xóa' status by default. @Where clause in entity handles this.
        return hocSinhRepository.searchStudents(keyword, maLopHoc, pageable)
                .map(hocSinhMapper::toDto);
    }

    @Transactional(readOnly = true)
    public StudentResponse getStudentByMaHocSinh(String maHocSinh) {
        HocSinh hocSinh = hocSinhRepository.findByMaHocSinh(maHocSinh)
                .orElseThrow(() -> new ResourceNotFoundException("Học sinh với mã " + maHocSinh + " không tìm thấy."));
        return hocSinhMapper.toDto(hocSinh);
    }

    @Transactional
    public StudentResponse createStudent(StudentRequest studentRequest) {
        if (hocSinhRepository.existsByMaHocSinh(studentRequest.getMaHocSinh())) {
            throw new DuplicateResourceException("Mã học sinh " + studentRequest.getMaHocSinh() + " đã tồn tại.");
        }
        validateStudentAge(studentRequest.getNgaySinh());
        // Custom validation for MaLopHoc existence is handled by @ValidMaLopHoc custom annotation

        HocSinh hocSinh = hocSinhMapper.toEntity(studentRequest);
        // Default status if not provided, or ensure it's valid
        if (hocSinh.getTrangThai() == null || hocSinh.getTrangThai().isEmpty()) {
            hocSinh.setTrangThai("Đang học");
        }

        HocSinh savedHocSinh = hocSinhRepository.save(hocSinh);
        return hocSinhMapper.toDto(savedHocSinh);
    }

    @Transactional
    public StudentResponse updateStudent(String maHocSinh, StudentRequest studentRequest) {
        HocSinh existingHocSinh = hocSinhRepository.findByMaHocSinh(maHocSinh)
                .orElseThrow(() -> new ResourceNotFoundException("Học sinh với mã " + maHocSinh + " không tìm thấy."));

        validateStudentAge(studentRequest.getNgaySinh());
        // Custom validation for MaLopHoc existence is handled by @ValidMaLopHoc custom annotation

        hocSinhMapper.updateEntityFromDto(studentRequest, existingHocSinh);
        HocSinh updatedHocSinh = hocSinhRepository.save(existingHocSinh);
        return hocSinhMapper.toDto(updatedHocSinh);
    }

    @Transactional
    public void softDeleteStudent(String maHocSinh) {
        HocSinh hocSinh = hocSinhRepository.findByMaHocSinh(maHocSinh)
                .orElseThrow(() -> new ResourceNotFoundException("Học sinh với mã " + maHocSinh + " không tìm thấy."));

        // Implement soft delete by changing status
        hocSinh.setTrangThai("Đã xóa");
        hocSinhRepository.save(hocSinh);
    }

    private void validateStudentAge(LocalDate ngaySinh) {
        LocalDate today = LocalDate.now();
        int age = Period.between(ngaySinh, today).getYears();
        if (age < 5 || age > 20) {
            throw new com.onenet.quanlyhocsinh.exception.ValidationException("Tuổi học sinh phải nằm trong khoảng từ 5 đến 20 tuổi.");
        }
    }
}
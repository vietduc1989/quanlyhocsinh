// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.controller;

import com.onenet.quanlyhocsinh.dto.PaginationResponse;
import com.onenet.quanlyhocsinh.dto.StudentRequest;
import com.onenet.quanlyhocsinh.dto.StudentResponse;
import com.onenet.quanlyhocsinh.service.HocSinhService;
import jakarta.validation.Valid;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Sort;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/students")
public class StudentController {

    private final HocSinhService hocSinhService;

    public StudentController(HocSinhService hocSinhService) {
        this.hocSinhService = hocSinhService;
    }

    @PreAuthorize("hasAnyRole('ADMIN', 'TEACHER')")
    @GetMapping
    public ResponseEntity<PaginationResponse<StudentResponse>> getAllStudents(
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(defaultValue = "maHocSinh") String sortBy,
            @RequestParam(defaultValue = "asc") String sortDir,
            @RequestParam(required = false) String keyword,
            @RequestParam(required = false) String maLopHoc) {

        Sort.Direction direction = sortDir.equalsIgnoreCase("desc") ? Sort.Direction.DESC : Sort.Direction.ASC;
        PageRequest pageRequest = PageRequest.of(page, size, Sort.by(direction, sortBy));

        Page<StudentResponse> studentsPage = hocSinhService.searchAndPaginateStudents(
                keyword, maLopHoc, pageRequest);

        PaginationResponse<StudentResponse> response = new PaginationResponse<>(
                studentsPage.getContent(),
                studentsPage.getNumber(),
                studentsPage.getSize(),
                studentsPage.getTotalElements(),
                studentsPage.getTotalPages(),
                studentsPage.isLast()
        );
        return ResponseEntity.ok(response);
    }

    @PreAuthorize("hasAnyRole('ADMIN', 'TEACHER')")
    @GetMapping("/{maHocSinh}")
    public ResponseEntity<StudentResponse> getStudentById(@PathVariable String maHocSinh) {
        StudentResponse student = hocSinhService.getStudentByMaHocSinh(maHocSinh);
        return ResponseEntity.ok(student);
    }

    @PreAuthorize("hasRole('ADMIN')")
    @PostMapping
    public ResponseEntity<StudentResponse> createStudent(@Valid @RequestBody StudentRequest studentRequest) {
        StudentResponse createdStudent = hocSinhService.createStudent(studentRequest);
        return new ResponseEntity<>(createdStudent, HttpStatus.CREATED);
    }

    @PreAuthorize("hasRole('ADMIN')")
    @PutMapping("/{maHocSinh}")
    public ResponseEntity<StudentResponse> updateStudent(@PathVariable String maHocSinh,
                                                         @Valid @RequestBody StudentRequest studentRequest) {
        StudentResponse updatedStudent = hocSinhService.updateStudent(maHocSinh, studentRequest);
        return ResponseEntity.ok(updatedStudent);
    }

    @PreAuthorize("hasRole('ADMIN')")
    @DeleteMapping("/{maHocSinh}")
    public ResponseEntity<Void> deleteStudent(@PathVariable String maHocSinh) {
        hocSinhService.softDeleteStudent(maHocSinh);
        return ResponseEntity.noContent().build();
    }
}
// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.repository;

import com.onenet.quanlyhocsinh.entity.HocSinh;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface HocSinhRepository extends JpaRepository<HocSinh, String> {

    Optional<HocSinh> findByMaHocSinh(String maHocSinh);

    boolean existsByMaHocSinh(String maHocSinh);

    // Custom query for searching and pagination
    @Query("SELECT hs FROM HocSinh hs WHERE " +
           "(LOWER(hs.maHocSinh) LIKE LOWER(CONCAT('%', :keyword, '%')) OR " +
           "LOWER(hs.hoTen) LIKE LOWER(CONCAT('%', :keyword, '%')) OR " +
           "LOWER(hs.maLopHoc) LIKE LOWER(CONCAT('%', :keyword, '%')) OR " +
           ":keyword IS NULL) AND " +
           "(LOWER(hs.maLopHoc) LIKE LOWER(CONCAT('%', :maLopHoc, '%')) OR :maLopHoc IS NULL)")
    Page<HocSinh> searchStudents(
            @Param("keyword") String keyword,
            @Param("maLopHoc") String maLopHoc,
            Pageable pageable);
}
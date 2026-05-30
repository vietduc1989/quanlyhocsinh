// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class LopHocResponse {
    private String maLopHoc;
    private String tenLop;
    private String khoiHoc;
    // Add other relevant fields from LopHoc module if needed for display/validation
}
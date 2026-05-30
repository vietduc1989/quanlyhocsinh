// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.service;

import com.onenet.quanlyhocsinh.dto.LopHocResponse;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

@FeignClient(name = "lopHocService", url = "${lopHoc.service.url}")
public interface LopHocClient {

    @GetMapping("/{maLopHoc}")
    LopHocResponse getLopHocByMa(@PathVariable("maLopHoc") String maLopHoc);

    // Consider adding an endpoint to check existence directly for performance if the LopHoc service supports it
    @GetMapping("/exists/{maLopHoc}")
    boolean checkLopHocExists(@PathVariable("maLopHoc") String maLopHoc);
}
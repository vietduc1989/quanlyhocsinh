// QUAN-20260530-0942
package com.onenet.quanlyhocsinh;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.openfeign.EnableFeignClients;
import org.springframework.data.jpa.repository.config.EnableJpaAuditing;

@SpringBootApplication
@EnableJpaAuditing(auditorAwareRef = "auditorAwareImpl")
@EnableFeignClients
public class QuanLyHocSinhApplication {

    public static void main(String[] args) {
        SpringApplication.run(QuanLyHocSinhApplication.class, args);
    }

}
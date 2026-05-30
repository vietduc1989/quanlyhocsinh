// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.validation;

import com.onenet.quanlyhocsinh.service.LopHocClient;
import feign.FeignException;
import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;
import org.springframework.stereotype.Component;

@Component
public class MaLopHocValidator implements ConstraintValidator<ValidMaLopHoc, String> {

    private final LopHocClient lopHocClient;

    // Use constructor injection for dependencies
    public MaLopHocValidator(LopHocClient lopHocClient) {
        this.lopHocClient = lopHocClient;
    }

    @Override
    public void initialize(ValidMaLopHoc constraintAnnotation) {
        // No initialization needed
    }

    @Override
    public boolean isValid(String maLopHoc, ConstraintValidatorContext context) {
        if (maLopHoc == null || maLopHoc.isBlank()) {
            // @NotBlank already handles this, but ensures custom validator also works if it's the only constraint
            return false;
        }
        try {
            // Call the LopHoc module to check if the MaLopHoc exists
            // Using checkLopHocExists if available for better performance,
            // otherwise, fallback to getLopHocByMa and check for null/exception.
            return lopHocClient.checkLopHocExists(maLopHoc);
        } catch (FeignException.NotFound e) {
            // LopHoc not found, means invalid MaLopHoc
            return false;
        } catch (FeignException e) {
            // Handle other Feign exceptions, e.g., service unavailable
            // Log the error and possibly fail validation or return true based on business rules
            context.disableDefaultConstraintViolation();
            context.buildConstraintViolationWithTemplate("Không thể kiểm tra Mã lớp học do lỗi hệ thống Lớp Học. Vui lòng thử lại sau.")
                    .addConstraintViolation();
            return false;
        } catch (Exception e) {
            // Catch any other unexpected exceptions
            context.disableDefaultConstraintViolation();
            context.buildConstraintViolationWithTemplate("Đã xảy ra lỗi khi kiểm tra Mã lớp học: " + e.getMessage())
                    .addConstraintViolation();
            return false;
        }
    }
}
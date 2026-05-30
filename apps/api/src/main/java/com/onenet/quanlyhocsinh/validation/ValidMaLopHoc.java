// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.validation;

import jakarta.validation.Constraint;
import jakarta.validation.Payload;

import java.lang.annotation.*;

@Documented
@Constraint(validatedBy = MaLopHocValidator.class)
@Target({ElementType.FIELD, ElementType.PARAMETER})
@Retention(RetentionPolicy.RUNTIME)
public @interface ValidMaLopHoc {
    String message() default "Mã lớp học không tồn tại trong hệ thống.";
    Class<?>[] groups() default {};
    Class<? extends Payload>[] payload() default {};
}
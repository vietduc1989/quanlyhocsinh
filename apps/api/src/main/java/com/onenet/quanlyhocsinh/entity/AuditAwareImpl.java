// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.entity;

import com.onenet.quanlyhocsinh.service.AuthService;
import org.springframework.data.domain.AuditorAware;
import org.springframework.stereotype.Component;

import java.util.Optional;

@Component
public class AuditAwareImpl implements AuditorAware<String> {

    private final AuthService authService; // Assuming an AuthService exists to get current user info

    public AuditAwareImpl(AuthService authService) {
        this.authService = authService;
    }

    @Override
    public Optional<String> getCurrentAuditor() {
        // This method should return the username/id of the currently logged-in user.
        // In a real ONENET application, this would retrieve user info from Spring SecurityContext
        // which has been populated by the ONENET AuthN/AuthZ system (e.g., from a JWT token).
        return Optional.of(authService.getCurrentUser());
    }
}
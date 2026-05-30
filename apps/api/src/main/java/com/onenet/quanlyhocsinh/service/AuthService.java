// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.service;

import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

@Service
public class AuthService {
    // This is a placeholder for actual ONENET AuthN/AuthZ integration.
    // In a real ONENET application, this service would interact with
    // the security context populated by ONENET's authentication mechanism (e.g., JWT token parsing).

    public String getCurrentUser() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        if (authentication == null || !authentication.isAuthenticated()) {
            return "anonymousUser"; // Default for unauthenticated or system operations
        }
        Object principal = authentication.getPrincipal();
        if (principal instanceof UserDetails userDetails) {
            return userDetails.getUsername();
        } else if (principal instanceof String) {
            return (String) principal;
        }
        return "unknown"; // Fallback
    }

    public boolean hasRole(String role) {
        // Placeholder method to check if current user has a specific role.
        // In a real application, this would query the Authentication object for authorities.
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        return authentication != null && authentication.getAuthorities().stream()
                .anyMatch(a -> a.getAuthority().equals("ROLE_" + role.toUpperCase()));
    }
}
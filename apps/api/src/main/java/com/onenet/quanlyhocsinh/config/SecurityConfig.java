// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@Configuration
@EnableWebSecurity
@EnableMethodSecurity(prePostEnabled = true) // Enable @PreAuthorize
public class SecurityConfig {

    // Placeholder for ONENET's AuthN/AuthZ integration.
    // In a real scenario, this would integrate with ONENET's JWT/OAuth2
    // or a custom authentication filter provided by ONENET.
    // For this task, we'll assume a basic JWT setup or direct role injection for @PreAuthorize to work.

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http
            .csrf(csrf -> csrf.disable()) // Disable CSRF for stateless REST APIs
            .authorizeHttpRequests(authorize -> authorize
                .requestMatchers("/api/auth/**").permitAll() // Example public endpoints
                .requestMatchers("/api/students/**").authenticated() // All student endpoints require authentication
                .anyRequest().authenticated()
            )
            .sessionManagement(session -> session
                .sessionCreationPolicy(SessionCreationPolicy.STATELESS) // REST API is stateless
            );
            // .addFilterBefore(jwtRequestFilter, UsernamePasswordAuthenticationFilter.class); // Example JWT filter

        return http.build();
    }

    // Placeholder for UserDetailsService and PasswordEncoder if local authentication is used
    // In ONENET, this would typically involve an external identity provider
    /*
    @Bean
    public UserDetailsService userDetailsService() {
        // This is a minimal in-memory user for demonstration.
        // Replace with actual integration with ONENET's user management.
        UserDetails admin = User.builder()
                .username("admin")
                .password(passwordEncoder().encode("adminpass"))
                .roles("ADMIN")
                .build();
        UserDetails teacher = User.builder()
                .username("teacher")
                .password(passwordEncoder().encode("teacherpass"))
                .roles("TEACHER")
                .build();
        return new InMemoryUserDetailsManager(admin, teacher);
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

    @Bean
    public AuthenticationManager authenticationManager(AuthenticationConfiguration authenticationConfiguration) throws Exception {
        return authenticationConfiguration.getAuthenticationManager();
    }
    */
}
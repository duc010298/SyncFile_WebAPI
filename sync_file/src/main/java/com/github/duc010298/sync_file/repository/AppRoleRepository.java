package com.github.duc010298.sync_file.repository;

import com.github.duc010298.sync_file.entity.AppRole;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;
import java.util.UUID;

public interface AppRoleRepository extends JpaRepository<AppRole, Long> {

    @Query("SELECT r.roleName FROM AppRole r JOIN r.appUsers u WHERE u.userId = ?1")
    List<String> getRoleNames(UUID userId);
}
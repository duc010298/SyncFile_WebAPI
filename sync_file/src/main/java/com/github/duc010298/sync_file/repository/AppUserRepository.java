package com.github.duc010298.sync_file.repository;

import com.github.duc010298.sync_file.entity.AppUser;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AppUserRepository extends JpaRepository<AppUser, Long> {

	AppUser findByUserName(String userName);
}

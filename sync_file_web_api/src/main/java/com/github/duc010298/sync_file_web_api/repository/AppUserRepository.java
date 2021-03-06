package com.github.duc010298.sync_file_web_api.repository;

import com.github.duc010298.sync_file_web_api.entity.AppUser;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AppUserRepository extends JpaRepository<AppUser, Long> {

	AppUser findByUserName(String userName);
}

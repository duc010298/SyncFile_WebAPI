package com.github.duc010298.sync_file.entity;

import java.io.Serializable;
import javax.persistence.*;
import java.util.List;
import java.util.UUID;


/**
 * The persistent class for the app_user database table.
 * 
 */
@Entity
@Table(name = "app_user", schema = "public")
@NamedQuery(name="AppUser.findAll", query="SELECT a FROM AppUser a")
public class AppUser implements Serializable {
	private static final long serialVersionUID = 1L;
	private UUID userId;
	private String encryptedPassword;
	private String userName;
	private List<AppRole> appRoles;

	public AppUser() {
	}


	@Id
	@Column(name="user_id")
	@org.hibernate.annotations.Type(type = "pg-uuid")
	public UUID getUserId() {
		return this.userId;
	}

	public void setUserId(UUID userId) {
		this.userId = userId;
	}


	@Column(name="encrypted_password")
	public String getEncryptedPassword() {
		return this.encryptedPassword;
	}

	public void setEncryptedPassword(String encryptedPassword) {
		this.encryptedPassword = encryptedPassword;
	}


	@Column(name="user_name")
	public String getUserName() {
		return this.userName;
	}

	public void setUserName(String userName) {
		this.userName = userName;
	}


	//bi-directional many-to-many association to AppRole
	@ManyToMany
	@JoinTable(
		name="user_role"
		, joinColumns={
			@JoinColumn(name="user_id")
			}
		, inverseJoinColumns={
			@JoinColumn(name="role_id")
			}
		)
	public List<AppRole> getAppRoles() {
		return this.appRoles;
	}

	public void setAppRoles(List<AppRole> appRoles) {
		this.appRoles = appRoles;
	}

}
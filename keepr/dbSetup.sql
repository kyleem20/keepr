CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  name TEXT NOT NULL COMMENT 'Name of Keep',
  description TEXT NOT NULL,
  views INT Not Null Default 0,
  keeps INT Not Null Default 0,
  shares INT Not Null Default 0,
  img TEXT,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  name TEXT NOT NULL COMMENT 'Name of Vault',
  description TEXT,
  isPrivate BOOLEAN,
  img TEXT,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  vaultId INT NOT NULL,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  keepId INT NOT NULL,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
Alter Table
  keeps
Modify
  Column shares INT Not Null Default 0;
Select
  *
From
  keeps
WHERE
  id = 25;
-- Active: 1702936188373@@34.214.18.131@3306@Keepr

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        bio varchar(255) COMMENT 'user bio',
        coverImg VARCHAR(1000)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS profiles(
        id VARCHAR(255) NOT NULL PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(255),
        picture VARCHAR(255)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name CHAR(255) NOT NULL,
        description VARCHAR(1000) NOT NULL,
        img VARCHAR(1000) NOT NULL,
        views INT NOT NULL DEFAULT 0,
        kept INT NOT NULL DEFAULT 0,
        creatorId CHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name CHAR(150) NOT NULL,
        description VARCHAR(700) NOT NULL,
        img VARCHAR(1000) NOT NULL,
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        creatorId CHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

SELECT * FROM keeps

SELECT * FROM vaults

SELECT * FROM vaultkeeps

SELECT * FROM profiles

SELECT * FROM accounts

DROP TABLE keeps

DROP TABLE vaults

DROP TABLE vaultkeeps

DROP TABLE accounts

DROP TABLE profiles

INSERT INTO
    keeps(
        name,
        description,
        img,
        views,
        kept,
        creatorId
    )
VALUES (
        "Good Stuff",
        "When you believe",
        "https://images.unsplash.com/photo-1633436374961-09b92742047b?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fExhc2FnbmF8ZW58MHx8MHx8fDA%3D",
        0,
        0,
        "652ef49aff9881f8395eb350"
    )

INSERT INTO
    accounts(
        name,
        email,
        picture,
        bio,
        coverImg
    )
VALUES (
        "Laura",
        "lauraalspaugh2002@icloud.com",
        "https://images.unsplash.com/photo-1617659258448-f2711883be96?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJsYWNrJTIwYW5kJTIwd2hpdGUlMjBhZXN0aGV0aWN8ZW58MHx8MHx8fDA%3D",
        "Hey there. Trust the process. No one finds success overnight....trust it ",
        "https://s.gravatar.com/avatar/b43abc76c68151fbc8b3d734acfcfc64?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fho.png"
    )
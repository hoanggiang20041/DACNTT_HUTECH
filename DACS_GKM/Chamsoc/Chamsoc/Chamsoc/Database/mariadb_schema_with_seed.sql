CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetRoles` (
        `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetUsers` (
        `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `RoleId` longtext CHARACTER SET utf8mb4 NOT NULL,
        `FullName` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Address` longtext CHARACTER SET utf8mb4 NOT NULL,
        `DateOfBirth` datetime(6) NOT NULL,
        `Gender` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Avatar` longtext CHARACTER SET utf8mb4 NULL,
        `IsActive` tinyint(1) NOT NULL,
        `Role` longtext CHARACTER SET utf8mb4 NULL,
        `IsLocked` tinyint(1) NOT NULL,
        `Balance` decimal(65,30) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NULL,
        `LastLoginAt` datetime(6) NULL,
        `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
        `EmailConfirmed` tinyint(1) NOT NULL,
        `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
        `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
        `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
        `PhoneNumberConfirmed` tinyint(1) NOT NULL,
        `TwoFactorEnabled` tinyint(1) NOT NULL,
        `LockoutEnd` datetime(6) NULL,
        `LockoutEnabled` tinyint(1) NOT NULL,
        `AccessFailedCount` int NOT NULL,
        CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Services` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Description` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
        `BasePrice` decimal(18,2) NOT NULL,
        `IsActive` tinyint(1) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NULL,
        CONSTRAINT `PK_Services` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetRoleClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetUserClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetUserLogins` (
        `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
        CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetUserRoles` (
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
        CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `AspNetUserTokens` (
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Value` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
        CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Caregivers` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Skills` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
        `IsAvailable` tinyint(1) NOT NULL,
        `Degree` longtext CHARACTER SET utf8mb4 NULL,
        `Certificate` longtext CHARACTER SET utf8mb4 NULL,
        `IdentityAndHealthDocs` longtext CHARACTER SET utf8mb4 NULL,
        `RegistrationDate` datetime(6) NOT NULL,
        `IsVerified` tinyint(1) NOT NULL,
        `Rating` decimal(18,2) NOT NULL,
        `CompletedJobs` int NOT NULL,
        `HourlyRate` decimal(18,2) NOT NULL,
        `Experience` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Pricing` longtext CHARACTER SET utf8mb4 NOT NULL,
        `TotalRatings` double NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `Contact` longtext CHARACTER SET utf8mb4 NULL,
        `AvatarUrl` longtext CHARACTER SET utf8mb4 NULL,
        `CertificateFilePath` longtext CHARACTER SET utf8mb4 NULL,
        `Price` decimal(65,30) NOT NULL,
        `FullName` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Caregivers` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Caregivers_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Seniors` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Age` int NOT NULL,
        `CareNeeds` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
        `RegistrationDate` datetime(6) NOT NULL,
        `Status` tinyint(1) NOT NULL,
        `IsVerified` tinyint(1) NOT NULL,
        `Price` decimal(18,2) NOT NULL,
        `IdentityAndHealthDocs` longtext CHARACTER SET utf8mb4 NULL,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `AvatarUrl` longtext CHARACTER SET utf8mb4 NULL,
        `FullName` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Seniors` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Seniors_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `CareJobs` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `SeniorId` int NOT NULL,
        `CaregiverId` int NULL,
        `StartTime` datetime(6) NOT NULL,
        `EndTime` datetime(6) NOT NULL,
        `ServiceType` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Description` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
        `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `TotalBill` decimal(18,2) NOT NULL,
        `DepositAmount` decimal(18,2) NOT NULL,
        `Deposit` decimal(18,2) NOT NULL,
        `RemainingAmount` decimal(18,2) NOT NULL,
        `IsDepositPaid` tinyint(1) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NOT NULL,
        `CreatedByRole` longtext CHARACTER SET utf8mb4 NOT NULL,
        `DepositMade` tinyint(1) NOT NULL,
        `DepositNote` longtext CHARACTER SET utf8mb4 NOT NULL,
        `PaymentStatus` longtext CHARACTER SET utf8mb4 NOT NULL,
        `PaymentMethod` longtext CHARACTER SET utf8mb4 NOT NULL,
        `PaymentTime` datetime(6) NULL,
        `CompletedAt` datetime(6) NULL,
        `Rating` decimal(65,30) NULL,
        `Review` varchar(500) CHARACTER SET utf8mb4 NULL,
        `ServiceId` int NOT NULL,
        `Location` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Latitude` double NOT NULL,
        `Longitude` double NOT NULL,
        `CaregiverAccepted` tinyint(1) NOT NULL,
        `SeniorAccepted` tinyint(1) NOT NULL,
        `CaregiverName` longtext CHARACTER SET utf8mb4 NOT NULL,
        `SeniorName` longtext CHARACTER SET utf8mb4 NOT NULL,
        `SeniorPhone` longtext CHARACTER SET utf8mb4 NOT NULL,
        `HasRated` tinyint(1) NULL,
        `HasComplained` tinyint(1) NULL,
        CONSTRAINT `PK_CareJobs` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_CareJobs_Caregivers_CaregiverId` FOREIGN KEY (`CaregiverId`) REFERENCES `Caregivers` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_CareJobs_Seniors_SeniorId` FOREIGN KEY (`SeniorId`) REFERENCES `Seniors` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_CareJobs_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `Services` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Complaints` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `JobId` int NOT NULL,
        `CaregiverId` int NOT NULL,
        `SeniorId` int NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `Resolution` longtext CHARACTER SET utf8mb4 NULL,
        `ImagePath` longtext CHARACTER SET utf8mb4 NULL,
        `ThumbnailPath` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Complaints` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Complaints_CareJobs_JobId` FOREIGN KEY (`JobId`) REFERENCES `CareJobs` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Complaints_Caregivers_CaregiverId` FOREIGN KEY (`CaregiverId`) REFERENCES `Caregivers` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Complaints_Seniors_SeniorId` FOREIGN KEY (`SeniorId`) REFERENCES `Seniors` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Notifications` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Message` longtext CHARACTER SET utf8mb4 NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `IsRead` tinyint(1) NOT NULL,
        `Type` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Link` longtext CHARACTER SET utf8mb4 NULL,
        `JobId` int NULL,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `CareJobId` int NULL,
        CONSTRAINT `PK_Notifications` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Notifications_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Notifications_CareJobs_CareJobId` FOREIGN KEY (`CareJobId`) REFERENCES `CareJobs` (`Id`),
        CONSTRAINT `FK_Notifications_CareJobs_JobId` FOREIGN KEY (`JobId`) REFERENCES `CareJobs` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Payments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `JobId` int NOT NULL,
        `SeniorId` int NOT NULL,
        `CaregiverId` int NOT NULL,
        `Amount` decimal(18,2) NOT NULL,
        `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `ApprovedAt` datetime(6) NULL,
        `ApprovedBy` longtext CHARACTER SET utf8mb4 NOT NULL,
        `RejectedAt` datetime(6) NULL,
        `RejectedBy` longtext CHARACTER SET utf8mb4 NOT NULL,
        `RejectionReason` longtext CHARACTER SET utf8mb4 NOT NULL,
        `PaymentMethod` longtext CHARACTER SET utf8mb4 NOT NULL,
        `TransactionId` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Payments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Payments_CareJobs_JobId` FOREIGN KEY (`JobId`) REFERENCES `CareJobs` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Payments_Caregivers_CaregiverId` FOREIGN KEY (`CaregiverId`) REFERENCES `Caregivers` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Payments_Seniors_SeniorId` FOREIGN KEY (`SeniorId`) REFERENCES `Seniors` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Ratings` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `JobId` int NOT NULL,
        `CaregiverId` int NOT NULL,
        `SeniorId` int NOT NULL,
        `Stars` int NOT NULL,
        `Comment` varchar(500) CHARACTER SET utf8mb4 NULL,
        `Review` varchar(500) CHARACTER SET utf8mb4 NULL,
        `CreatedAt` datetime(6) NOT NULL,
        CONSTRAINT `PK_Ratings` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Ratings_CareJobs_JobId` FOREIGN KEY (`JobId`) REFERENCES `CareJobs` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Ratings_Caregivers_CaregiverId` FOREIGN KEY (`CaregiverId`) REFERENCES `Caregivers` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Ratings_Seniors_SeniorId` FOREIGN KEY (`SeniorId`) REFERENCES `Seniors` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE TABLE `Transactions` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `TransactionCode` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Amount` decimal(18,2) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `PaymentMethod` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
        `CareJobId` int NOT NULL,
        `SeniorId` int NULL,
        `CaregiverId` int NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Transactions` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Transactions_CareJobs_CareJobId` FOREIGN KEY (`CareJobId`) REFERENCES `CareJobs` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Transactions_Caregivers_CaregiverId` FOREIGN KEY (`CaregiverId`) REFERENCES `Caregivers` (`Id`),
        CONSTRAINT `FK_Transactions_Seniors_SeniorId` FOREIGN KEY (`SeniorId`) REFERENCES `Seniors` (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE UNIQUE INDEX `IX_Caregivers_UserId` ON `Caregivers` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_CareJobs_CaregiverId` ON `CareJobs` (`CaregiverId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_CareJobs_SeniorId` ON `CareJobs` (`SeniorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_CareJobs_ServiceId` ON `CareJobs` (`ServiceId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Complaints_CaregiverId` ON `Complaints` (`CaregiverId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Complaints_JobId` ON `Complaints` (`JobId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Complaints_SeniorId` ON `Complaints` (`SeniorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Notifications_CareJobId` ON `Notifications` (`CareJobId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Notifications_JobId` ON `Notifications` (`JobId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Notifications_UserId` ON `Notifications` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Payments_CaregiverId` ON `Payments` (`CaregiverId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Payments_JobId` ON `Payments` (`JobId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Payments_SeniorId` ON `Payments` (`SeniorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Ratings_CaregiverId` ON `Ratings` (`CaregiverId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Ratings_JobId` ON `Ratings` (`JobId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Ratings_SeniorId` ON `Ratings` (`SeniorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE UNIQUE INDEX `IX_Seniors_UserId` ON `Seniors` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Transactions_CaregiverId` ON `Transactions` (`CaregiverId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Transactions_CareJobId` ON `Transactions` (`CareJobId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    CREATE INDEX `IX_Transactions_SeniorId` ON `Transactions` (`SeniorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251013145126_InitialMariaDB') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251013145126_InitialMariaDB', '9.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;


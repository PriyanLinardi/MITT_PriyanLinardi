CREATE TABLE [User](
	username NVARCHAR(50) PRIMARY KEY,
	password NVARCHAR(50) NOT NULL,
)

Create Table UserProfiles(
	username NVARCHAR(50) PRIMARY KEY,
	name varchar(50),
	address nvarchar(500),
	bod datetime,
	email nvarchar(50)
)

Create Table Skill(
	SkillId INT IDENTITY PRIMARY KEY,
	SkillName VARCHAR(500)
)

Create Table SkillLevel(
	SkillLevelId INT IDENTITY PRIMARY KEY,
	SkillLevelName VARCHAR(500)
)

Create Table UserSkills(
	UserSkillId NVARCHAR(50),
	UserName NVARCHAR(50),
	SkillId int,
	SkillLevelId int
	FOREIGN KEY (UserName) REFERENCES [User](UserName),
	FOREIGN KEY (SkillId) REFERENCES Skill(SkillId),
	FOREIGN KEY (SkillLevelId) REFERENCES SkillLevel(SkillLevelId)
)

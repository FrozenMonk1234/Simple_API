CREATE TABLE applicant 
(
	Id SERIAL PRIMARY KEY,
	Name varchar(50) NOT NULL UNIQUE
);

CREATE TABLE skills
(
	Id SERIAL PRIMARY KEY,
	Name varchar(100) NOT NULL,
	ApplicantId INTEGER NOT NULL,
	FOREIGN KEY ApplicantId REFERENCES applicant(Id)
);

INSERT INTO applicant (Name)
VALUES ('Mahlabelele Makaba');

select * from applicant;

INSERT INTO skills (name, applicantId)
VALUES ('C#', 1),
		('.Net Web API', 1),
		('PostgreSQL', 1);
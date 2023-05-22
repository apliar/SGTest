CREATE DATABASE sgtestdb
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;
\connect sgtestdb;
CREATE TABLE Departments (
	id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
	parent_id INT,
	manager_id INT,
	name TEXT NOT NULL,
	phone TEXT
);
CREATE TABLE JobTitles (
	id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
	name TEXT NOT NULL
);
CREATE TABLE Employees (
	id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
	department INT,
	fullname TEXT NOT NULL,
	login TEXT,
	password TEXT,
	jobtitle INT,
	CONSTRAINT fk_departments
		FOREIGN KEY(department)
		REFERENCES Departments(id),
	CONSTRAINT fk_jobtitles
		FOREIGN KEY(jobtitle)
		REFERENCES JobTitles(id)
);
ALTER TABLE Departments
ADD CONSTRAINT fk_employees
FOREIGN KEY(manager_id) REFERENCES Employees(id);

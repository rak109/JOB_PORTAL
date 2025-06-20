-- 1. COMPANY
CREATE TABLE Company (
    Company_ID INT PRIMARY KEY,
    Company_Name VARCHAR(100),
    Password VARBINARY(MAX),
    Email_ID VARCHAR(100)
);

-- 2. USERS
CREATE TABLE Users (
    User_ID INT PRIMARY KEY,
    Username VARCHAR(50),
    Password VARBINARY(MAX),
    Email_ID VARCHAR(100),
    User_Type VARCHAR(50),
    User_Profile VARBINARY(MAX),
    CHECK (User_Type IN ('JOB SEEKER', 'EMPLOYER'))
);

-- 3. BRANCH
CREATE TABLE Branch (
    Branch_ID INT PRIMARY KEY,
    Company_ID INT,
    Branch_Name VARCHAR(100),
    Location VARCHAR(100),
    Password VARBINARY(MAX),
    FOREIGN KEY (Company_ID) REFERENCES Company(Company_ID)
);

-- 4. EMPLOYER_DETAILS
CREATE TABLE Employer_Details (
    Employer_ID INT PRIMARY KEY,
    User_ID INT,
    Branch_ID INT,
    Emp_Name VARCHAR(100),
    FOREIGN KEY (User_ID) REFERENCES Users(User_ID),
    FOREIGN KEY (Branch_ID) REFERENCES Branch(Branch_ID)
);

-- 5. JOB
CREATE TABLE Job (
    Job_ID INT PRIMARY KEY,
    Title VARCHAR(100),
    Description TEXT,
    Posting_Date DATE,
    Last_Date DATE,
    Location VARCHAR(100),
    Job_Type VARCHAR(50),
    Skill_ID INT, -- no FK here to avoid circular
    Domain VARCHAR(100),
    Salary DECIMAL(10,2),
    Experience_Level VARCHAR(50),
    Employer_ID INT,
    FOREIGN KEY (Employer_ID) REFERENCES Employer_Details(Employer_ID),
    CHECK (Job_Type IN ('fulltime', 'parttime'))
);

-- 6. JOBSEEKER
CREATE TABLE JobSeeker (
    JobSeeker_ID INT PRIMARY KEY,
    User_ID INT,
    Academic_ID INT,
    Skill_ID INT,
    Professional_ID INT,
    Address TEXT,
    Experience_Level VARCHAR(50),
    DOB DATE,
    Resume VARBINARY(MAX),
    FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);

-- 7. SKILL
CREATE TABLE Skill (
    Skill_ID INT PRIMARY KEY,
    JobSeeker_ID INT,
    Skill_Name VARCHAR(100),
    Expert_Level VARCHAR(50),
    Job_ID INT,
    FOREIGN KEY (JobSeeker_ID) REFERENCES JobSeeker(JobSeeker_ID),
    FOREIGN KEY (Job_ID) REFERENCES Job(Job_ID)
);

-- 8. ACADEMIC_DETAILS
CREATE TABLE Academic_Details (
    Academic_ID INT PRIMARY KEY,
    JobSeeker_ID INT,
    Class VARCHAR(50),
    Start_Year DATE,
    College_University_Name VARCHAR(100),
    End_Year DATE,
    CGPA DECIMAL(4,2),
    FOREIGN KEY (JobSeeker_ID) REFERENCES JobSeeker(JobSeeker_ID)
);

-- 9. PROFESSIONAL_DETAILS
CREATE TABLE Professional_Details (
    Professional_ID INT PRIMARY KEY,
    JobSeeker_ID INT,
    Company_Name VARCHAR(100),
    Designation VARCHAR(100),
    From_Date DATE,
    To_Date DATE,
    FOREIGN KEY (JobSeeker_ID) REFERENCES JobSeeker(JobSeeker_ID)
);

-- 10. APPLICATION
CREATE TABLE Application (
    Application_ID INT PRIMARY KEY,
    Job_ID INT,
    JobSeeker_ID INT,
    Apply_Date DATE,
    Status VARCHAR(50),
    Employer_ID INT,
    FOREIGN KEY (Job_ID) REFERENCES Job(Job_ID),
    FOREIGN KEY (JobSeeker_ID) REFERENCES JobSeeker(JobSeeker_ID),
    FOREIGN KEY (Employer_ID) REFERENCES Employer_Details(Employer_ID),
    CHECK (Status IN ('withdraw', 'accept', 'approve', 'reject'))
);


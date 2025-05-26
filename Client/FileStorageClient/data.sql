-- Tạo bảng User
CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(20) NOT NULL, -- Admin, Manager, Member
    FullName VARCHAR(100) NOT NULL
);

-- Tạo bảng Member
CREATE TABLE Members (
    Id INT PRIMARY KEY,
    Code VARCHAR(20) NOT NULL,
    FullName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Class VARCHAR(20) NOT NULL,
    Department VARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    JoinDate DATE NOT NULL,
    IsActive BIT NOT NULL,
    TrainingPoints INT NOT NULL
);

-- Tạo bảng Activity
CREATE TABLE Activities (
    Id INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description TEXT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Location VARCHAR(100) NOT NULL,
    MaxParticipants INT NOT NULL,
    Points INT NOT NULL,
    IsActive BIT NOT NULL
);

-- Tạo bảng Participation
CREATE TABLE Participations (
    Id INT PRIMARY KEY,
    MemberId INT NOT NULL,
    ActivityId INT NOT NULL,
    RegisterDate DATETIME NOT NULL,
    IsAttended BIT NOT NULL,
    PointsEarned INT NOT NULL,
    FOREIGN KEY (MemberId) REFERENCES Members(Id),
    FOREIGN KEY (ActivityId) REFERENCES Activities(Id)
);

-- Tạo bảng Award
CREATE TABLE Awards (
    Id INT PRIMARY KEY,
    MemberId INT NOT NULL,
    Type VARCHAR(50) NOT NULL,
    Description TEXT NOT NULL,
    AwardDate DATE NOT NULL,
    Points INT NOT NULL,
    FOREIGN KEY (MemberId) REFERENCES Members(Id)
);

-- Dữ liệu mẫu cho bảng Users
INSERT INTO Users (Id, Username, Password, Role, FullName) VALUES
(1, 'admin', 'admin', 'Admin', 'Quản trị viên'),
(2, 'manager', 'manager', 'Manager', 'Bí thư Đoàn'),
(3, 'member', 'member', 'Member', 'Đoàn viên');

-- Dữ liệu mẫu cho bảng Members
INSERT INTO Members (Id, Code, FullName, DateOfBirth, Class, Department, Phone, Email, JoinDate, IsActive, TrainingPoints) VALUES
(1, 'DV001', 'Nguyễn Văn An', '2000-01-15', 'CNTT01', 'Công nghệ thông tin', '0123456789', 'an@email.com', '2023-05-26', 1, 85),
(2, 'DV002', 'Trần Thị Bình', '2001-03-20', 'KT02', 'Kinh tế', '0987654321', 'binh@email.com', '2023-05-26', 1, 92);

-- Dữ liệu mẫu cho bảng Activities
INSERT INTO Activities (Id, Name, Description, StartDate, EndDate, Location, MaxParticipants, Points, IsActive) VALUES
(1, 'Tình nguyện mùa hè', 'Hoạt động tình nguyện hè 2024', '2025-06-05', '2025-06-10', 'Vùng cao', 50, 10, 1),
(2, 'Học tập chính trị', 'Sinh hoạt học tập chính trị tháng 5', '2025-05-01', '2025-05-03', 'Hội trường A', 100, 5, 1);

-- Dữ liệu mẫu cho bảng Awards
INSERT INTO Awards (Id, MemberId, Type, Description, AwardDate, Points) VALUES
(1, 1, 'Giấy khen', 'Khen thưởng vì thành tích xuất sắc trong học tập', '2025-05-20', 10),
(2, 2, 'Bằng khen', 'Khen thưởng vì đóng góp tích cực cho hoạt động', '2025-05-25', 15);

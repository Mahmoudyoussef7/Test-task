CREATE DATABASE ClientsDB;

USE ClientsDB;


CREATE TABLE MaritalStatus (
    ID INT PRIMARY KEY,
    Status VARCHAR(50)
);


INSERT INTO MaritalStatus (ID, Status)
VALUES
    (1, 'Single'),
    (2, 'Married'),
    (3, 'Divorced'),
    (4, 'Widowed');


CREATE TABLE Client (
        ID INT PRIMARY KEY,
        FirstName VARCHAR(100) NOT Null,
        LastName VARCHAR(100) NOT Null,
        DateOfBirth DATE NOT Null,
        MaritalStatusID INT NOT Null,
        MobileNumber BIGINT NOT Null,
        Email VARCHAR(100) NOT Null,
        ImagePath VARCHAR(200) NOT Null,
        FOREIGN KEY (MaritalStatusID) REFERENCES MaritalStatus(ID)
    );


INSERT INTO Client (ID, FirstName, LastName, DateOfBirth, MaritalStatusID, MobileNumber, Email, ImagePath)
VALUES
    (1, 'Ahmed', 'Ahmed', '1990-05-15', 2, 1234567890, 'johndoe@example.com', '/images/asd.jpg'),
    (2, 'Mohamed', 'Ahmed', '1985-12-03', 2, 9876543210, 'janesmith@example.com', '/images/asd.jpg'),
    (3, 'Sara', 'dssdf', '1978-08-20', 1, 4567890123, 'michaeljohnson@example.com', '/images/asd.jpg'),
    (4, 'saad', 'asd', '1995-04-10', 1, 7890123456, 'emilydavis@example.com', '/images/asd.jpg');



	--get all clients SP
create PROCEDURE SP_GetClients
    @PageSize INT,
    @PageNumber INT,
    @Search NVARCHAR(100) = NULL,
    @MaritalStatusID INT = NULL,
    @BirthDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    SELECT ID, FirstName, LastName, DateOfBirth, MaritalStatusID, MobileNumber, Email, ImagePath
    FROM Client
    WHERE (@Search IS NULL OR FirstName LIKE '%' + @Search + '%' OR LastName LIKE '%' + @Search + '%' OR Email LIKE '%' + @Search + '%')
    AND (@MaritalStatusID IS NULL OR MaritalStatusID = @MaritalStatusID)
    AND (@BirthDate IS NULL OR DateOfBirth = @BirthDate)
    ORDER BY ID
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END

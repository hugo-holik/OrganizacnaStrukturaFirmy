-- Generate dummy data for the Company table
INSERT INTO Companies (Name, LeaderId)
VALUES
    ('Company 1', null);

-- Generate dummy data for the Employees table
INSERT INTO Employees (Degree, FirstName, LastName, Phone, Email, CompanyId)
VALUES
    ('BSc', 'John', 'Doe', '1234567890', 'john.doe@example.com', 1),
    ('BSc', 'Jane', 'Smith', '9876543210', 'jane.smith@example.com', 1),
    ('MBA', 'David', 'Johnson', '5555555555', 'david.johnson@example.com', 1),
    ('PhD', 'Michael', 'Williams', '1111111111', 'michael.williams@example.com', 1),
    ('MSc', 'Emily', 'Brown', '2222222222', 'emily.brown@example.com', 1),
    ('BSc', 'Daniel', 'Jones', '3333333333', 'daniel.jones@example.com', 1),
    ('BSc', 'Olivia', 'Miller', '4444444444', 'olivia.miller@example.com', 1),
    ('MSc', 'William', 'Davis', '5555555555', 'william.davis@example.com', 1),
    ('BSc', 'Sophia', 'Garcia', '6666666666', 'sophia.garcia@example.com', 1),
    ('PhD', 'James', 'Martinez', '7777777777', 'james.martinez@example.com', 1),
    ('MSc', 'Emma', 'Anderson', '8888888888', 'emma.anderson@example.com', 1),
    ('BSc', 'Jacob', 'Thomas', '9999999999', 'jacob.thomas@example.com', 1),
    (null, 'Sophia', 'Clark', '1111111111', 'sophia.clark@example.com', 1),
    ('MSc', 'Logan', 'Rodriguez', '2222222222', 'logan.rodriguez@example.com', 1),
    ('BSc', 'Ava', 'Lewis', '3333333333', 'ava.lewis@example.com', 1),
    ('MSc', 'Jackson', 'Lee', '4444444444', 'jackson.lee@example.com', 1),
    ('BSc', 'Olivia', 'Wright', '5555555555', 'olivia.wright@example.com', 1),
    ('BSc', 'Lucas', 'Hall', '6666666666', 'lucas.hall@example.com', 1),
    ('MSc', 'Amelia', 'Young', '7777777777', 'amelia.young@example.com', 1),
    (null, 'Benjamin', 'Lopez', '8888888888', 'benjamin.lopez@example.com', 1),
    ('MSc', 'Mia', 'King', '9999999999', 'mia.king@example.com', 1),
    (null, 'Henry', 'Scott', '1111111111', 'henry.scott@example.com', 1),
    ('BSc', 'Harper', 'Green', '2222222222', 'harper.green@example.com', 1),
    ('MSc', 'Elijah', 'Adams', '3333333333', 'elijah.adams@example.com', 1),
    ('BSc', 'Evelyn', 'Baker', '4444444444', 'evelyn.baker@example.com', 1),
    ('MSc', 'William', 'Carter', '5555555555', 'william.carter@example.com', 1),
    ('BSc', 'Elizabeth', 'Gonzalez', '6666666666', 'elizabeth.gonzalez@example.com', 1),
    ('BSc', 'James', 'Mitchell', '7777777777', 'james.mitchell@example.com', 1),
    ('MSc', 'Sofia', 'Perez', '8888888888', 'sofia.perez@example.com', 1),
    ('BSc', 'Emily', 'Hill', '9999999999', 'emily.hill@example.com', 1);

-- Set Leaders in Companies table
UPDATE Companies
SET LeaderId = 1
WHERE Id = 1;

-- Generate dummy data for the Divisions table
INSERT INTO Divisions (Name, LeaderId, CompanyId)
VALUES
    ('Division 1', 2, 1),
    ('Division 2', 3, 1);

-- Generate dummy data for the Projects table
INSERT INTO Projects (Name, LeaderId, DivisionId)
VALUES
    ('Project 1', 4, 1),
    ('Project 2', 5, 1),
    ('Project 3', 6, 2),
    ('Project 4', 7, 2),
    ('Project 5', 8, 2);

-- Generate dummy data for the Departments table
INSERT INTO Departments(Name, LeaderId, ProjectId)
VALUES
    ('Department 1', 9, 1),
    ('Department 2', 10, 1),
    ('Department 3', 11, 1),
    ('Department 4', 12, 2),
    ('Department 5', 13, 2),
    ('Department 6', 14, 2),
    ('Department 7', 15, 3),
    ('Department 8', 16, 3),
    ('Department 9', 17, 3),
    ('Department 10', 18, 3),
    ('Department 11', 19, 4),
    ('Department 12', 20, 4),
    ('Department 13', 21, 4),
    ('Department 14', 22, 5),
    ('Department 15', 23, 5);
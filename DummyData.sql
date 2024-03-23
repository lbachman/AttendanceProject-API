-- Insert data into users table
INSERT INTO users (userName) VALUES
('user1'),
('user2'),
('user3');

-- Insert data into students table
INSERT INTO students (student_uuid, student_userName) VALUES
('uuid1', 'student1'),
('uuid2', 'student2'),
('uuid3', 'student3');

-- Insert data into class table
INSERT INTO class (semester_code, room, start_time, end_time, days, instructor_id) VALUES
('SEM001', 'Room A', '08:00:00', '10:00:00', 'Monday', 1),
('SEM002', 'Room B', '10:00:00', '12:00:00', 'Tuesday', 2),
('SEM003', 'Room C', '13:00:00', '15:00:00', 'Wednesday', 3);

-- Insert data into days table
INSERT INTO days (class_id, day) VALUES
(1, 'Monday'),
(2, 'Tuesday'),
(3, 'Wednesday');

-- Insert data into student_class table
INSERT INTO student_class (student_uuid, class_id) VALUES
('uuid1', 1),
('uuid2', 2),
('uuid3', 3);

-- Insert data into attends table
INSERT INTO attends (student_uuid, class_id, attendance_date) VALUES
('uuid1', 1, '2024-03-25'),
('uuid2', 2, '2024-03-26'),
('uuid3', 3, '2024-03-27');

-- Insert data into message table
INSERT INTO message (message) VALUES
('Message 1'),
('Message 2'),
('Message 3');

-- Insert data into communication table
INSERT INTO communication (instructor_id, student_uuid, class_id, message_id) VALUES
(1, 'uuid1', 1, 1),
(2, 'uuid2', 2, 2),
(3, 'uuid3', 3, 3);

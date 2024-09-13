DROP DATABASE IF EXISTS BookStore;
CREATE DATABASE IF NOT EXISTS BookStore;

USE BookStore;

CREATE USER IF NOT EXISTS 'ga-app'@'localhost' IDENTIFIED BY 'ga-5ecret-%';
CREATE USER IF NOT EXISTS 'ga-app'@'%' IDENTIFIED BY 'ga-5ecret-%';

GRANT ALL privileges ON ga_emne7_avansert.* TO 'ga-app'@'%';
GRANT ALL privileges ON ga_emne7_avansert.* TO 'ga-app'@'localhost';

FLUSH PRIVILEGES;

DROP TABLE IF EXISTS Book;

CREATE TABLE Book (
ID INT AUTO_INCREMENT PRIMARY KEY,
Title VARCHAR(250),
Author VARCHAR(250),
PublicationYear INT,
ISBN VARCHAR(20),
InStock INT
);
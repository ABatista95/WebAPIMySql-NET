CREATE DATABASE `bd_patients` /*!40100 DEFAULT CHARACTER SET utf8mb4 */

-- bd_patients.paciente definition

CREATE TABLE bd_patients.paciente (
	id BIGINT NOT NULL,
	name varchar(100) NOT NULL,
	surnames varchar(100) NOT NULL,
	direction varchar(255) NOT NULL,
	email varchar(100) NOT NULL,
	phone varchar(100) NOT NULL,
	city varchar(100) NOT NULL,
	location varchar(100) NOT NULL,
	birthday_date DATETIME NULL,
	nationality varchar(100) NOT NULL
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

CREATE DEFINER=`root`@`localhost` PROCEDURE `bd_patients`.`sp_getAllPatient`()
BEGIN
	SELECT * FROM bd_patients.paciente;
END
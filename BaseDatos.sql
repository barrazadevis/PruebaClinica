CREATE DATABASE Clinica
GO
USE Clinica
GO
CREATE TABLE Medicamento
(
	ID INT PRIMARY KEY IDENTITY,
	NombreMedicamento VARCHAR(100),
	FechaRecibido DATETIME,
	Valor DECIMAL(15,2),
	Cantidad INT
)

-- Crear la base de datos Empresa
CREATE DATABASE Empresa;

-- Usar la base de datos Empresa
USE Empresa;
-- Crear el catálogo T_CAT_PUESTO
CREATE TABLE T_CAT_PUESTO (
    Id_Puesto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Puesto VARCHAR(50)
);
-- REGISTROS 
-- Insertar algunos registros de ejemplo en la tabla de catálogo "T_CAT_PUESTO"
INSERT INTO T_CAT_PUESTO (Nombre_Puesto) VALUES ('Gerente');
INSERT INTO T_CAT_PUESTO (Nombre_Puesto) VALUES ('Supervisor');
INSERT INTO T_CAT_PUESTO (Nombre_Puesto) VALUES ('Analista');
INSERT INTO T_CAT_PUESTO (Nombre_Puesto) VALUES ('Técnico');



-- Crear la tabla T_EMPLEADOS
CREATE TABLE T_EMPLEADOS (
    Id_NumEmp INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellidos VARCHAR(50),
	Activo BIT DEFAULT 1,
    Id_Puesto INT,
    FOREIGN KEY (Id_Puesto) REFERENCES T_CAT_PUESTO(Id_Puesto)
);

-- ********************************PROCEDIMIENTOS************************************

--============================INSERTAR====================================
CREATE PROCEDURE SP_INSERTAR_EMPLEADO
@Nombre VARCHAR(50),
@Apellidos VARCHAR(50),
@Id_Puesto INT
AS
BEGIN
    INSERT INTO T_EMPLEADOS(Nombre, Apellidos, Id_Puesto) VALUES(@Nombre, @Apellidos, @Id_Puesto)
END
--============================ACTUALIZAR====================================
CREATE PROCEDURE SP_ACTUALIZAR_EMPLEADO
@Id_NumEmp INT,
@Nombre VARCHAR(50),
@Apellidos VARCHAR(50),
@Id_Puesto INT
AS
BEGIN
    UPDATE T_EMPLEADOS SET Nombre = @Nombre, Apellidos = @Apellidos, Id_Puesto = @Id_Puesto WHERE Id_NumEmp = @Id_NumEmp
END

--============================ELIMINAR====================================
CREATE or ALTER PROCEDURE SP_ELIMINAR_EMPLEADO
    @Id_NumEmp INT
AS
BEGIN
    UPDATE T_EMPLEADOS
    SET Activo = 0
    WHERE Id_NumEmp = @Id_NumEmp
END
--============================CONSULTAR TODOS====================================
CREATE or ALTER PROCEDURE SP_CONSULTAR_TODOS
AS
BEGIN
    SELECT E.Id_NumEmp, E.Nombre, E.Apellidos,E.Activo, P.Nombre_Puesto AS Puesto
    FROM T_EMPLEADOS E
    JOIN T_CAT_PUESTO P ON E.Id_Puesto = P.Id_Puesto
END

--============================CONSULTAR UNO====================================
CREATE or ALTER PROCEDURE SP_CONSULTAR_UNO
    @Id_NumEmp INT
AS
BEGIN
    SELECT E.Id_NumEmp, E.Nombre, E.Apellidos,E.Activo,P.Nombre_Puesto AS Puesto
    FROM T_EMPLEADOS E
    JOIN T_CAT_PUESTO P ON E.Id_Puesto = P.Id_Puesto
    WHERE E.Id_NumEmp = @Id_NumEmp
END


--------------------------------------------------------------------------------
EXEC SP_INSERTAR_EMPLEADO
    @Nombre = 'Juan',
    @Apellidos = 'Pérez',
    @Id_Puesto = 1


EXEC SP_CONSULTAR_UNO 1
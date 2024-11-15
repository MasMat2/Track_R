-- Identificar y eliminar la restricción de clave externa
DECLARE @constraintName NVARCHAR(200)
SELECT @constraintName = name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('Configuracion.Colonia') AND name LIKE 'FK__Colonia__IdMunic%';

IF @constraintName IS NOT NULL
BEGIN
    EXEC('ALTER TABLE Configuracion.Colonia DROP CONSTRAINT ' + @constraintName);
END

-- Eliminar la columna IdMunicipio
ALTER TABLE Configuracion.Colonia
DROP COLUMN IdMunicipio;
--Eliminar la columna Codigo Postal
ALTER TABLE Configuracion.Colonia
DROP COLUMN CodigoPostal
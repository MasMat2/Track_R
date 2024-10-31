-- Añadir el campo IdEntidadEstructura a la tabla Notificacion
ALTER TABLE Configuracion.Notificacion
ADD IdPadecimiento INT NULL;

-- Añadir la clave foránea a la tabla Notificacion
ALTER TABLE Configuracion.Notificacion
ADD CONSTRAINT FK_Notificacion_EntidadEstructura
FOREIGN KEY (IdPadecimiento) REFERENCES Configuracion.EntidadEstructura(IdEntidadEstructura);
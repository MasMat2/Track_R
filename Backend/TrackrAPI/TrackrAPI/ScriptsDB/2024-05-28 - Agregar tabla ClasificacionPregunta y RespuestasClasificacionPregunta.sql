
CREATE TABLE RespuestasClasificacionPregunta
(
    IdRespuestasClasificacionPregunta int NOT NULL IDENTITY(1,1),
    IdClasificacionPregunta int,
    Nombre varchar(100),
    Estatus bit,
    Identificador varchar(10),
    Valor int,
    PRIMARY KEY (IdRespuestasClasificacionPregunta),
    FOREIGN KEY (IdClasificacionPregunta) REFERENCES Proyectos.ClasificacionPregunta(IdClasificacionPregunta)
)

CREATE TABLE ClasificacionPregunta
(
    IdClasificacionPregunta int NOT NULL IDENTITY(1,1),
    Nombre varchar(50),
    Estatus bit,
    Clave varchar(20),
    PRIMARY KEY (IdClasificacionPregunta)
)  
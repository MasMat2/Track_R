use Trackr_Dev

    ALTER TABLE Proyectos.Reactivo
    ADD EscalaLikert bit,
        PreguntaAbierta bit,
        RespuestaSimple bit,
        RespuestaMultiple bit

    CREATE TABLE Proyectos.Respuesta
    (
        IdRespuesta int NOT NULL IDENTITY(1,1),
        IdReactivo int NOT NULL,
        Clave nvarchar(30),
        Respuesta nvarchar(2000),
        RespuestaCorrecta bit,
        Valor int,
        PRIMARY KEY (IdRespuesta),
        FOREIGN KEY (IdReactivo) REFERENCES Proyectos.Reactivo(IdReactivo)
    )


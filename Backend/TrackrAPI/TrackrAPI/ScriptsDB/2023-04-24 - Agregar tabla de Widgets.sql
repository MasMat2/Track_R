-- Tabla Widget
CREATE TABLE Widget (
	[IdWidget] INT IDENTITY(1, 1) NOT NULL,
	[Clave] NVARCHAR(20) NOT NULL,
	[Nombre] NVARCHAR(50) NOT NULL,

	PRIMARY KEY (IdWidget)
);

-- Tabla Usuario Widget
CREATE TABLE UsuarioWidget (
	[IdUsuarioWidget] INT IDENTITY(1, 1) NOT NULL,
	[IdWidget] INT NOT NULL,
	[IdUsuario] INT NOT NULL,

	PRIMARY KEY (IdUsuarioWidget),
	FOREIGN KEY (IdWidget) REFERENCES Widget(IdWidget),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
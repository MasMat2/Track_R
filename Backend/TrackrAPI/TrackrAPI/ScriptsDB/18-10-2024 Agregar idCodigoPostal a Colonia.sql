 ALTER TABLE Configuracion.Colonia  add 
IdCodigoPostal INT FOREIGN KEY REFERENCES Configuracion.CodigoPostal(IdCodigoPostal)
 
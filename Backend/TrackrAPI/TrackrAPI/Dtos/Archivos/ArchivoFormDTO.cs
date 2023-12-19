﻿using System.Globalization;
using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace TrackrAPI.Dtos.Archivos;

public class ArchivoFormDTO
{
    public string Nombre { get; set; }
    public string FechaRealizacion { get; set; }
    public byte[] Archivo { get; set; }
    public string ArchivoTipoMime { get; set; }
    public string ArchivoNombre { get; set; }
}


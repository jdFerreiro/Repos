using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyZadERP.ViewModels.DTO
{
    public class UserDTO
    {
        [PrimaryKey]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string TecnicoEmail { get; set; }
        public int idRol { get; set; }
        public int TecnicoIdTecnico { get; set; }
        public string Imagen { get; set; }
        public string token { get; set; }
    }
}

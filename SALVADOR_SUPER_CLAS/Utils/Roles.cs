namespace SALVADOR_SUPER_CLAS.Utils
{
    public static class Roles
    {
        public const string Administrador = "Administrador";
        public const string Boletero = "Boletero";
        public const string GerenteOperaciones = "GerenteOperaciones";

        public const string GerenciaYAdministracion = "Administrador,GerenteOperaciones";

        public static string NombreVisible(string? rol)
        {
            return rol switch
            {
                Administrador => "Administrador",
                Boletero => "Boletero / Cajero",
                GerenteOperaciones => "Gerencia (Reportes)",
                _ => rol ?? ""
            };
        }
    }
}

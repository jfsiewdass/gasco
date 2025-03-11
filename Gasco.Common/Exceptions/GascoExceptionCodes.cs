namespace Gasco.Common.Exceptions
{
    public class GascoExceptionCodes
    {
        public static ExceptionInfo GascoEx9999 => new(9999, "Su sesión fue cerrada debido a razones internas.");
        public static ExceptionInfo GascoEx1001 => new(1001, "Los datos recibidos no son válidos.");
        public static ExceptionInfo GascoEx1002 => new(1002, "El correo electrónico no tiene un formato válido.");
        public static ExceptionInfo GascoEx1003 => new(1003, "La clave no tiene un formato válido."); 
        public static ExceptionInfo GascoEx1004 => new(1004, "El usuario no existe.");
        public static ExceptionInfo GascoEx1005 => new(1005, "Usuario o clave inválida.");
        public static ExceptionInfo GascoEx1006 => new(1006, "Usuario no válido para ingresar a esta aplicación.");
        public static ExceptionInfo GascoEx1007 => new(1007, "Información de sesión de usuario inválida.");
        public static ExceptionInfo GascoEx1008 => new(1008, "No se logró leer la información de sesión de usuario.");
        public static ExceptionInfo GascoEx1009 => new(1009, "Error, el usuario se encuentra inactivo.");
        public static ExceptionInfo GascoEx1010 => new(1010, "No está autorizado para realizar esta petición.");
        public static ExceptionInfo GascoEx1011 => new(1011, "Su sesión ha expirado.");

    }
}

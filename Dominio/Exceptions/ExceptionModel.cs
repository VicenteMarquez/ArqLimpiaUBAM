
namespace Dominio.Excepciones
{
    public class ExceptionModel:Exception
    {
        public static string EnvioArgument(Exception ex) {
            switch(ex){
                case InsufficientExecutionStackException _: {
                        return "Error de Excepcion 5";
                    } break;
                case ArgumentNullException _: {
                        return "Error de Excepcion 5";
                    }
                    break;
                case UnauthorizedAccessException _: {
                        return "Error de Excepcion 4";
                    }
                    break;
                case InvalidOperationException _: {
                        return "Error de Conexión con el Servidor.";
                    }
                    break;
                case ArgumentException _: { return "Error perifique los campos sean validos."; }break;
                case Exception _: { 
                        return"Error verifique, que los campos se registre correctamente."; 
                    }break;
                default: { 
                        return null; 
                    }break;
            }
        
        }

    }
}

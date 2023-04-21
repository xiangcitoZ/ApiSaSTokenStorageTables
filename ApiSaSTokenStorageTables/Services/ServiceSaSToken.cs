using Azure.Data.Tables.Sas;
using Azure.Data.Tables;

namespace ApiSaSTokenStorageTables.Services
{
    public class ServiceSaSToken
    {
        //ESTA CLASE GENERARA TOKENS A PARTIR DE LA 
        //TABLA ALUMNOS
        private TableClient tablaAlumnos;

        public ServiceSaSToken(IConfiguration configuration)
        {
            string azureKeys =
                configuration.GetValue<string>("AzureKeys:StorageAccount");
            TableServiceClient serviceClient =
                new TableServiceClient(azureKeys);
            this.tablaAlumnos = serviceClient.GetTableClient("alumnos");
        }

        //QUEREMOS UN TOKEN QUE SOLAMENTE NOS DEVUELVA LOS ALUMNOS
        //DE UN DETERMINADO CURSO.
        public string GenerateSaSToken(string curso)
        {
            //NECESITAMOS LOS PERMISOS DE ACCESO
            TableSasPermissions permisos =
                 TableSasPermissions.Read;
            //DEBEMOS CREAR UN CONSTRUCTOR CON LOS PERMISOS 
            //Y EL TIEMPO DE ACCESO A LA TABLA
            TableSasBuilder builder =
                this.tablaAlumnos.GetSasBuilder(permisos,
                DateTime.Now.AddMinutes(50));
            //QUEREMOS QUE SOLAMENTE NOS MUESTRE LOS CURSOS
            //(PARTITION KEY)
            //JAVA
            builder.PartitionKeyStart = curso;
            builder.PartitionKeyEnd = curso;
            //CON TODO ESTO MONTADO, NOS DARA UNA URI
            //DE ACCESO
            Uri uriToken =
                this.tablaAlumnos.GenerateSasUri(builder);
            //EXTRAEMOS LA RUTA HTTPS CON EL TOKEN
            string token = uriToken.AbsoluteUri;
            return token;
        }

    }
}

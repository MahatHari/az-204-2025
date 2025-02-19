using System.Net;
using Microsoft.Azure.Cosmos;
namespace cosmosDB
{
    public class Program
    {
        //  endpoint
        private static readonly string EndpointUrl = "";

        // primaryKey
        private static readonly string PrimaryKey = "";

        // comos cleint
        private CosmosClient cosmosClient;

        // database 
        private Database database;

        // container 
        private Container container;

        // names of database and container we will create
        private string databseId = "az204Database2025";
        private string containerId = "az204Container2025";

        public static async Task Main(string[] args)
        {

            try
            {
                Console.WriteLine("Begining Operations .. \n");
                Program p = new Program();
                await p.CosmosAsync();

            }
            catch (CosmosException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occured {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e);
            }
            finally
            {
                Console.WriteLine("End of program, press any key to exit.");
                Console.ReadKey();
            }


        }
        public async Task CosmosAsync()
        {

            // Crea a new instance of Comsos client 
            this.cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);

            // Run CreateDatabaseAsync method
            await this.CreateDatabaseAsync();

            // Run the CreateContainerAsync method

            await this.CreateContainerAsync();
        }
        public async Task CreateDatabaseAsync()
        {
            // create a new database using the cosmosClient
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databseId);
            Console.WriteLine("Created Database {0} \n", this.database.Id);
        }
        public async Task CreateContainerAsync()
        {
            // create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
            Console.WriteLine("Created container : {0}\n", this.container.Id);
        }
    }
}
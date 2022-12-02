using So_z_O_yin.Broker;
using So_z_O_yin.Service;
using System.Data.SqlClient;
using System.IO;

internal class Program
{
    static async Task Main()
    {
        await GameService.Game();
    }
}

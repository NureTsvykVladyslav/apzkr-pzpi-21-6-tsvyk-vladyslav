using System.Data;
using ApexiBee.Application.Interfaces;
using ApexiBee.Infrastructure.Implementation.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace ApexiBee.Infrastructure.Implementation.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string serverName = ".\\SQLEXPRESS";
        private readonly string databaseName = "ApexiBee";
        private readonly string backupName = "db.bak";
        private readonly ConfigurationHelper _configurationHelper;

        public DatabaseService(ConfigurationHelper configurationHelper)
        {
            this._configurationHelper = configurationHelper;
        }

        public bool CreateDbBackup()
        {
            ServerConnection serverConnection = new ServerConnection(this.serverName);
            Server server = new Server(serverConnection);

            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.Database = this.databaseName;

            BackupDeviceItem backupDeviceItem = new BackupDeviceItem(this.backupName, DeviceType.File);
            backup.Devices.Add(backupDeviceItem);

            backup.SqlBackup(server);
            return true;
        }

        public bool RestoreDbFromLastBackup()
        {
            ServerConnection serverConnection = new ServerConnection(this.serverName);
            Server server = new Server(serverConnection);

            server.ConnectionContext.ExecuteNonQuery($"ALTER DATABASE [{this.databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

            Restore restore = new Restore();

            restore.Database = this.databaseName;
            restore.Action = RestoreActionType.Database;
            restore.Devices.AddDevice(this.backupName, DeviceType.File);

            DataTable backupSets = restore.ReadBackupHeader(server);

            int fileNumber = backupSets.Rows.Count;

            restore.FileNumber = fileNumber;

            restore.SqlRestore(server);

            server.ConnectionContext.ExecuteNonQuery($"ALTER DATABASE [{this.databaseName}] SET MULTI_USER");

            return true;
        }

        public DateTime? GetLastBackupDate()
        {
            string query = $"USE msdb;\r\nSELECT TOP 1 MAX(bs.backup_finish_date) FROM backupset bs WHERE bs.database_name = '{databaseName}'";
            string? connectionString = this._configurationHelper.GetConnectionString("default");
            if (connectionString == null)
            {
                return null;
            }

            DateTime? lastBackupDate;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    lastBackupDate = command.ExecuteScalar() as DateTime?;
                }
            }

            if (lastBackupDate.HasValue)
            {
                return lastBackupDate;
            }
            else
            {
                return null;
            }
        }
    }
}

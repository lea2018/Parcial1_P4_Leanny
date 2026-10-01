using Dapper;
using Microsoft.Data.Sqlite;
using Parcial1_P4_Leanny.Models;

namespace Parcial1_P4_Leanny.Services
{
    public class NumbersService
    {
        private readonly string _connectionString;

        public NumbersService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private SqliteConnection CrearConexion()
        {
            return new SqliteConnection(_connectionString);
        }

        public async Task InitializeAsync()
        {
            using var connection = CrearConexion();

            await connection.OpenAsync();

            string sql = """
                CREATE TABLE IF NOT EXISTS NumberRecords
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Fecha TEXT NOT NULL,
                    Numero REAL NOT NULL,
                    Resultado REAL NOT NULL
                );
                """;

            await connection.ExecuteAsync(sql);
        }

        public async Task<int> SaveAsync(NumberRecord record)
        {
            using var connection = CrearConexion();

            string sql = """
                INSERT INTO NumberRecords (Fecha, Numero, Resultado)
                VALUES (@Fecha, @Numero, @Resultado);

                SELECT last_insert_rowid();
                """;

            return await connection.ExecuteScalarAsync<int>(sql, record);
        }

        public async Task<bool> UpdateAsync(NumberRecord record)
        {
            using var connection = CrearConexion();

            string sql = """
                UPDATE NumberRecords
                SET Fecha = @Fecha,
                    Numero = @Numero,
                    Resultado = @Resultado
                WHERE Id = @Id;
                """;

            int filas = await connection.ExecuteAsync(sql, record);

            return filas > 0;
        }

        public async Task<NumberRecord?> GetByIdAsync(int id)
        {
            using var connection = CrearConexion();

            string sql = """
                SELECT Id, Fecha, Numero, Resultado
                FROM NumberRecords
                WHERE Id = @Id;
                """;

            return await connection.QueryFirstOrDefaultAsync<NumberRecord>(
                sql,
                new { Id = id });
        }

        public async Task<IEnumerable<NumberRecord>> GetListAsync()
        {
            using var connection = CrearConexion();

            string sql = """
                SELECT Id, Fecha, Numero, Resultado
                FROM NumberRecords
                ORDER BY Id ASC;
                """;

            return await connection.QueryAsync<NumberRecord>(sql);
        }
    }
}

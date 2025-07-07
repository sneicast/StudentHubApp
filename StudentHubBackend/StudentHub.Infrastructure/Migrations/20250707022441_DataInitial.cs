using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Insert CreditPrograms
            migrationBuilder.InsertData(
                table: "CreditPrograms",
                columns: new[] { "Id", "Name", "TotalCredits" },
                values: new object[,]
                {
                    { 1, "Ingeniería Electrónica", 9 },
                    { 2, "Ingeniería en Telecomunicaciones", 9 },
                    { 3, "Ingeniería en Software", 9 },
                    { 4, "Ciencias de la Computación", 9 }
                }
            );

            // Insert Professors
            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "Name", "Email" },
                values: new object[,]
                {
                    { 1, "Maria Fernanda Gomez", "maria.gomez@example.co" },
                    { 2, "Carlos Andres Rodriguez", "carlos.rodriguez@example.com" },
                    { 3, "Ana Lucia Martinez", "ana.martinez@example.com" },
                    { 4, "Jorge Enrique Ramirez", "jorge.ramirez@example.com" },
                    { 5, "Juan Camilo Suarez", "camilo.suarez@example.com" }
                }
            );

            // Insert Subjects
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name", "Credits" },
                values: new object[,]
                {
                    { 1, "Fisica General", 3 },
                    { 2, "Quimica Basic", 3 },
                    { 3, "Introduccion a la Programacion", 3 },
                    { 4, "Comunicacion Oral y Escrita", 3 },
                    { 5, "Calculo Diferencia", 3 },
                    { 6, "Estadistica", 3 },
                    { 7, "Algoritmos y Estructuras de Dato", 3 },
                    { 8, "Etica Profesional", 3 },
                    { 9, "Electronica Basic", 3 },
                    { 10, "Matematicas", 3 }
                }
            );

            // Insert Classes
            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "SubjectId", "ProfessorId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 4 },
                    { 8, 4 },
                    { 9, 5 },
                    { 10, 5 }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

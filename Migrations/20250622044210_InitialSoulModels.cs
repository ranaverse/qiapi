using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qiapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialSoulModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoulRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoulSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrequencyLevel = table.Column<int>(type: "int", nullable: true),
                    ActivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Signals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resonance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Heartbeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Emotion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intensity = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SignalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heartbeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heartbeats_Signals_SignalId",
                        column: x => x.SignalId,
                        principalTable: "Signals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Heartbeats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: true),
                    Resonance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unlocked = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggeredBySignalId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insights_Signals_TriggeredBySignalId",
                        column: x => x.TriggeredBySignalId,
                        principalTable: "Signals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Insights_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Whispers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeltResonance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClarityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoticedBecause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointingTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinkedSignalId = table.Column<int>(type: "int", nullable: true),
                    LinkedHeartbeatId = table.Column<int>(type: "int", nullable: true),
                    ResultedInsightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whispers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Whispers_Heartbeats_LinkedHeartbeatId",
                        column: x => x.LinkedHeartbeatId,
                        principalTable: "Heartbeats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Whispers_Insights_ResultedInsightId",
                        column: x => x.ResultedInsightId,
                        principalTable: "Insights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Whispers_Signals_LinkedSignalId",
                        column: x => x.LinkedSignalId,
                        principalTable: "Signals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Whispers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mirrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReflectedFromUserId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedInsightId = table.Column<int>(type: "int", nullable: true),
                    LinkedHeartbeatId = table.Column<int>(type: "int", nullable: true),
                    LinkedWhisperId = table.Column<int>(type: "int", nullable: true),
                    TruthResonanceScore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MirrorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transparency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mirrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mirrors_Heartbeats_LinkedHeartbeatId",
                        column: x => x.LinkedHeartbeatId,
                        principalTable: "Heartbeats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mirrors_Insights_LinkedInsightId",
                        column: x => x.LinkedInsightId,
                        principalTable: "Insights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mirrors_Users_ReflectedFromUserId",
                        column: x => x.ReflectedFromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mirrors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mirrors_Whispers_LinkedWhisperId",
                        column: x => x.LinkedWhisperId,
                        principalTable: "Whispers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heartbeats_SignalId",
                table: "Heartbeats",
                column: "SignalId");

            migrationBuilder.CreateIndex(
                name: "IX_Heartbeats_UserId",
                table: "Heartbeats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Insights_TriggeredBySignalId",
                table: "Insights",
                column: "TriggeredBySignalId");

            migrationBuilder.CreateIndex(
                name: "IX_Insights_UserId",
                table: "Insights",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_LinkedHeartbeatId",
                table: "Mirrors",
                column: "LinkedHeartbeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_LinkedInsightId",
                table: "Mirrors",
                column: "LinkedInsightId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_LinkedWhisperId",
                table: "Mirrors",
                column: "LinkedWhisperId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_ReflectedFromUserId",
                table: "Mirrors",
                column: "ReflectedFromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_UserId",
                table: "Mirrors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Signals_UserId",
                table: "Signals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_LinkedHeartbeatId",
                table: "Whispers",
                column: "LinkedHeartbeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_LinkedSignalId",
                table: "Whispers",
                column: "LinkedSignalId");

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_ResultedInsightId",
                table: "Whispers",
                column: "ResultedInsightId");

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_UserId",
                table: "Whispers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mirrors");

            migrationBuilder.DropTable(
                name: "Whispers");

            migrationBuilder.DropTable(
                name: "Heartbeats");

            migrationBuilder.DropTable(
                name: "Insights");

            migrationBuilder.DropTable(
                name: "Signals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

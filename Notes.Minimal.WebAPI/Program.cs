var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<INotesRepo, NotesRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.OperationFilter<AddRequiredHeaderParameter>();
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notes.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/Notes",[ApiKey] () =>
{
	using var scope = app.Services.CreateScope();

	INotesRepo repo = scope.ServiceProvider.GetRequiredService<INotesRepo>();

	return repo?.GetAll();
});

app.MapGet("/api/Notes/{id}", [ApiKey] (int id) =>
 {
	 using var scope = app.Services.CreateScope();

	 INotesRepo repo = scope.ServiceProvider.GetRequiredService<INotesRepo>();

	 return repo?.GetById(id);
 });

app.MapGet("/api/Search/{term}", [ApiKey] (string term) =>
{
	using var scope = app.Services.CreateScope();

	INotesRepo repo = scope.ServiceProvider.GetRequiredService<INotesRepo>();

	return repo?.SearchNotes(term);
});

app.MapPost("/api/Notes", [ApiKey] ([FromBody] Note note) =>
{
	if (string.IsNullOrEmpty(note?.Title) || string.IsNullOrEmpty(note?.NoteContent))
	{
		return Results.BadRequest(new { message = "Invalid data provided" });
	}

	using var scope = app.Services.CreateScope();

	INotesRepo repo = scope.ServiceProvider.GetRequiredService<INotesRepo>();

	var addedNote = repo?.Add(note);

	return Results.Ok(new { message = "Note added successfully", Note = addedNote });
});

app.MapPut("/api/Notes/{id}", [ApiKey] (int id, [FromBody] Note note) =>
{
	using var scope = app.Services.CreateScope();

	INotesRepo repo = scope.ServiceProvider.GetRequiredService<INotesRepo>();

	var existingnote = repo?.GetById(id);

	if (existingnote == null) return Results.NotFound(new { message = $"Note with {id} not found" });

	var result = repo?.Update(note);

	return Results.Ok(new { message = "Updated successfully", Note = result });
});


app.MapDelete("/api/Notes/{id}", [ApiKey] (int id) =>
{
	using var scope = app.Services.CreateScope();

	INotesRepo repo = scope.ServiceProvider.GetRequiredService<INotesRepo>();

	var note = repo?.GetById(id);

	if (note == null) return Results.NotFound(new { message = $"Note with {id} not found" });

	var result = repo?.Delete(note);

	return Results.Ok(new { message = "Deleted successfully" });
});

app.Run();
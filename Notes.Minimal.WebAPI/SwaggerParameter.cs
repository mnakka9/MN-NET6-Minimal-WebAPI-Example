namespace Notes.WebAPI.Attributes;

public class AddRequiredHeaderParameter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (operation.Parameters == null)
			operation.Parameters = new List<OpenApiParameter>();

		operation.Parameters.Add(new OpenApiParameter
		{
			Name = "ApiKey",
			In = ParameterLocation.Header,
			Required = true
		});
	}
}

using Microsoft.EntityFrameworkCore;
using Scheduling.Data;
using Scheduling.Models;

namespace Scheduling.Routes
{
    public static class SchedulingRoute
    {
        public static void SchedulingRoutes(this WebApplication app)
        {
            var route = app.MapGroup("Scheduling");

            route.MapPost("",
                async (SchedulingRequest req, SchedulingContext context) =>
                {
                    var scheduling = new SchedulingModel(
                        req.Name,    
                        req.Inicio,  
                        req.Fim,     
                        req.Periodos 
                    );

                    await context.AddAsync(scheduling);
                    await context.SaveChangesAsync();

                    return Results.Created($"/Scheduling/{scheduling.Id}", scheduling);
                }
            );

            route.MapGet("", async (SchedulingContext context) =>
            {
                var scheduling = await context.Scheduling.ToListAsync();
                return Results.Ok(scheduling);
            });

            route.MapGet("{id:int}", 
                async (int id, SchedulingContext context) =>
                {
                    var scheduling = await context.Scheduling
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (scheduling == null)
                        return Results.NotFound();

                    return Results.Ok(scheduling); 
                }
            );

            route.MapPut("{id:int}", 
                async (int id, SchedulingRequest req, SchedulingContext context) =>
                {
                    var scheduling = await context.Scheduling.FirstOrDefaultAsync(x => x.Id == id);
                    
                    if (scheduling == null)
                        return Results.NotFound(); 
                    scheduling.ChangeName(req.Name); 
                    scheduling.Inicio = req.Inicio;  
                    scheduling.Fim = req.Fim;        
                    scheduling.Periodos = req.Periodos; 

                    await context.SaveChangesAsync(); 
                    
                    return Results.Ok(scheduling); 
                }
            );

            route.MapDelete("{id:int}", 
                async (int id, SchedulingContext context) =>
                {
                    var scheduling = await context.Scheduling.FirstOrDefaultAsync(x => x.Id == id);

                    if (scheduling == null)
                        return Results.NotFound(); 

                    context.Scheduling.Remove(scheduling);  
                    await context.SaveChangesAsync(); 
                    return Results.Ok(scheduling);  
                }
            );

        }
    }
}

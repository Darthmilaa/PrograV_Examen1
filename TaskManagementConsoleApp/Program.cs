using System;
using TaskManagementLibrary; // RETO 1: Esta línea está bien

class Program
{
    static bool confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    }

    static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Ver tareas");
            Console.WriteLine("3. Actualizar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Completar tarea");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Titulo: ");
                    var title = Console.ReadLine();
                    Console.Write("Descripcion: ");
                    var description = Console.ReadLine();
                    // RETO 2: Verificar que los datos no sean solo espacios en blanco
                    if (string.IsNullOrWhiteSpace(title))
                        title = null;
                    if (string.IsNullOrWhiteSpace(description))
                        description = null;

                    var task = taskService.AddTask(title, description);
                    Console.WriteLine($"Tarea agregada con Id: {task.Id}");
                    break;
                case "2":
                    var tasks = taskService.GetAllTasks();
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var t in tasks)
                    {
                        Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                    }
                    Console.WriteLine("-------------------------------------------------");
                    break;
                case "3":
                    Console.Write("Introduzca el Id de la tarea por actualizar: ");
                    var updateId = int.Parse(Console.ReadLine());
                    task = taskService.GetTaskById(updateId); // RETO 3: Cargar la tarea con el id indicado

                    if (task != null)
                    {
                        Console.WriteLine($"Título actual: {task.Title}"); // RETO 4: Imprimir el título de la tarea seleccionada
                        Console.WriteLine($"Descripción actual: {task.Description}"); // RETO 5: Imprimir la descripción de la tarea seleccionada

                        Console.Write("-> Nuevo título: ");
                        var newTitle = Console.ReadLine();
                        Console.Write("-> Nueva Descripción: ");
                        var newDescription = Console.ReadLine();
                        Console.Write("Completada (true/false): ");
                        var isCompleted = bool.Parse(Console.ReadLine());

                        if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted)) // RETO 6: Modificar en la librería
                        {
                            Console.WriteLine("Tarea actualizada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "4":
                    Console.Write("Introduzca el Id de la tarea a eliminar: ");
                    var deleteId = 0;
                    try
                    {
                        deleteId = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        break;
                    }

                    task = taskService.GetTaskById(deleteId); // RETO 7: Llamado a GetTaskById corregido
                    if (task != null)
                    {
                        Console.WriteLine($"Tarea: {task.Title}");
                        if (confirme("eliminar"))
                        {
                            if (taskService.DeleteTask(deleteId))
                            {
                                Console.WriteLine("Tarea eliminada exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("Tarea no encontrada.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "5":
                    // RETO 8: Crear la funcionalidad completa del método para completar tarea
                    Console.Write("Introduzca el Id de la tarea a completar: ");
                    var completeId = int.Parse(Console.ReadLine());
                    var taskToComplete = taskService.GetTaskById(completeId);

                    if (taskToComplete != null)
                    {
                        Console.WriteLine($"Título de la tarea: {taskToComplete.Title}");
                        if (taskService.CompleteTaskById(completeId))
                        {
                            Console.WriteLine("Tarea completada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo completar la tarea.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }
        }
    }
}

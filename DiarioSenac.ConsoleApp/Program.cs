using DiarioSenac.Classes;

try
{
    MenuPrincipal menu = new MenuPrincipal();
    menu.Menu();
}
catch (Exception ex)
{
    Console.WriteLine("Ocorreu um erro ao iniciar a aplicação:");
    Console.WriteLine(ex.Message);
}


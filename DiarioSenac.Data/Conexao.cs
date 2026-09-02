using Microsoft.EntityFrameworkCore;

namespace DiarioSenac.Data;

public class Conexao
{
    public void InserirRegistro(Registro registro)
    {
        using var context = new DiarioSenacContext();
        context.Registros.Add(registro);
        context.SaveChanges();
    }

    public List<Registro> ListarRegistros()
    {
        using var context = new DiarioSenacContext();
        return context.Registros
            .OrderBy(r => r.Id)
            .ToList();
    }

    public Registro? BuscarRegistroPorId(int id)
    {
        using var context = new DiarioSenacContext();
        return context.Registros.FirstOrDefault(r => r.Id == id);
    }

    public void RemoverRegistro(int id)
    {
        using var context = new DiarioSenacContext();
        var registro = context.Registros.FirstOrDefault(r => r.Id == id);

        if (registro is null)
            return;

        context.Registros.Remove(registro);
        context.SaveChanges();
    }
}


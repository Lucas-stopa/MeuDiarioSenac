using Microsoft.EntityFrameworkCore;
using DiarioSenac.Model;

namespace DiarioSenac.Data;

public class RegistroDAO
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
            .Include(r => r.Usuario)
            .OrderBy(r => r.Id)
            .ToList();
    }

    /// <summary>
    /// Lista registros de um usuário específico
    /// </summary>
    public List<Registro> ListarRegistrosPorUsuario(int usuarioId)
    {
        using var context = new DiarioSenacContext();
        return context.Registros
            .Include(r => r.Usuario)
            .Where(r => r.UsuarioId == usuarioId)
            .OrderBy(r => r.Id)
            .ToList();
    }

    public Registro? BuscarRegistroPorId(int id)
    {
        using var context = new DiarioSenacContext();
        return context.Registros
            .Include(r => r.Usuario)
            .FirstOrDefault(r => r.Id == id);
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
    
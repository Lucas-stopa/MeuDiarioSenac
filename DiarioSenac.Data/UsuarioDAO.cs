using Microsoft.EntityFrameworkCore;

namespace DiarioSenac.Data;

public class UsuarioDAO
{
    public void InserirUsuario(Usuario usuario)
    {
        using var context = new DiarioSenacContext();
        context.Usuarios.Add(usuario);
        context.SaveChanges();
    }

    public List<Usuario> ListarUsuarios()
    {
        using var context = new DiarioSenacContext();
        return context.Usuarios
            .OrderBy(u => u.Id)
            .ToList();
    }

    public Usuario? BuscarUsuarioPorId(int id)
    {
        using var context = new DiarioSenacContext();
        return context.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public void AtualizarUsuario(Usuario usuario)
    {
        using var context = new DiarioSenacContext();
        var usuarioExistente = context.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);

        if (usuarioExistente is null)
            return;

        usuarioExistente.Nome = usuario.Nome;
        context.SaveChanges();
    }

    public void RemoverUsuario(int id)
    {
        using var context = new DiarioSenacContext();
        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario is null)
            return;

        context.Usuarios.Remove(usuario);
        context.SaveChanges();
    }
}

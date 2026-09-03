using MySql.Data.MySqlClient;
using DiarioSenac.Data;
using DiarioSenac.Model;

namespace DiarioSenac.Classes;

public class RegistroDAO
{
    public DiarioSenacContext bdconexao = new DiarioSenacContext();
    
    public void InserirRegistro(Registro registro)
    {
        bdconexao.Registros.Add(registro);
        bdconexao.SaveChanges();
    }

    public List<Registro> ListarRegistros()
    {
        return bdconexao.Registros.ToList();
    }

    public Registro? BuscarRegistroPorId(int id)
    {
        return bdconexao.Registros.FirstOrDefault(r => r.Id == id);
    }

    public void RemoverRegistro(int id)
    {
        var registro = BuscarRegistroPorId(id);
        if (registro != null)
        {
            bdconexao.Registros.Remove(registro);
            bdconexao.SaveChanges();
        }
    }
}
    
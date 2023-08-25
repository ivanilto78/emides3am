using ControledePet.Data;
using ControledePet.Models;

namespace ControledePet.Repositorio
{
    public class PetRepositorio: IPetRepositorio
    {

        private readonly BancoContext _bancoContext;
        public PetRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public List<PetModel> BuscarTodos()
        {
            return _bancoContext.Pets.ToList();
        }

        public PetModel Adicionar(PetModel pet)
        {
            //Gravar no banco
            _bancoContext.Pets.Add(pet);
            _bancoContext.SaveChanges();
           return pet;
        }

        public PetModel ListarPorId(int id)
        {
           return _bancoContext.Pets.FirstOrDefault(x => x.Id == id);
        }

        public PetModel Atualizar(PetModel pet)
        {
            PetModel petDB = ListarPorId(pet.Id);
            if (petDB == null) throw new SystemException("Houve um erro ao atualizar");

            petDB.Nome = pet.Nome;
            petDB.Genero = pet.Genero;
            petDB.Raca = pet.Raca;

            _bancoContext.Pets.Update(petDB);
            _bancoContext.SaveChanges();
 
            return petDB;

        }

        public bool Apagar(int id)
        {
            PetModel petDB = ListarPorId(id);
            if (petDB == null) throw new SystemException("Houve um erro ao atualizar");

          
            _bancoContext.Pets.Remove(petDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}

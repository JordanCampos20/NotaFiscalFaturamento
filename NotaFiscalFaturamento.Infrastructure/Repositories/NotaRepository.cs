using Microsoft.EntityFrameworkCore;
using NotaFiscalFaturamento.Domain.Entities;
using NotaFiscalFaturamento.Domain.Interfaces;
using NotaFiscalFaturamento.Infrastructure.Context;

namespace NotaFiscalFaturamento.Infrastructure.Repositories
{
    public class NotaRepository(ApplicationDbContext context) : INotaRepository
    {
        private readonly ApplicationDbContext _context = context;


        public Nota GetById(int id)
        {
            return _context.Notas
                .AsNoTracking()
                .Include(item => item.Produtos)
                .FirstOrDefault(item => item.Id == id)!;
        }

        public IEnumerable<Nota> GetNotas()
        {
            return [.. _context.Notas.Include(item => item.Produtos)];
        }

        public Nota? Create(Nota nota)
        {
            _context.Notas.Add(nota);

            if (_context.SaveChanges() > 0)
                return nota;

            return null;
        }

        public Nota? Update(int id, Nota nota)
        {
            if (id != nota.Id)
                throw new Exception("Nota não condiz com o do objeto!");
            
            _context.Notas.Update(nota);

            if (_context.SaveChanges() > 0)
                return nota;

            return null;
        }

        public bool Remove(int id)
        {
            Nota? nota = _context.Notas.Find(id);

            if (nota == null)
                throw new Exception("Nota não encontrado!");

            _context.Notas.Remove(nota);

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }
    }
}

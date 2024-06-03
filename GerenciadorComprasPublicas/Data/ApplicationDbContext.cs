using GerenciadorComprasPublicas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorComprasPublicas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Itens { get; set; }
        public DbSet<HistoricoPreco> HistoricoPrecos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Licitacao> Licitacoes { get; set; }
        public DbSet<FaseLicitacao> FasesLicitacao { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Secretaria> Secretarias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PlanejamentoAnual> PlanejamentosAnuais { get; set; }
        public DbSet<PlanejamentoItem> PlanejamentosItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }                
    }
}

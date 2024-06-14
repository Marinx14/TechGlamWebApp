using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Modelli;

namespace WebApp.Servizi
{
    public class CarrelloServizi
    {

        public ApplicationDbContext _dbContext { get; set; }

        public List<ProdottoClonato> _prodottiClonati { get; set; }

        public CarrelloServizi(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _prodottiClonati = new List<ProdottoClonato>();


        }
        public async Task<Carrello> GetCarrelloAsync(string IdUser)
        {
            var carrello = await _dbContext.CarrelloProdotti.FirstOrDefaultAsync(d => d.IDUtenteAssociato == IdUser);
            if (carrello == null)
            {
                carrello = new Carrello()
                {
                    IDCarrello = Guid.NewGuid(),
                    IDUtenteAssociato = IdUser,
                    prodottiClonati = new List<ProdottoClonato>()
                };
                await _dbContext.CarrelloProdotti.AddAsync(carrello);
                await _dbContext.SaveChangesAsync();
            }
            return carrello;
        }

        public async Task AggiungiProdottoCarrello(string idUser, Prodotto prodottoDaAggiungere, int quantita)
        {
            var prodottoEsistente = _dbContext.Prodotti.FirstOrDefault(p => p.IDProdotto == prodottoDaAggiungere.IDProdotto);
            var prodottoClonato = new ProdottoClonato(prodottoEsistente);
            var dettagliCarrello = await _dbContext.DettagliCarrello.ToListAsync();
            var carrello = await this.GetCarrelloAsync(idUser);
            dettagliCarrello = dettagliCarrello.Where(p => p.IDCarrelloAssociato.Equals(carrello.IDCarrello)).ToList();
            var esistente = dettagliCarrello.FirstOrDefault(pc => pc.Nome.Equals(prodottoClonato.Nome) && pc.Taglia.Equals(prodottoClonato.Taglia)
            && pc.IDCarrelloAssociato.Equals(carrello.IDCarrello));

            if (carrello != null)
            {
                if (esistente != null)
                {
                    esistente.QuantitaOrdinati += quantita;
                    _dbContext.Entry(esistente).State = EntityState.Modified;
                }
                else
                {
                    prodottoClonato.IDCarrelloAssociato = carrello.IDCarrello;
                    prodottoClonato.QuantitaOrdinati = quantita;
                    _dbContext.Entry(prodottoClonato).State = EntityState.Added;
                }
            }
                    await _dbContext.SaveChangesAsync();
        }

        public async Task EliminaProdottoCarrello(Guid idProdotto, string idUser)
        {
            var dettagliCarrello = await _dbContext.DettagliCarrello.ToListAsync();
            //Prendere ID del carrello associato all'utente per rimuovere
            var prodotto = _dbContext.DettagliCarrello.FirstOrDefault(p => p.IDProdottoClonato == idProdotto);
            if (prodotto != null)
            {
                 _dbContext.DettagliCarrello.Remove(prodotto);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task SvuotaCarrello(string idUser)
        {
            //Prendere ID del carrello associato all'utente per rimuovere
            var carrello = await this.GetCarrelloAsync(idUser);
            var dettagliCarrello = await _dbContext.DettagliCarrello.ToListAsync();
            dettagliCarrello = dettagliCarrello.Where(p => p.IDCarrelloAssociato.Equals(carrello.IDCarrello)).ToList();
            foreach (var item in dettagliCarrello) 
            {
                _dbContext.DettagliCarrello.RemoveRange(item);
            }
            //_dbContext.DettagliCarrello.RemoveRange(carrello.prodottiClonati);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Prodotto> GetProdottoIdAsync(Guid idProdotto)
        {
            return await _dbContext.Prodotti.FirstOrDefaultAsync(p => p.IDProdotto == idProdotto);
        }
        public async Task<bool> InvioProdotti(string idUtente)
        {
            var carrello = await GetCarrelloAsync(idUtente);
            var dettagliCarrello = await _dbContext.DettagliCarrello.ToListAsync();
            dettagliCarrello = dettagliCarrello.Where(p => p.IDCarrelloAssociato.Equals(carrello.IDCarrello)).ToList();
            if (carrello != null && dettagliCarrello.Any())
            {
                foreach (var prodotto in carrello.prodottiClonati)
                {
                    var prodottoNelDb = await GetProdottoIdAsync(prodotto.IDProdottoClonato);
                    if (prodottoNelDb != null)
                    {
                        if (prodottoNelDb.Quantita < prodotto.QuantitaOrdinati)
                        {
                            return false;
                        }
                        else { 
                        prodottoNelDb.Quantita -= prodotto.QuantitaOrdinati;
                        _dbContext.Prodotti.Update(prodottoNelDb);
                        _dbContext.DettagliCarrello.Remove(prodotto);
                        carrello.prodottiClonati.Remove(prodotto);
                        _dbContext.CarrelloProdotti.Update(carrello);

                        await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                await SvuotaCarrello(idUtente);
                return true;
            }
            return false;
        }


        public async Task<List<ProdottoClonato>> ProdottiClonatiAsync(Carrello carrello)
        {
            var result = await _dbContext.DettagliCarrello.ToListAsync();
            result = result.Where(p => p.IDCarrelloAssociato.Equals(carrello.IDCarrello)).DistinctBy(t => t.Nome).ToList();
            return result;
        }

    }
}
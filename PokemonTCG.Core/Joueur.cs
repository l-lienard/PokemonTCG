using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TPPokemon
{
    public class Joueur
    {
        public const int TailleBancMax = 5;

        public string Nom { get; }
        public List<Carte> Deck {  get; }
        public List<Carte> Main { get; } = new List<Carte>();
        public CartePokemon PokemonActif { get; private set; }
        public List<CartePokemon> Banc { get; } = new List<CartePokemon>();

        private bool aPiocherCeTour = false;
        private bool EnergieAttacheeCeTour = false;
        private bool aAttaquerCeTour = false;

        public Joueur(string nom, List<Carte> deck)
        {
            Nom = nom;
            Deck = deck;
        }

        public Carte Piocher()
        {
            if(aPiocherCeTour)
            {
                throw new InvalidOperationException("Le joueur à déja piocher ce tour..");
            }
            if(Deck.Count == 0)
            {
                throw new InvalidOperationException("Le deck du joueur est vide..");
            }
            Carte cartePiochee = Deck[0];
            Deck.RemoveAt(0);
            Main.Add(cartePiochee);
            aPiocherCeTour = true;

            return cartePiochee;
        }

        public void JouerCarte(Carte carte)
        {
            if(!Main.Contains(carte))
            {
                throw new InvalidOperationException("La carte n'est pas dans la main");
            }

            switch(carte)
            {
                case CartePokemon pokemon:
                    if(PokemonActif == null)
                    {
                        PokemonActif = pokemon;
                    }
                    else
                    {
                        if(Banc.Count >= TailleBancMax)
                        {
                            throw new InvalidOperationException("Le banc est plein..");
                        }
                        Banc.Add(pokemon);
                    }
                    break;

                case CarteEnergie energie:
                    if(PokemonActif == null)
                    {
                        throw new InvalidOperationException("Il n'y a pas de Pokemon Actif");
                    }
                    if(EnergieAttacheeCeTour)
                    {
                        throw new InvalidOperationException("Une énergie à déjà été attachée ce tour..");
                    }
                    PokemonActif.AttacherEnergie();
                    EnergieAttacheeCeTour = true;
                    break;

                case CarteDresseur dresseur:
                    Console.WriteLine(dresseur.Effet);
                    break;
            }

            Main.Remove(carte);
        }

        public int Attaquer(Attaque attaque, Joueur adversaire)
        {
            if (PokemonActif == null || adversaire.PokemonActif == null)
            {
                throw new InvalidOperationException("Les deux joueurs doivent avoir un Pokemon Actif..");
            }
            if(!PokemonActif.Attaques.Contains(attaque))
            {
                throw new ArgumentException("L'attaque n'appartient à aucun pokemon actif..");
            }
            if(PokemonActif.EnergiesAttachees < attaque.Cout)
            {
                throw new InvalidOperationException("Le cout de l'attaque est supérieur aux énergies attachées..");
            }
            if(aAttaquerCeTour == true)
            {
                throw new InvalidOperationException("Vous avez déjà attaquer ce tour..");
            }
            adversaire.PokemonActif.RecevoirDegats(attaque.Degats);
            aAttaquerCeTour = true;
            return attaque.Degats;
        }

        public void PasserSonTour()
        {
            aAttaquerCeTour = false;
            aPiocherCeTour = false;
            EnergieAttacheeCeTour = false;
        }

    }
}

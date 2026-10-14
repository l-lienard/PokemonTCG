using System;
using System.Collections.Generic;
using System.Text;

namespace TPPokemon
{
    public enum Rarete { Commune, PeuCommune, Rare, HoloRare, UltraRare}
    public enum TypePokemon
    {
        Plante, Feu, Eau, Electrik, Psy, Combat, Obscurite, Metal, Normal
    }

    public class Attaque
    {
        public string Nom { get; }
        public int Degats { get; }
        public int Cout { get; }

        public Attaque(string nom, int degats, int cout)
        {
            Nom = nom;
            Degats = degats;
            Cout = cout;
        }
    }

    public abstract class Carte
    {
        public string Nom {  get; set; }
        public string Description { get; set; }
        public Rarete Rarete { get; set; }

        protected Carte(string nom, string description, Rarete rarete)
        {
            Nom = nom;
            Description = description;
            Rarete = rarete;
        }

        public virtual string Decrire()
        {
            return $"{Nom} ({Rarete}) : {Description}";
            throw new NotImplementedException();
        }
    }

    public class CartePokemon : Carte
    {
        public int PointsDeVie { get; private set; }
        public TypePokemon Type { get; }
        public int EnergiesAttachees { get; private set; }
        public List<Attaque> Attaques { get; } = new List<Attaque>();

        public bool EstKO
        {
            get
            {
                return this.PointsDeVie <= 0;
            }
        }

        public CartePokemon(string nom, string description, Rarete rarete, int pv, TypePokemon type) : base(nom, description, rarete)
        {
            PointsDeVie = pv;
            Type = type;
        }

        public void AttacherEnergie()
        {
            this.EnergiesAttachees++;
        }

        public void RecevoirDegats(int degats)
        {
            if(degats < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            PointsDeVie = Math.Max(0, PointsDeVie - degats);
        }

        public override string Decrire()
        {
            return $"{base.Decrire()} | PV : {PointsDeVie} | Type : {Type} ";
        }
    }

    public class CarteEnergie : Carte
    {
        public TypePokemon Type { get; }
        
        public CarteEnergie(string nom, TypePokemon type) : base(nom, "Carte énergie", Rarete.Commune)
        {
            this.Type = type;
        }

    }

    public class CarteDresseur : Carte
    {
        public string Categorie { get; }
        public string Effet { get; }

        public CarteDresseur(string nom, Rarete rarete, string categorie, string effet) : base(nom, effet, rarete)
        {
            this.Categorie = categorie;
            this.Effet = effet;
        }
    }

}

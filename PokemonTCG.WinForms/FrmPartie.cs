using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPPokemon;

namespace PokemonTCG.WinForms
{
    public partial class FrmPartie : Form
    {
        private Joueur _joueur;
        private Joueur _adversaire;

        public bool _AlternerJoueurs;

        public FrmPartie(Joueur joueur, Joueur adversaire, bool alternerJoueur = false)
        {
            InitializeComponent();

            _joueur = joueur;
            _adversaire = adversaire;
            _AlternerJoueurs = alternerJoueur;
            grpJoueur.Text = _joueur.Nom;

            lstMain.DisplayMember = "Nom";
            lstBanc.DisplayMember = "Nom";
            cmbAttaques.DisplayMember = "Nom";

            ActualiserAffichage();
        }

        private void ActualiserAffichage()
        {
            lblDeck.Text = $"Deck : {_joueur.Deck.Count} carte(s)";

            lstMain.Items.Clear();
            lstMain.Items.AddRange(_joueur.Main.ToArray());

            lstBanc.Items.Clear();
            lstBanc.Items.AddRange(_joueur.Banc.ToArray());

            CartePokemon? actif = _joueur.PokemonActif;
            if (actif != null)
            {
                lblActif.Text = actif.Decrire();
                lblEnergies.Text = $"Énergies : {actif.EnergiesAttachees}";
            }
            else
            {
                lblActif.Text = "Aucun Pokémon actif";
                lblEnergies.Text = "Énergies : 0";
            }

            cmbAttaques.Items.Clear();
            if (actif != null)
            {
                cmbAttaques.Items.AddRange(actif.Attaques.ToArray());
            }

            CartePokemon? actifAdversaire = _adversaire.PokemonActif;
            if (actifAdversaire != null)
            {
                lblActifAdversaire.Text = actifAdversaire.Decrire();
            }
            else
            {
                lblActifAdversaire.Text = "Aucun Pokémon actif";
            }
        }

        private void AfficherMessage(string texte, bool erreur)
        {
            lblMessage.Text = texte;
            if (erreur)
            {
                lblMessage.ForeColor = Color.Red;
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
            }
        }

        private void btnAttaquer_Click(object sender, EventArgs e)
        {
            if (cmbAttaques.SelectedItem == null)
            {
                AfficherMessage("Sélectionnez une attaque.", true);
                return;
            }

            Attaque attaque = (Attaque)cmbAttaques.SelectedItem;

            try
            {
                int degats = _joueur.Attaquer(attaque, _adversaire);
                string message = $"{attaque.Nom} inflige {degats} dégâts !";

                if (_adversaire.PokemonActif != null && _adversaire.PokemonActif.EstKO)
                {
                    message += " Le Pokémon adverse est K.O. !";
                }


                AfficherMessage(message, false);
            }
            catch (InvalidOperationException ex)
            {
                AfficherMessage(ex.Message, true);
            }
            catch (ArgumentException ex)
            {
                AfficherMessage(ex.Message, true);
            }
            ActualiserAffichage();
        }

        private void btnPasserTour_Click(object sender, EventArgs e)
        {
            _joueur.PasserSonTour();

            if (_AlternerJoueurs)
            {
                Joueur ancien = _joueur;
                _joueur = _adversaire;
                _adversaire = ancien;

                _joueur.PasserSonTour();

                grpJoueur.Text = _joueur.Nom;
                grpAdversaire.Text = _adversaire.Nom;
                AfficherMessage($"Tour terminé. C'est à {_joueur.Nom} de jouer.", false);
            }
            else
            {
                AfficherMessage("Tour terminé.", false);
            }

            ActualiserAffichage();
        }

        private void btnJouerCarte_Click(object sender, EventArgs e)
        {
            if (lstMain.SelectedItem == null)
            {
                AfficherMessage("Sélectionnez une carte.", true);
                return;
            }

            Carte carte = (Carte)lstMain.SelectedItem;

            try
            {
                _joueur.JouerCarte(carte);
                AfficherMessage($"{carte.Nom} jouée.", false);
            }
            catch (InvalidOperationException ex)
            {
                AfficherMessage(ex.Message, true);
            }
            ActualiserAffichage();
        }

        private void btnPiocher_Click(object sender, EventArgs e)
        {
            try
            {
                Carte carte = _joueur.Piocher();
                AfficherMessage($"Vous avez pioché : {carte.Nom}", false);
            }
            catch (InvalidOperationException ex)
            {
                AfficherMessage(ex.Message, true);
            }

            ActualiserAffichage();
        }
    }
}
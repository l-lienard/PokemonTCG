namespace PokemonTCG.WinForms
{
    partial class FrmPartie
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpJoueur = new GroupBox();
            btnPasserTour = new Button();
            btnAttaquer = new Button();
            btnJouerCarte = new Button();
            btnPiocher = new Button();
            cmbAttaques = new ComboBox();
            lblEnergies = new Label();
            lblActif = new Label();
            lstBanc = new ListBox();
            lstMain = new ListBox();
            lblDeck = new Label();
            grpAdversaire = new GroupBox();
            lblActifAdversaire = new Label();
            lblMessage = new Label();
            grpJoueur.SuspendLayout();
            grpAdversaire.SuspendLayout();
            SuspendLayout();
            // 
            // grpJoueur
            // 
            grpJoueur.Controls.Add(btnPasserTour);
            grpJoueur.Controls.Add(btnAttaquer);
            grpJoueur.Controls.Add(btnJouerCarte);
            grpJoueur.Controls.Add(btnPiocher);
            grpJoueur.Controls.Add(cmbAttaques);
            grpJoueur.Controls.Add(lblEnergies);
            grpJoueur.Controls.Add(lblActif);
            grpJoueur.Controls.Add(lstBanc);
            grpJoueur.Controls.Add(lstMain);
            grpJoueur.Controls.Add(lblDeck);
            grpJoueur.Location = new Point(12, 12);
            grpJoueur.Name = "grpJoueur";
            grpJoueur.Size = new Size(513, 375);
            grpJoueur.TabIndex = 0;
            grpJoueur.TabStop = false;
            grpJoueur.Text = "Joueur";
            // 
            // btnPasserTour
            // 
            btnPasserTour.Font = new Font("Segoe UI", 12F);
            btnPasserTour.Location = new Point(308, 299);
            btnPasserTour.Name = "btnPasserTour";
            btnPasserTour.Size = new Size(177, 41);
            btnPasserTour.TabIndex = 9;
            btnPasserTour.Text = "Passer Tour";
            btnPasserTour.UseVisualStyleBackColor = true;
            btnPasserTour.Click += btnPasserTour_Click;
            // 
            // btnAttaquer
            // 
            btnAttaquer.Font = new Font("Segoe UI", 12F);
            btnAttaquer.Location = new Point(263, 246);
            btnAttaquer.Name = "btnAttaquer";
            btnAttaquer.Size = new Size(222, 36);
            btnAttaquer.TabIndex = 8;
            btnAttaquer.Text = "Attaquer";
            btnAttaquer.UseVisualStyleBackColor = true;
            btnAttaquer.Click += btnAttaquer_Click;
            // 
            // btnJouerCarte
            // 
            btnJouerCarte.Font = new Font("Segoe UI", 12F);
            btnJouerCarte.Location = new Point(159, 299);
            btnJouerCarte.Name = "btnJouerCarte";
            btnJouerCarte.Size = new Size(127, 41);
            btnJouerCarte.TabIndex = 7;
            btnJouerCarte.Text = "Jouer Carte";
            btnJouerCarte.UseVisualStyleBackColor = true;
            btnJouerCarte.Click += btnJouerCarte_Click;
            // 
            // btnPiocher
            // 
            btnPiocher.Font = new Font("Segoe UI", 12F);
            btnPiocher.Location = new Point(14, 299);
            btnPiocher.Name = "btnPiocher";
            btnPiocher.Size = new Size(130, 41);
            btnPiocher.TabIndex = 6;
            btnPiocher.Text = "Piocher";
            btnPiocher.UseVisualStyleBackColor = true;
            btnPiocher.Click += btnPiocher_Click;
            // 
            // cmbAttaques
            // 
            cmbAttaques.FormattingEnabled = true;
            cmbAttaques.Location = new Point(14, 259);
            cmbAttaques.Name = "cmbAttaques";
            cmbAttaques.Size = new Size(220, 23);
            cmbAttaques.TabIndex = 5;
            // 
            // lblEnergies
            // 
            lblEnergies.BackColor = Color.LemonChiffon;
            lblEnergies.Font = new Font("Segoe UI", 12F);
            lblEnergies.Location = new Point(327, 203);
            lblEnergies.Name = "lblEnergies";
            lblEnergies.Size = new Size(151, 37);
            lblEnergies.TabIndex = 4;
            lblEnergies.Text = "label1";
            // 
            // lblActif
            // 
            lblActif.BackColor = Color.LemonChiffon;
            lblActif.Font = new Font("Segoe UI", 9F);
            lblActif.Location = new Point(14, 206);
            lblActif.Name = "lblActif";
            lblActif.Size = new Size(286, 30);
            lblActif.TabIndex = 3;
            lblActif.Text = "label1";
            // 
            // lstBanc
            // 
            lstBanc.FormattingEnabled = true;
            lstBanc.Location = new Point(263, 55);
            lstBanc.Name = "lstBanc";
            lstBanc.Size = new Size(222, 139);
            lstBanc.TabIndex = 2;
            // 
            // lstMain
            // 
            lstMain.FormattingEnabled = true;
            lstMain.Location = new Point(14, 55);
            lstMain.Name = "lstMain";
            lstMain.Size = new Size(220, 139);
            lstMain.TabIndex = 1;
            // 
            // lblDeck
            // 
            lblDeck.AutoSize = true;
            lblDeck.Location = new Point(29, 35);
            lblDeck.Name = "lblDeck";
            lblDeck.Size = new Size(38, 15);
            lblDeck.TabIndex = 0;
            lblDeck.Text = "label1";
            // 
            // grpAdversaire
            // 
            grpAdversaire.Controls.Add(lblActifAdversaire);
            grpAdversaire.Location = new Point(541, 12);
            grpAdversaire.Name = "grpAdversaire";
            grpAdversaire.Size = new Size(237, 375);
            grpAdversaire.TabIndex = 1;
            grpAdversaire.TabStop = false;
            grpAdversaire.Text = "Adversaire";
            // 
            // lblActifAdversaire
            // 
            lblActifAdversaire.BackColor = Color.LemonChiffon;
            lblActifAdversaire.Location = new Point(26, 55);
            lblActifAdversaire.Name = "lblActifAdversaire";
            lblActifAdversaire.Size = new Size(186, 122);
            lblActifAdversaire.TabIndex = 0;
            lblActifAdversaire.Text = "label1";
            // 
            // lblMessage
            // 
            lblMessage.BackColor = SystemColors.ButtonHighlight;
            lblMessage.BorderStyle = BorderStyle.FixedSingle;
            lblMessage.Location = new Point(12, 395);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(766, 40);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "label1";
            // 
            // FrmPartie
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMessage);
            Controls.Add(grpAdversaire);
            Controls.Add(grpJoueur);
            Name = "FrmPartie";
            Text = "FrmPartie";
            grpJoueur.ResumeLayout(false);
            grpJoueur.PerformLayout();
            grpAdversaire.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpJoueur;
        private ListBox lstMain;
        private Label lblDeck;
        private Button btnPiocher;
        private ComboBox cmbAttaques;
        private Label lblEnergies;
        private Label lblActif;
        private ListBox lstBanc;
        private Button btnPasserTour;
        private Button btnAttaquer;
        private Button btnJouerCarte;
        private GroupBox grpAdversaire;
        private Label lblActifAdversaire;
        private Label lblMessage;
    }
}
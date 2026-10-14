using PokemonTCG;
using TPPokemon;

namespace PokemonTCG.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var catalogue = PokemonApiService.ChargerCatalogueAsync().GetAwaiter().GetResult();

            Joueur Kulo = new Joueur("Kulo", PokemonApiService.CreerDeck(catalogue));
            Joueur Braun = new Joueur("Braun", PokemonApiService.CreerDeck(catalogue));

            Braun.Piocher();
            Braun.JouerCarte(Braun.Main[0]);   

            Application.Run(new FrmPartie(Kulo, Braun, true));
        }
    }
}
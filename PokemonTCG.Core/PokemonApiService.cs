using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TPPokemon
{
    // ---- Classes qui reflètent le JSON de pokemontcg.io ----
    public class ApiResponse
    {
        public List<ApiCard> Data { get; set; } = new();
    }

    public class ApiCard
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Hp { get; set; }              // string dans l'API !
        public List<string>? Types { get; set; }
        public List<ApiAttack>? Attacks { get; set; }
        public string? Rarity { get; set; }
        public string? FlavorText { get; set; }
    }

    public class ApiAttack
    {
        public string? Name { get; set; }
        public int ConvertedEnergyCost { get; set; }  // nombre d'énergies requises
        public string? Damage { get; set; }           // "30", "20+", "" ...
    }

    // ---- Service : chargement + conversion vers TES classes ----
    public static class PokemonApiService
    {
        private static readonly HttpClient _http = new HttpClient(new HttpClientHandler
        {
            Proxy = new WebProxy("http://192.168.100.1:3128")
            {
                Credentials = new NetworkCredential("llienard", "JointsAbries14")
            },
            UseProxy = true
        })
        { Timeout = TimeSpan.FromSeconds(60) };
        private static readonly string _cheminCache = Path.Combine(AppContext.BaseDirectory, "pokemons_cache.json");
        private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

        /// <summary>
        /// Charge le catalogue (depuis le cache local s'il existe, sinon depuis l'API).
        /// 1 page = 250 cartes max.
        /// </summary>
        public static async Task<List<ApiCard>> ChargerCatalogueAsync(int pages = 1)
        {
            if (File.Exists(_cheminCache))
            {
                string jsonCache = await File.ReadAllTextAsync(_cheminCache);
                var depuisCache = JsonSerializer.Deserialize<List<ApiCard>>(jsonCache, _options);
                if (depuisCache != null && depuisCache.Count > 0)
                    return depuisCache;
            }

            // Pokémon de base uniquement (pas d'évolutions dans ton jeu)
            string q = Uri.EscapeDataString("supertype:Pokémon subtypes:Basic");
            string champs = "id,name,hp,types,attacks,rarity,flavorText";

            var catalogue = new List<ApiCard>();
            for (int page = 1; page <= pages; page++)
            {
                string url = $"https://api.pokemontcg.io/v2/cards?q={q}&pageSize=250&page={page}&select={champs}";
                string json = await _http.GetStringAsync(url);
                var reponse = JsonSerializer.Deserialize<ApiResponse>(json, _options);
                if (reponse == null || reponse.Data.Count == 0) break;
                catalogue.AddRange(reponse.Data);
            }

            await File.WriteAllTextAsync(_cheminCache, JsonSerializer.Serialize(catalogue));
            return catalogue;
        }

        /// <summary>
        /// Crée un deck mélangé (Pokémon + énergies correspondant à leurs types).
        /// Le premier Pokémon est placé en haut du deck pour pouvoir démarrer.
        /// </summary>
        public static List<Carte> CreerDeck(List<ApiCard> catalogue, int nbPokemon = 8, int nbEnergies = 12)
        {
            var rng = Random.Shared;
            var pokemons = new List<CartePokemon>();

            foreach (var apiCard in catalogue.OrderBy(_ => rng.Next()))
            {
                var carte = Convertir(apiCard);   // nouvelle instance à chaque fois
                if (carte != null) pokemons.Add(carte);
                if (pokemons.Count == nbPokemon) break;
            }

            if (pokemons.Count == 0)
                throw new InvalidOperationException("Aucun Pokémon valide dans le catalogue.");

            var deck = new List<Carte>(pokemons);

            for (int i = 0; i < nbEnergies; i++)
            {
                TypePokemon type = pokemons[rng.Next(pokemons.Count)].Type;
                deck.Add(new CarteEnergie($"Energie {type}", type));
            }

            deck = deck.OrderBy(_ => rng.Next()).ToList();

            // Garantit un Pokémon en haut du deck
            Carte premier = deck.First(c => c is CartePokemon);
            deck.Remove(premier);
            deck.Insert(0, premier);

            return deck;
        }

        private static CartePokemon? Convertir(ApiCard c)
        {
            if (string.IsNullOrEmpty(c.Name)) return null;
            if (!int.TryParse(c.Hp, out int pv)) return null;
            if (c.Types == null || c.Types.Count == 0) return null;
            if (c.Attacks == null || c.Attacks.Count == 0) return null;

            var carte = new CartePokemon(
                c.Name,
                c.FlavorText ?? "",
                ConvertirRarete(c.Rarity),
                pv,
                ConvertirType(c.Types[0]));

            foreach (var a in c.Attacks)
            {
                carte.Attaques.Add(new Attaque(a.Name ?? "Attaque", ExtraireDegats(a.Damage), a.ConvertedEnergyCost));
            }
            return carte;
        }

        private static TypePokemon ConvertirType(string type) => type switch
        {
            "Grass" => TypePokemon.Plante,
            "Fire" => TypePokemon.Feu,
            "Water" => TypePokemon.Eau,
            "Lightning" => TypePokemon.Electrik,
            "Psychic" or "Fairy" => TypePokemon.Psy,
            "Fighting" => TypePokemon.Combat,
            "Darkness" => TypePokemon.Obscurite,
            "Metal" => TypePokemon.Metal,
            _ => TypePokemon.Normal          // Colorless, Dragon...
        };

        private static Rarete ConvertirRarete(string? rarity)
        {
            if (string.IsNullOrEmpty(rarity)) return Rarete.Commune;
            if (rarity.Contains("Ultra") || rarity.Contains("Secret") || rarity.Contains("Illustration")) return Rarete.UltraRare;
            if (rarity.Contains("Holo")) return Rarete.HoloRare;
            if (rarity.Contains("Uncommon")) return Rarete.PeuCommune;
            if (rarity.Contains("Common")) return Rarete.Commune;
            if (rarity.Contains("Rare")) return Rarete.Rare;
            return Rarete.Commune;
        }

        // "30" -> 30, "20+" -> 20, "" -> 0
        private static int ExtraireDegats(string? damage)
        {
            string chiffres = new string((damage ?? "").Where(char.IsDigit).ToArray());
            return int.TryParse(chiffres, out int d) ? d : 0;
        }
    }
}
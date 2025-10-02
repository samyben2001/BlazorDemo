using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Pages
{
    public partial class Pendu
    {

        private const int MAX_ERRORS = 10;
        private int _errors = 0;

        private List<string> _mots = new List<string>(){
    "abstraction", "acupuncture", "aerodynamique", "alchimiste", "allegorie",
    "anagramme", "androide", "anachronisme", "archipel", "artificiel",
    "bibliotheque", "brouhaha", "brasserie", "boulevard", "bourrasque",
    "cacophonie", "calligraphie", "cathedrale", "chrysantheme", "cinquantaine",
    "consonance", "confiture", "cryptographie", "cartographie", "cylindre",
    "democratie", "dissonance", "divergence", "dichotomie", "dynastie",
    "enigmatique", "equateur", "equilibre", "espionnage", "euphonie",
    "fantastique", "foudre", "frissonner", "formidable", "fugitive",
    "gargantuesque", "geographie", "geometrie", "gladiateur", "gouvernance",
    "harmonique", "hermetique", "hydrogene", "hologramme", "hexagone",
    "illustre", "illusion", "inextinguible", "incongru", "invisible",
    "jacquard", "judiciaire", "jonglerie", "jubilation", "justicier",
    "kaléidoscope", "karyotype", "kilometrage", "kiosquier", "kinetique",
    "labyrinthe", "langoustine", "legendaire", "lexicologie", "liturgie",
    "magnanime", "manuscrit", "megalithe", "metaphore", "mystification",
    "nebulisation", "necropole", "neologisme", "neurasthenie", "noctambule",
    "onomatopee", "ophtalmologie", "orchestre", "ornement", "oscillation",
    "paradoxe", "paradigme", "perspicace", "pyromane", "polyglotte",
    "quadrature", "quintessence", "quotidien", "quincaillerie", "quarantenaire",
    "rhizome", "rhythmique", "radiographie", "rigoureux", "rubiscent"
};
        private string? _motATrouver = null;
        private string _motMasque = string.Empty; 
        private List<char> _propositions = new List<char>();
        private char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private string? _messageFin = null;
        private string? _errorMessage = null;
        
        
        public void StartGame()
        {
            Random rng = new Random();
            _motATrouver = _mots[rng.Next(0, _mots.Count)].ToUpper();
            _motMasque = new string('*', _motATrouver.Length);
            _messageFin = null;
        }

        public void OnLetterSelected(ChangeEventArgs e)
        {
            if (e.Value is null) return;

            char proposition = e.Value.ToString()!.ToUpper()[0];
            Console.WriteLine(proposition);

            // Check si proposition déjà effectuée
            if (_propositions.Contains(proposition))
            {
                _errorMessage = "Proposition déjà éffectuée!";
                _errors++;
                if (_errors == MAX_ERRORS)
                {
                    _motMasque = _motATrouver!;
                    ResetGame($"Perdu! Le mot à trouver était {_motMasque}");
                }
                return;
            }

            _propositions.Add(proposition);

            // Check si proposition est absente du mot
            if (!_motATrouver!.Contains(proposition))
            {
                _errorMessage = "Proposition non présente!";
                _errors++;
                if (_errors == MAX_ERRORS)
                {
                    _motMasque = _motATrouver!;
                    ResetGame($"Perdu! Le mot à trouver était {_motMasque}");
                }
                return;
            }

            _errorMessage = null;
            // Mise à jour du mot masqué
            for (int i = 0; i < _motATrouver.Length; i++)
            {

                if (_motATrouver[i] == proposition)
                {
                    char[] temp = _motMasque.ToCharArray();
                    temp[i] = proposition;
                    _motMasque = new string(temp);
                }
            }


            if (_motMasque == _motATrouver)
            {
                ResetGame("Bravo! Vous avez trouver le mot masqué!");
            }
        }

        private void ResetGame(string endMessage)
        {
            _motATrouver = null;
            _errorMessage = null;
            _errors = 0; 
            _propositions = new List<char>();
            _messageFin = endMessage;
        }
    }
}
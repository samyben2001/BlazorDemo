using BlazorDemo.Enums;
using BlazorDemo.Models.Entities;

namespace BlazorDemo.Pages
{
    public partial class Demineur
    {
        private const int _NB_CASES = 81;
        private const int _NB_BOMBES = 10;
        private List<DemineurCaseInfos> _cases;
        private bool? _isGameOver = null;
        private bool _isFirstClick = true;
        private int _bombeRestantes = 0;

        protected override void OnInitialized()
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            _isGameOver = null;
            _isFirstClick = true;
            _bombeRestantes = 0;
            InitializeBoard();
        }

        private void OnCaseSelected(DemineurCaseInfos selectedCase)
        {
            if (_isGameOver == null)
            {
                // initialise les bombes si premier click de la partie
                if (_isFirstClick)
                {
                    _isFirstClick = false;
                    InitializeBombs(selectedCase);
                }

                // défaite si la case sélectionée est une bombe
                if (selectedCase.Etat == DemineurEtat.Bombe)
                {
                    _isGameOver = true;
                    // révele le plateau complet
                    foreach (DemineurCaseInfos c in _cases)
                    {
                        RevealEndCase(c);
                    }
                    return;
                }

                // révèle la case sélectionée
                RevealCase(selectedCase);

                // victoire si toutes les cases non-bombes ont été révelées
                if (!_cases.Any(c => c.Etat != DemineurEtat.Bombe && !c.IsActive))
                {
                    _isGameOver = false;
                }
            }
        }

        private void OnCaseFlaged(DemineurCaseInfos c)
        {
            if (_isGameOver == null)
            {
                if (!_isFirstClick)
                {
                    c.IsFlaged = !c.IsFlaged;
                    if (c.IsFlaged)
                    {
                        _bombeRestantes--;
                    }
                    else
                    {
                        _bombeRestantes++;
                    }
                }
            }
        }

        private void InitializeBoard()
        {
            _cases = new List<DemineurCaseInfos>(_NB_CASES);
            // remplissage du plateau avec des cases d'un état inconnu
            for (int i = 0; i < Math.Sqrt(_NB_CASES); i++)
            {
                for (int j = 0; j < Math.Sqrt(_NB_CASES); j++)
                {
                    _cases.Add(new DemineurCaseInfos(i, j, DemineurEtat.Inconnu));
                }
            }
        }

        private void InitializeBombs(DemineurCaseInfos selectedCase)
        {
            // placement des bombes de manières aléatoire
            for (int i = 0; i < _NB_BOMBES; i++)
            {
                Random rng = new Random();
                int j = rng.Next(0, _NB_CASES);
                // évite qu'une bombe soit posée plusieurs fois sur la même case ou sur la case sélectionée
                if (_cases[j].Etat == DemineurEtat.Bombe || _cases[j] == selectedCase)
                {
                    i--;
                }
                else
                {
                    _cases[j].Etat = DemineurEtat.Bombe;
                    _bombeRestantes++;
                }
            }
        }

        private void RevealCase(DemineurCaseInfos selectedCase)
        {
            selectedCase.IsActive = true;
            // récupération des cases connexes
            IEnumerable<DemineurCaseInfos> caseConnexes = _cases.Where(c => Math.Abs(c.X - selectedCase.X) < 2 && Math.Abs(c.Y - selectedCase.Y) < 2 && c != selectedCase && !c.IsActive);

            int nbBombesConnexes = CalculateAdjacentBombs(selectedCase, caseConnexes);

            // révélation des cases connexes en si la case sélectionnée est entourée de 0 bombes
            if (nbBombesConnexes == 0)
            {
                foreach (DemineurCaseInfos caseConnexe in caseConnexes)
                {
                    caseConnexe.IsActive = true;
                    RevealCase(caseConnexe);
                }
            }
        }

        private void RevealEndCase(DemineurCaseInfos selectedCase)
        {
            selectedCase.IsActive = true;
            // récupération des cases connexes
            IEnumerable<DemineurCaseInfos> caseConnexes = _cases.Where(c => Math.Abs(c.X - selectedCase.X) < 2 && Math.Abs(c.Y - selectedCase.Y) < 2 && c != selectedCase);
            CalculateAdjacentBombs(selectedCase, caseConnexes);
        }

        private static int CalculateAdjacentBombs(DemineurCaseInfos selectedCase, IEnumerable<DemineurCaseInfos> caseConnexes)
        {
            // calcul du nombres de bombes connexes
            int nbBombesConnexes = 0;
            foreach (DemineurCaseInfos caseConnexe in caseConnexes)
            {
                if (caseConnexe.Etat == DemineurEtat.Bombe)
                {
                    nbBombesConnexes++;
                }
            }

            // assigne l'état de la bombe en fonction du nombre de bombes adjacentes
            if (selectedCase.Etat != DemineurEtat.Bombe)
            {
                selectedCase.Etat = (DemineurEtat)nbBombesConnexes;
            }

            return nbBombesConnexes;
        }
    }
}
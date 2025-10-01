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

                // d�faite si la case s�lection�e est une bombe
                if (selectedCase.Etat == DemineurEtat.Bombe)
                {
                    _isGameOver = true;
                    // r�vele le plateau complet
                    foreach (DemineurCaseInfos c in _cases)
                    {
                        RevealEndCase(c);
                    }
                    return;
                }

                // r�v�le la case s�lection�e
                RevealCase(selectedCase);

                // victoire si toutes les cases non-bombes ont �t� r�vel�es
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
            // remplissage du plateau avec des cases d'un �tat inconnu
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
            // placement des bombes de mani�res al�atoire
            for (int i = 0; i < _NB_BOMBES; i++)
            {
                Random rng = new Random();
                int j = rng.Next(0, _NB_CASES);
                // �vite qu'une bombe soit pos�e plusieurs fois sur la m�me case ou sur la case s�lection�e
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
            // r�cup�ration des cases connexes
            IEnumerable<DemineurCaseInfos> caseConnexes = _cases.Where(c => Math.Abs(c.X - selectedCase.X) < 2 && Math.Abs(c.Y - selectedCase.Y) < 2 && c != selectedCase && !c.IsActive);

            int nbBombesConnexes = CalculateAdjacentBombs(selectedCase, caseConnexes);

            // r�v�lation des cases connexes en si la case s�lectionn�e est entour�e de 0 bombes
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
            // r�cup�ration des cases connexes
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

            // assigne l'�tat de la bombe en fonction du nombre de bombes adjacentes
            if (selectedCase.Etat != DemineurEtat.Bombe)
            {
                selectedCase.Etat = (DemineurEtat)nbBombesConnexes;
            }

            return nbBombesConnexes;
        }
    }
}